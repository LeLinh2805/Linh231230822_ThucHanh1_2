using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using ThucHanh1.Data;
using ThucHanh1.Models;

namespace ThucHanh1.Controllers
{
    public class LearnerController : Controller
    {
        private readonly SchoolContext _context;

        public LearnerController(SchoolContext context)
        {
            _context = context;
            
        }
        public IActionResult Create()
        {
            ViewBag.MajorID = new SelectList(_context.Majors, "MajorID", "MajorName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                _context.Learners.Add(learner);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MajorID = new SelectList(_context.Majors, "MajorID", "MajorName");
            return View(learner);
        }

        public IActionResult Edit(int id)
        {
            var learner = _context.Learners.Find(id);

            if (learner == null)
                return NotFound();

            ViewBag.MajorID = new SelectList(_context.Majors, "MajorID", "MajorName", learner.MajorID);

            return View(learner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(learner);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MajorID = new SelectList(_context.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }
        public IActionResult Delete(int id)
        {
            var learner = _context.Learners
                            .Include(l => l.Major)
                            .Include(l => l.Enrollments)
                            .FirstOrDefault(l => l.LearnerID == id);

            if (learner == null)
                return NotFound();

            //if (learner.Enrollments.Count > 0)
              //  return Content("This learner has enrollments and cannot be deleted.");

            return View(learner);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var learner = _context.Learners
                            .Include(l => l.Enrollments) // phải include để xóa enrollment
                            .FirstOrDefault(l => l.LearnerID == id);

            if (learner != null)
            {
                // Xóa tất cả enrollment trước
                if (learner.Enrollments.Any())
                    _context.Enrollments.RemoveRange(learner.Enrollments);

                _context.Learners.Remove(learner);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = _context.Learners
                .Where(l => l.MajorID == mid)
                .Include(m => m.Major)
                .ToList();

            return PartialView("LearnerTable", learners);
        }
        private int pageSize = 3;

        public IActionResult Index(int? mid)
        {
            var learners = _context.Learners.Include(m => m.Major).AsQueryable();

            if (mid != null)
            {
                learners = learners.Where(l => l.MajorID == mid)
                                   .Include(m => m.Major);
            }

            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;

            var result = learners.Take(pageSize).ToList();
            return View(result);
        }
        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            var learners = _context.Learners.AsQueryable();

            int page = pageIndex == null || pageIndex <= 0 ? 1 : pageIndex.Value;

            if (mid != null)
            {
                learners = learners.Where(l => l.MajorID == mid);
            }
            ViewBag.mid = mid;

            if (!string.IsNullOrEmpty(keyword))
            {
                learners = learners.Where(l => l.FirstMidName.ToLower()
                .Contains(keyword.ToLower()));
            }
            ViewBag.keyword = keyword;

            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;

            var result = learners
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Include(m => m.Major)
                .ToList();

            return PartialView("LearnerTable", result);
        }




    }

}
