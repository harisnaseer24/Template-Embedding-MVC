using _2301C2TempEmbedding.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _2301C2TempEmbedding.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

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
            return View();
        }

       
    }
}