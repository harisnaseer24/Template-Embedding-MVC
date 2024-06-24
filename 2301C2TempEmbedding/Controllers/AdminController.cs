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
    }
}
