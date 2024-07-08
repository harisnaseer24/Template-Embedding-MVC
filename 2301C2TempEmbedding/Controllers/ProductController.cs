using _2301C2TempEmbedding.Models;

using Microsoft.AspNetCore.Mvc;

namespace _2301C2TempEmbedding.Controllers
{
    public class ProductController : Controller
    {
        EcommerceContext db = new EcommerceContext();
        public IActionResult Index()
        {

            return View(db.Products.ToList());
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product p1)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(p1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {

            return View();
            }
        }
    }
}
