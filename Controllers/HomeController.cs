using Microsoft.AspNetCore.Mvc;

namespace frontendnet
{
    public class HomeController : Controller {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error([FromServices] IHostEnvironment hostEnvironment)
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View(); 
        }
    }
}
