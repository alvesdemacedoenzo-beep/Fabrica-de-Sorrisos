using FabricaDeSorrisos.UI.Api;
using System.Net.Http.Json;

namespace FabricaDeSorrisos.UI.Services
{
    public class PersonagemService
    {
        private readonly HttpClient _client;
        public PersonagemService()
        {
            _client = ApiClient.GetClient();
        }

        public record PersonagemItem(int Id, string Nome);
        public record PersonagemRequest(string Nome);

        public async Task<List<PersonagemItem>> GetAllAsync()
        {
            var result = await _client.GetFromJsonAsync<List<PersonagemItem>>("personagens");
            return result ?? new List<PersonagemItem>();
        }

        public async Task<bool> CreateAsync(string nome)
        {
            var resp = await _client.PostAsJsonAsync("personagens", new PersonagemRequest(nome));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, string nome)
        {
            var resp = await _client.PutAsJsonAsync($"personagens/{id}", new PersonagemRequest(nome));
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _client.DeleteAsync($"personagens/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
