using System.Net;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet; 

[Authorize(Roles = "Administrador")]
public class ArchivosController(ArchivosClientService archivos, IConfiguration configuration) : Controller
{
    public async Task<IActionResult> Index()
    {
        List<Archivo>? lista = [];
        try
        {
            lista = await archivos.GetAsync(); 
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }

        ViewBag.Url = configuration["UrlWebAPI"];
        return View(lista);
    }

    public async Task<IActionResult> Detalle(int id)
    {
        Archivo? item = null;
        try
        {
            item = await archivos.GetAsync(id);
            if (item == null) return NotFound();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }

        ViewBag.Url = configuration["UrlWebAPI"];
        return View(item); 
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CrearAsync(Upload itemToCreate)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        if (ModelState.IsValid)
        {
            try
            {
                if ((itemToCreate.Portada.Length / 1024) > 100)
                {
                    ModelState.AddModelError("Portada", $"El archivo de {itemToCreate.Portada.Length / 1024} KB supera el tamprecio máximo permitido.");
                }
                if (itemToCreate.Portada.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Portada", $"El archivo {itemToCreate.Portada.FileName} no tiene una extensión permitida.");
                    return View(itemToCreate);
                }

                await archivos.PostAsync(itemToCreate);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }

        ModelState.AddModelError("Portada", "No ha sido posible realizar la acción. Inténtelo nuevamente");
        return View(itemToCreate); 
    }

    public async Task<IActionResult> EditarAsync(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        try
        {
            Archivo? itemToEdit = await archivos.GetAsync(id);
            ViewBag.ArchivoId = itemToEdit?.ArchivoId; 
            ViewBag.Nombre = itemToEdit?.Nombre;
            if (itemToEdit == null) return NotFound();
        }catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditarAsync(int id, Upload itemToEdit)
    {
        if (id != itemToEdit.ArchivoId) return NotFound();

        ViewBag.Url = configuration["UrlWebAPI"];
        if (ModelState.IsValid)
        {
            try
            {
                if ((itemToEdit.Portada.Length / 1024) > 100)
                {
                    ModelState.AddModelError("Portada", $"El archivo de {itemToEdit.Portada.Length / 1024} KB supera el tamprecio máximo permitido.");
                    return View(itemToEdit); 
                }
                if (itemToEdit.Portada.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("Portada", $"El archivo {itemToEdit.Portada.FileName} no tiene una extensión permitida");
                    return View(itemToEdit);
                }

                await archivos.PutAsync(itemToEdit);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }

        ModelState.AddModelError("Portada", "No ha sido posible realizar la acción. Inténtelo nuevamente");
        return View(itemToEdit);
    }

    public async Task<IActionResult> Eliminar(int id, bool? showError = false)
    {
        Archivo? itemToDelete = null; 
        try
        {
            itemToDelete = await archivos.GetAsync(id);
            if (itemToDelete == null) return NotFound();

            if (showError.GetValueOrDefault())
                ViewData["ErrorMessage"] = "No ha sido posible realizar la acción. Inténtelo nuevamente";
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Salir", "Auth");
        }

        ViewBag.Url = configuration["UrlWebAPI"];
        return View(itemToDelete); 
    }

    [HttpPost]
    public async Task<IActionResult> Eliminar(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        if (ModelState.IsValid)
        {
            try
            {
                await archivos.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Unauthorized)
                    return RedirectToAction("Salir", "Auth");
            }
        }
        return RedirectToAction(nameof(Eliminar), new { id, showError = true}); 
    }
}