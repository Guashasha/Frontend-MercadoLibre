using frontendnet.Models;

namespace frontendnet.Services
{
    public class ProductosClientService(HttpClient client)
    {
        public async Task<List<Producto>?> GetAsync(string? search)
        {
            return await client.GetFromJsonAsync<List<Producto>>($"api/productos?s={search}");
        }

        public async Task<Producto?> GetAsync(int id)
        {
            return await client.GetFromJsonAsync<Producto>($"api/productos/{id}");
        }

        public async Task PostAsync(Producto producto)
        {
            Console.WriteLine("Este es el cliente el servicio"); 
            var response = await client.PostAsJsonAsync($"api/productos", producto);
            response.EnsureSuccessStatusCode(); 
        }

        public async Task PutAsync(Producto producto)
        {
            var response = await client.PutAsJsonAsync($"api/productos/{producto.ProductoId}", producto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await client.DeleteAsync($"api/productos/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task PostAsync(int id, int categoriaid)
        {
            var response = await client.PostAsJsonAsync($"api/productos/{id}/categoria", new { categoriaid });
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id, int categoriaid)
        {
            var response = await client.DeleteAsync($"api/productos/{id}/categoria/{categoriaid}");
            response.EnsureSuccessStatusCode();
        }
    }
}
