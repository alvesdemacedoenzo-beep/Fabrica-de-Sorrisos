using FabricaDeSorrisos.UI.Api;
using System.Net.Http.Json;

namespace FabricaDeSorrisos.UI.Services
{
    public class CategoryService
    {
        private readonly HttpClient _client;
        public CategoryService()
        {
            _client = ApiClient.GetClient();
        }

        public record CategoriaRequest(string Nome);

        public async Task<bool> CreateAsync(string nome)
        {
            var resp = await _client.PostAsJsonAsync("categorias", new CategoriaRequest(nome));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, string nome)
        {
            var resp = await _client.PutAsJsonAsync($"categorias/{id}", new CategoriaRequest(nome));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _client.DeleteAsync($"categorias/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
