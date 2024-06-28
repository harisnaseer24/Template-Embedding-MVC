using _2301C2TempEmbedding.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _2301C2TempEmbedding.Controllers
{
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("role") == "admin") {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
           
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

        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string pass)
        {
            if(email=="admin@gmail.com" && pass == "123")
            {
                HttpContext.Session.SetString("adminEmail", email);
                HttpContext.Session.SetString("role", "admin");



                // return RedirectToAction("Index","Home");
                return RedirectToAction("Index");
            }
            else if (email == "user@gmail.com" && pass == "123")
            {
                HttpContext.Session.SetString("userEmail", email);
                HttpContext.Session.SetString("role", "user");

                return RedirectToAction("Index","Home");

            }
            else
            {

                ViewBag.msg = "Invalid Credentials";
                return View();
            }
           
        }
    }
}
