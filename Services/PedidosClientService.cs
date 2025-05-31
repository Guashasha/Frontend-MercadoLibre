using frontendnet.Models; 
using System.Net.Http.Headers; 

namespace frontendnet.Services
{
    public class PedidosClientService(HttpClient client)
    {
        public async Task<List<Pedido>?> GetAsync()
        {
            return await client.GetFromJsonAsync<List<Pedido>>("api/pedidos");
        }

        public async Task PostAsync(int productoid, int cantidad)
        {
            var data = new { Productoid = productoid, Cantidad = cantidad };
            Console.WriteLine(data.Productoid);
            var response = await client.PostAsJsonAsync($"api/pedidos", data);
            response.EnsureSuccessStatusCode();
        }
    }
}
