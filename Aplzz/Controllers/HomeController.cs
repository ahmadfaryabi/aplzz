using Microsoft.AspNetCore.Mvc;

namespace Aplzz.controllers
{
    
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}