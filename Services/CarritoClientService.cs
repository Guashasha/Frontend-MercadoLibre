using System.Text;
using frontendnet.Models;
using Microsoft.Extensions.ObjectPool;

namespace frontendnet.Services
{
    public class CarritoClientService(HttpClient client)
    {
        public async Task<Carrito?> GetAsync()
        {
            return await client.GetFromJsonAsync<Carrito>($"api/cart/"); 
        }

        public async Task PostItemAsync(int productId){
            var data = new { ProductoId = productId}; 
            var response = await client.PostAsJsonAsync($"api/cart/item", data);
            response.EnsureSuccessStatusCode(); 
        }

        public async Task PutItemAsync(string email, int productId){
            var data = new { Email = email, ProductoId = productId}; 
            var response = await client.PutAsJsonAsync($"api/cart/item", data); 
            response.EnsureSuccessStatusCode(); 
        }

        public async Task DeleteItemAsync(int productId)
        {
            var response = await client.DeleteAsync($"api/cart/{productId}");
            response.EnsureSuccessStatusCode(); 
        }

        public async Task DeleteProductAsync(int productId)
        {
            var response = await client.DeleteAsync($"api/cart/producto/{productId}"); 
            response.EnsureSuccessStatusCode(); 
        }
    }
}
