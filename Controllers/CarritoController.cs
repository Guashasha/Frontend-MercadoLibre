using frontendnet.Models;
using System.Security.Claims;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet
{
    [Authorize(Roles = "Usuario")]
    public class CarritoController(CarritoClientService carritoService,
    IConfiguration configuration, PedidosClientService pedidosService) : Controller
    {

        private AuthUser? usuario = null;

        public async Task<IActionResult> Index()
        {
            usuario = new AuthUser
            {
                Email = User.FindFirstValue(ClaimTypes.Name)!,
                Nombre = User.FindFirstValue(ClaimTypes.GivenName)!,
                Rol = User.FindFirstValue(ClaimTypes.Role)!,
                Jwt = User.FindFirstValue("jwt")!
            };

            Carrito? car;
            car = await carritoService.GetAsync();

            ViewBag.Url = configuration["UrlWebAPI"];

            return View(car.Productos);
        }

        public async Task<IActionResult> IncreaseQuantity(int id)
        {
            usuario = new AuthUser
            {
                Email = User.FindFirstValue(ClaimTypes.Name)!,
                Nombre = User.FindFirstValue(ClaimTypes.GivenName)!,
                Rol = User.FindFirstValue(ClaimTypes.Role)!,
                Jwt = User.FindFirstValue("jwt")!
            };

            try
            {
                await carritoService.PutItemAsync(usuario.Email, id);

                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DecreaseQuantity(int id)
        {
            try
            {
                await carritoService.DeleteItemAsync(id);

                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await carritoService.DeleteProductAsync(id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BuyProducts()
        {

            try
            {
                Carrito? car = await carritoService.GetAsync();

                foreach (var producto in car.Productos)
                {
                    int productoid = producto.CarritoProducto.ProductoId;
                    int cantidad = producto.CarritoProducto.Cantidad;

                    await pedidosService.PostAsync(productoid, cantidad);

                }

                TempData["SuccessMessage"] = "Compra realizada con exito, Tus productos estan pendientes de envio.";

                return Redirect("/pedidos");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return RedirectToAction("Index");
        }
    }
}