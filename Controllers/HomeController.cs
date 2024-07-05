using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechNation.Models;

namespace TechNation.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
