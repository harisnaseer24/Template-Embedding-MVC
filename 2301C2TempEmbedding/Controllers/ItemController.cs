using _2301C2TempEmbedding.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _2301C2TempEmbedding.Controllers
{
    public class ItemController : Controller
    {
        //EcommerceContext db = new EcommerceContext();
        private readonly EcommerceContext db;
        public ItemController(EcommerceContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
                     var ItemsData = db.Items.Include(a => a.Cat);

            return View(ItemsData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item, IFormFile file)
        {
            var imageName = DateTime.Now.ToString("yymmddhhmmss");//8489327234846347
            imageName += Path.GetFileName(file.FileName);//8489327234846347apple.jpg
            string imagepath= Path.Combine(HttpContext.Request.PathBase.Value,"wwwroot/Uploads");
            var imagevalue = Path.Combine(imagepath,imageName);

            using(var stream = new FileStream(imagevalue, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var dbimage = Path.Combine("/Uploads", imageName);// /Uploads/8489327234846347apple.jpg
            item.Image = dbimage;

            db.Items.Add(item);
            db.SaveChanges();


            ViewBag.CatId = new SelectList(db.Categories, "CatId", "CatName");
            return RedirectToAction("Index");
        }
    }
}
