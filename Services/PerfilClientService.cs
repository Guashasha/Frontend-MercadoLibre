namespace frontendnet.Services
{
    public class PerfilClientService(HttpClient client)
    {
        public async Task<string> ObtenTiempoAsync()
        {
            return await client.GetStringAsync($"api/auth/tiempo");
        }
    }
}
