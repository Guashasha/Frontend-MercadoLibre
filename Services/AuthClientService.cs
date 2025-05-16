using System.Security.Claims;
using frontendnet.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace frontendnet.Services;

public class AuthClientService(HttpClient client, IHttpContextAccessor httpContextAccessor)
{
    public async Task<AuthUser> ObtenerTokenAsync(string email, string password)
    {
        Login usuario = new() { Email = email, Password = password};

        var response = await client.PostAsJsonAsync("api/auth", usuario);
        var token = await response.Content.ReadFromJsonAsync<AuthUser>();

        return token!;
    }

    public async void IniciaSesionAsync(List<Claim>  claims)
    {
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties(); 
        await httpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties)!;
    }
}