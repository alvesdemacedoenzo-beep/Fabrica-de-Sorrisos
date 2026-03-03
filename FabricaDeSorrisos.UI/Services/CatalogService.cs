using FabricaDeSorrisos.UI.Api;
using System.Net.Http.Json;

namespace FabricaDeSorrisos.UI.Services
{
    public class CatalogService
    {
        private readonly HttpClient _client;

        public CatalogService()
        {
            _client = ApiClient.GetClient();
        }

        public record MarcaItem(int Id, string Nome);
        public record SubCategoriaItem(int Id, string Nome);
        public record CategoriaItem(int Id, string Nome, List<SubCategoriaItem> SubCategorias);
        public record FaixaEtariaItem(int Id, string Descricao);
        public record PersonagemItem(int Id, string Nome);

        public async Task<List<MarcaItem>> GetMarcasAsync()
        {
            var result = await _client.GetFromJsonAsync<List<MarcaItem>>("marcas");
            return result ?? new List<MarcaItem>();
        }

        public async Task<List<CategoriaItem>> GetCategoriasAsync()
        {
            var result = await _client.GetFromJsonAsync<List<CategoriaItem>>("categorias");
            return result ?? new List<CategoriaItem>();
        }

        public async Task<List<FaixaEtariaItem>> GetFaixasAsync()
        {
            var result = await _client.GetFromJsonAsync<List<FaixaEtariaItem>>("faixasetarias");
            return result ?? new List<FaixaEtariaItem>();
        }

        public async Task<List<PersonagemItem>> GetPersonagensAsync()
        {
            var result = await _client.GetFromJsonAsync<List<PersonagemItem>>("personagens");
            return result ?? new List<PersonagemItem>();
        }
    }
}
