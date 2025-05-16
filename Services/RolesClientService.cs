using frontendnet.Models;

namespace frontendnet.Services
{
    public class RolesClientService(HttpClient client)
    {
        public async Task<List<Rol>?> GetAsync()
        {
            return await client.GetFromJsonAsync<List<Rol>>("api/roles");
        }
    }
}
