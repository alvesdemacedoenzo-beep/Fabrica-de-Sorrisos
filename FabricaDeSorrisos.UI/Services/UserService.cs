using FabricaDeSorrisos.UI.Api;
using FabricaDeSorrisos.UI.ViewModels.UserViewModels;
using System.Net.Http.Json;

namespace FabricaDeSorrisos.UI.Models.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = ApiClient.GetClient();
        }

        // ================================
        // LISTAR TODOS
        // ================================
        public async Task<List<UserViewModel>> ListarTodos()
        {
            try
            {
                return await _httpClient
                    .GetFromJsonAsync<List<UserViewModel>>("Usuarios")
                    ?? new List<UserViewModel>();
            }
            catch
            {
                return new List<UserViewModel>();
            }
        }

        // ================================
        // BUSCAR POR ID
        // ================================
        public async Task<UserViewModel?> ObterPorId(int id)
        {
            try
            {
                return await _httpClient
                    .GetFromJsonAsync<UserViewModel>($"Usuarios/{id}");
            }
            catch
            {
                return null;
            }
        }

        // ================================
        // CRIAR USUÁRIO
        // ================================
        public async Task<bool> Criar(UserViewModel usuario)
        {
            var request = new
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Password = usuario.Senha,
                Role = usuario.TipoUsuario
            };

            var response = await _httpClient.PostAsJsonAsync("auth/register", request);

            if (response.IsSuccessStatusCode)
                return true;

            var status = (int)response.StatusCode;
            var reason = response.ReasonPhrase;
            var body = await response.Content.ReadAsStringAsync();

            throw new Exception(
                $"ERRO AO CRIAR USUÁRIO\n\n" +
                $"Status: {status}\n" +
                $"Motivo: {reason}\n\n" +
                $"Resposta da API:\n{body}"
            );
        }

        // ================================
        // ATUALIZAR USUÁRIO
        // ================================
        public async Task<bool> Atualizar(int id, UserViewModel usuario)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"Usuarios/{id}", usuario);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // ================================
        // EXCLUIR USUÁRIO
        // ================================
        public async Task<bool> Excluir(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Usuarios/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
