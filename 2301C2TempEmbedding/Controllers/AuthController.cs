using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using _2301C2TempEmbedding.Models;
using Microsoft.AspNetCore.Identity;

namespace _2301C2TempEmbedding.Controllers
{
    public class AuthController : Controller
    {
        private readonly EcommerceContext db;
        public AuthController(EcommerceContext _db)
        {
            db = _db;
        }

        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(User user)
        {
            var CheckExistingUser = db.Users.FirstOrDefault(t => t.Email == user.Email);
            if(CheckExistingUser != null)
            {
                ViewBag.msg = "User Already Exists";
                return View();
            }

            var hasher = new PasswordHasher<string>();
            user.Password = hasher.HashPassword(user.Email, user.Password);
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {

            bool IsAuthenticated = false;
            string controller = "";

            ClaimsIdentity identity = null;

            var checkUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (checkUser != null)
            {
                var hasher = new PasswordHasher<string>();
                var verifyPass = hasher.VerifyHashedPassword(checkUser.Email, checkUser.Password, user.Password);

                if (verifyPass == PasswordVerificationResult.Success && checkUser.RoleId == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name ,checkUser.Username),
                    new Claim(ClaimTypes.Sid ,checkUser.Id.ToString()),
                    new Claim(ClaimTypes.Role ,"Admin"),
                }
                   , CookieAuthenticationDefaults.AuthenticationScheme);
                    IsAuthenticated = true;
                    controller = "Admin";
                }
                else if (verifyPass == PasswordVerificationResult.Success && checkUser.RoleId == 2)
                {
                    IsAuthenticated = true;
                    identity = new ClaimsIdentity(new[]
                   {
                    new Claim(ClaimTypes.Name ,checkUser.Username),
                      new Claim(ClaimTypes.Sid ,checkUser.Id.ToString()),
                    new Claim(ClaimTypes.Role ,"User"),
                }
                   , CookieAuthenticationDefaults.AuthenticationScheme);
                    controller = "Home";
                    HttpContext.Session.SetInt32("UserId",checkUser.Id);
                }
                else
                {
                    IsAuthenticated = false;
                    ViewBag.msg = "Invalid Credentials";

                }
                if (IsAuthenticated)
                {
                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", controller);
                }
  
            else
            {
                    ViewBag.msg = "Invalid Credentials";
                    return View();
            }
               
            }
            else
            {
                ViewBag.msg = "User not found";
                return View();
            }

           
        }
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


    }
}
