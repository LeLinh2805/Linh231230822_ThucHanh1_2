using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThucHanh1.Models;

namespace ThucHanh1.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>();
        public StudentController()
        {
            listStudents = new List<Student>()
            {
                new Student()
                {
                    Id = 231230822,
                    Name = "Le Van Linh",
                    Branch = Branch.IT,
                    Gender = Gender.Male,
                    IsRegular = true,
                    Address = "HTM",
                    Email = "levanlinh83014@gmail.com"

                },

                new Student()
                {
                    Id = 231235555,
                    Name = "Tung Duong",
                    Branch = Branch.CE,
                    Gender = Gender.Male,
                    IsRegular = true,
                    Address = "HP",
                    Email = "tungduong@gmail.com"

                },
                new Student()
                {
                    Id = 231234444,
                    Name = "Hai Yen",
                    Branch = Branch.EE,
                    Gender = Gender.Female,
                    IsRegular = false,
                    Address = "CG",
                    Email = "haiyen@gmail.com"

                },
                new Student()
                {
                    Id = 231230822,
                    Name = "Cam Van",
                    Branch = Branch.BE,
                    Gender = Gender.Female,
                    IsRegular = false,
                    Address = "HD",
                    Email = "van@gmail.com"

                }

            };
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem() { Text= "IT", Value= "1"},
                new SelectListItem() { Text= "BE", Value= "2"},
                new SelectListItem() { Text= "CE", Value= "3"},
                new SelectListItem() { Text= "EE", Value= "4"},
            };
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student s)
        { 
            s.Id = listStudents.Last<Student>().Id + 1;
            listStudents.Add(s);
            return View("Index", listStudents);
        }
        public IActionResult Index()
        {
            return View(listStudents);
        }
    }
}
