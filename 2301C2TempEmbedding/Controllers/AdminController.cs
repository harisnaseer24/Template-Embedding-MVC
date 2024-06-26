using _2301C2TempEmbedding.Models;
using Microsoft.AspNetCore.Mvc;

namespace _2301C2TempEmbedding.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product prd)
        {
            if (ModelState.IsValid)
            {
                return Content("data is all good.");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
