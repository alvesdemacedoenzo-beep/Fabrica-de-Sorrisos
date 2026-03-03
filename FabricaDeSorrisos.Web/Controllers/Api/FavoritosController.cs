using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FabricaDeSorrisos.Web.Controllers.Api;

[Route("api/Favoritos")]
[ApiController]
public class FavoritosControllerAPI : ControllerBase
{
    private readonly IFavoritoRepository _favoritoRepo;

    public FavoritosControllerAPI(IFavoritoRepository favoritoRepo)
    {
        _favoritoRepo = favoritoRepo;
    }

    // --- 1. AÇÃO DE FAVORITAR / DESFAVORITAR (Ao clicar no botão) ---
    [HttpPost("toggle/{brinquedoId}")]
    [Authorize] // Apenas usuários logados
    public async Task<IActionResult> Toggle(int brinquedoId)
    {
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");
        if (string.IsNullOrEmpty(usuarioIdClaim)) return Unauthorized();

        int usuarioId = int.Parse(usuarioIdClaim);

        // Verifica se já existe nos favoritos
        var favoritoExistente = await _favoritoRepo.GetByUsuarioEBrinquedoAsync(usuarioId, brinquedoId);

        if (favoritoExistente != null)
        {
            // Se já é favorito, remove (desfavorita)
            await _favoritoRepo.DeleteAsync(favoritoExistente);
            return Ok(new { success = true, action = "removed" });
        }
        else
        {
            // Se não é, adiciona
            var novoFavorito = new Favorito
            {
                UsuarioId = usuarioId,
                BrinquedoId = brinquedoId,
                DataFavoritado = DateTime.Now
            };
            await _favoritoRepo.AddAsync(novoFavorito);
            return Ok(new { success = true, action = "added" });
        }
    }

    // --- 2. O MÉTODO NOVO: BUSCA OS IDs DOS FAVORITOS (Ao carregar a página) ---
    [HttpGet("meus-ids")]
    public async Task<IActionResult> ObterMeusFavoritosIds()
    {
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");

        // Se o usuário não estiver logado, apenas retorna uma lista vazia e a tela continua com corações vazios
        if (string.IsNullOrEmpty(usuarioIdClaim))
            return Ok(new List<int>());

        int usuarioId = int.Parse(usuarioIdClaim);

        // Busca todos os favoritos do cliente no banco
        var favoritos = await _favoritoRepo.GetByUsuarioIdAsync(usuarioId);

        // Separa apenas os números de ID dos brinquedos para o JavaScript ler fácil
        var ids = favoritos.Select(f => f.BrinquedoId).ToList();

        return Ok(ids);
    }
}