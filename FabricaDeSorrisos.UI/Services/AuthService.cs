using FabricaDeSorrisos.UI.Api;
using FabricaDeSorrisos.UI.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FabricaDeSorrisos.UI.Models.Services
{
    public class AuthService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var client = ApiClient.GetClient();

            var response = await client.PostAsJsonAsync("auth/login", request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<LoginResponse>();
        }
    }
}
