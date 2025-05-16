using frontendnet.Models;
using System.Security.Claims;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet
{
    [Authorize(Roles = "Usuario")]
    public class CarritoController() : Controller
    {

        private AuthUser? usuario = null; 

        public async Task<IActionResult> Index()
        {
          return View();
        }
    }
}
