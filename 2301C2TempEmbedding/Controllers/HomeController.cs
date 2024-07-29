using _2301C2TempEmbedding.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace _2301C2TempEmbedding.Controllers
{
    public class HomeController : Controller
    {
        private readonly EcommerceContext db;
        public HomeController(EcommerceContext _db)
        {
            db = _db;
        }

        [Authorize]
        public IActionResult Index()
        {

            //if (HttpContext.Session.GetString("role") == "user")
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            return View();
        }

        [Authorize(Roles ="User")]
        public IActionResult Contact()
        {
            return View();
        } 
        public IActionResult About()
        {
            return View();
        }  
        public IActionResult Products()
        {
            var ItemsData = db.Items.Include(a => a.Cat);
            return View(ItemsData);
        }
        
        
        public IActionResult Details(int id)
        {
            var ItemsData = db.Items.Include(a => a.Cat);
            var ItemDetail = ItemsData.FirstOrDefault(b => b.Id == id);
            if(ItemDetail  != null)
            {

            return View(ItemDetail);
            }
            else
            {
                return RedirectToAction("Products");
            }
        }

       
    }
}