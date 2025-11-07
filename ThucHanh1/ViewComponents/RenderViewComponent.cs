using Microsoft.AspNetCore.Mvc;
using ThucHanh1.Models;

namespace ThucHanh1.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = new List<MenuItem>
            {
                new() { Id = 1, Name = "Student List", Link = "/Student/Index" },
                new() { Id = 2, Name = "Create Student", Link = "/Student/Create" }
            };

            return View("RenderLeftMenu", menu);
        }
    }
}
