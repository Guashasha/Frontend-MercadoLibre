using System.Security.Claims;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet
{
    public class AuthController(AuthClientService auth, UsuariosClientService usuarios) : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(Login model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var token = await auth.ObtenerTokenAsync(model.Email, model.Password);
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, token.Email),
                        new(ClaimTypes.GivenName, token.Nombre),
                        new("jwt", token.Jwt),
                        new(ClaimTypes.Role, token.Rol),
                    };
                    auth.IniciaSesionAsync(claims);

                    if (token.Rol == "Administrador")
                        return RedirectToAction("Index", "Productos");
                    else
                        return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    ModelState.AddModelError(
                        "Email",
                        "Credenciales no válidas. Inténtelo nuevamente."
                    );
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Administrador, Usuario")]
        public async Task<IActionResult> SalirAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CrearUsuario(UsuarioPwd newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(newUser.Rol))
                    {
                        newUser.Rol = "Usuario"; 
                    }

                    await usuarios.PostAsync(newUser);
                    return RedirectToAction("Index", "Auth");
                }
                catch (HttpRequestException ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        return RedirectToAction("Salir", "Auth");
                    ModelState.AddModelError(string.Empty, "Error al crear el usuario. Intente de nuevo.");
                }
            }
            return View(newUser);
        }
    }
}
