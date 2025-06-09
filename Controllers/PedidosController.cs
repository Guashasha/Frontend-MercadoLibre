using frontendnet.Models;
using System.Security.Claims;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet
{
    [Authorize(Roles = "Usuario")]
    public class PedidosController(PedidosClientService pedidosService, IConfiguration configuration) : Controller
    {

        public async Task<IActionResult> Index()
        {
            List<Pedido>? lista = [];
            ViewBag.Url = configuration["UrlWebAPI"];

            try
            {
                lista = await pedidosService.GetAsync();
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }

            return View(lista);
        }

    }
}