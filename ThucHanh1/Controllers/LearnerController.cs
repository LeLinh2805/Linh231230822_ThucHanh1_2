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

        public IActionResult Index()
        {
            var learners = _context.Learners
                            .Include(m => m.Major)
                            .ToList();

            return View(learners);
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

            if (learner.Enrollments.Count > 0)
                return Content("This learner has enrollments and cannot be deleted.");

            return View(learner);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var learner = _context.Learners.Find(id);

            if (learner != null)
            {
                _context.Learners.Remove(learner);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }


    }

}
