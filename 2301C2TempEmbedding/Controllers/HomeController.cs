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
                Cart cart = new Cart();
                ViewBag.Cart = cart;
                return View(ItemDetail);
            }
            else
            {
                return RedirectToAction("Products");
            }
        }
        [HttpPost]
        public IActionResult AddToCart(Cart cart)
        {
            var checkDuplicate = db.Carts.Where(X => X.ItemId == cart.ItemId).ToList();
            if (checkDuplicate.Any() && checkDuplicate != null) {


                checkDuplicate[0].Qty += cart.Qty;// 5 + 5

                checkDuplicate[0].Total += cart.Price * cart.Qty; //5 + 5*100
                db.Carts.Update(checkDuplicate[0]);
                db.SaveChanges();
                return RedirectToAction("Products");

            }
            else
            {
                cart.Total = cart.Price * cart.Qty;

                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Products");
            }

            //Cart cartData = new Cart()
            //{
            //    ItemId = Convert.ToInt32(ItemId),
            //    UserId = Convert.ToInt32(UserId),
            //    Qty = Convert.ToInt32(qty),
            //    Price = Convert.ToInt32(Price),
            //    Total= Convert.ToInt32(qty) * Convert.ToInt32(Price),
            //};
            
        }
       
    }
}