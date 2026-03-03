using FabricaDeSorrisos.UI.Api;
using System.Net.Http.Json;

namespace FabricaDeSorrisos.UI.Services
{
    public class MarcaService
    {
        private readonly HttpClient _client;
        public MarcaService()
        {
            _client = ApiClient.GetClient();
        }

        public record MarcaRequest(string Nome);

        public async Task<bool> CreateAsync(string nome)
        {
            var resp = await _client.PostAsJsonAsync("marcas", new MarcaRequest(nome));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, string nome)
        {
            var resp = await _client.PutAsJsonAsync($"marcas/{id}", new MarcaRequest(nome));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _client.DeleteAsync($"marcas/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
