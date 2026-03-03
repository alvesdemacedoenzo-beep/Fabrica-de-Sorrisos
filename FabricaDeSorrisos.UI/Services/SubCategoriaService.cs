using FabricaDeSorrisos.UI.Api;
using System.Net.Http.Json;

namespace FabricaDeSorrisos.UI.Services
{
    public class SubCategoriaService
    {
        private readonly HttpClient _client;
        public SubCategoriaService()
        {
            _client = ApiClient.GetClient();
        }

        public record SubCategoriaItem(int Id, string Nome, int CategoriaId);
        public record SubCategoriaRequest(string Nome, int CategoriaId);

        public async Task<List<SubCategoriaItem>> GetAllAsync()
        {
            var result = await _client.GetFromJsonAsync<List<SubCategoriaItem>>("subcategorias");
            return result ?? new List<SubCategoriaItem>();
        }

        public async Task<bool> CreateAsync(string nome, int categoriaId)
        {
            var resp = await _client.PostAsJsonAsync("subcategorias", new SubCategoriaRequest(nome, categoriaId));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, string nome)
        {
            var resp = await _client.PutAsJsonAsync($"subcategorias/{id}", new SubCategoriaRequest(nome, 0));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _client.DeleteAsync($"subcategorias/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
