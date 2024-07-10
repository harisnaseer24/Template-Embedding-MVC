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

        public IActionResult Edit(int id)
        {
            var product= db.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product prd)
        {

            if (ModelState.IsValid)
            {
                db.Products.Update(prd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {

            return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product prd)
        {
            db.Products.Remove(prd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
