using Microsoft.AspNetCore.Mvc;
using ThucHanh1.Data;

namespace ThucHanh1.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        private readonly SchoolContext _context;

        public MajorViewComponent(SchoolContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var majors = _context.Majors.ToList();
            return View(majors);
        }
    }
}
