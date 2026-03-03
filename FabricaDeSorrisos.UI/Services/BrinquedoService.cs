using FabricaDeSorrisos.UI.Api;
using FabricaDeSorrisos.UI.ViewModels.Brinquedos;
using System.Net.Http.Json;

namespace FabricaDeSorrisos.UI.Models.Services
{
    public class BrinquedoService
    {
        private readonly HttpClient _client;
        public class CreateBrinquedoRequest
        {
            public string Nome { get; set; } = string.Empty;
            public string Descricao { get; set; } = string.Empty;
            public decimal Preco { get; set; }
            public int Estoque { get; set; }
            public int MarcaId { get; set; }
            public int CategoriaId { get; set; }
            public int FaixaEtariaId { get; set; }
            public int? PersonagemId { get; set; }
            public bool Ativo { get; set; } = true;
            public string? ImagemBase64 { get; set; }
            public string? ImagemNomeArquivo { get; set; }
        }
        public class UpdateBrinquedoRequest
        {
            public string? Nome { get; set; }
            public string? Descricao { get; set; }
            public decimal? Preco { get; set; }
            public int? Estoque { get; set; }
            public bool? Ativo { get; set; }
            public int? MarcaId { get; set; }
            public int? CategoriaId { get; set; }
            public int? FaixaEtariaId { get; set; }
            public int? PersonagemId { get; set; }
            public int? SubCategoriaId { get; set; }
            public string? ImagemBase64 { get; set; }
            public string? ImagemNomeArquivo { get; set; }
        }

        public BrinquedoService()
        {
            _client = ApiClient.GetClient();
        }

        public async Task<PagedResult<BrinquedoViewModel>> ListarAsync(int pageIndex = 1, int pageSize = 50, string? termo = null, bool incluirInativos = true)
        {
            var url = $"brinquedos?pageIndex={pageIndex}&pageSize={pageSize}&incluirInativos={(incluirInativos ? "true" : "false")}";
            if (!string.IsNullOrWhiteSpace(termo))
                url += $"&termoBusca={Uri.EscapeDataString(termo)}";

            var result = await _client.GetFromJsonAsync<PagedResult<BrinquedoViewModel>>(url);
            return result ?? new PagedResult<BrinquedoViewModel>
            {
                Items = new List<BrinquedoViewModel>(),
                TotalCount = 0,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public async Task<bool> AtivarAsync(int id)
        {
            var resp = await _client.PostAsync($"brinquedos/{id}/ativar", content: null);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DesativarAsync(int id)
        {
            var resp = await _client.PostAsync($"brinquedos/{id}/desativar", content: null);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> CriarAsync(CreateBrinquedoRequest request)
        {
            var resp = await _client.PostAsJsonAsync("brinquedos", request);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> AtualizarAsync(int id, UpdateBrinquedoRequest request)
        {
            var resp = await _client.PutAsJsonAsync($"brinquedos/{id}", request);
            return resp.IsSuccessStatusCode;
        }
    }
}
