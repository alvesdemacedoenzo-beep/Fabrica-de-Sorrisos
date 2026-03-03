using FabricaDeSorrisos.Application.Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FabricaDeSorrisos.Web.Controllers;

[Authorize] // Só logado entra aqui
public class FavoritosController : Controller
{
    private readonly IFavoritoRepository _favoritoRepo;

    public FavoritosController(IFavoritoRepository favoritoRepo)
    {
        _favoritoRepo = favoritoRepo;
    }

    public async Task<IActionResult> Index()
    {
        // 1. Pega o ID do usuário (aquele que configuramos nos Claims)
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");
        if (string.IsNullOrEmpty(usuarioIdClaim)) return RedirectToAction("Login", "Account");

        int usuarioId = int.Parse(usuarioIdClaim);

        // 2. Busca os favoritos no banco
        var favoritos = await _favoritoRepo.GetByUsuarioIdAsync(usuarioId);

        // 3. Retorna a View com a lista
        return View(favoritos);
    }
}