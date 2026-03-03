using FabricaDeSorrisos.Application.Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FabricaDeSorrisos.Web.Controllers;

[Authorize] // Só usuários logados podem ver seus pedidos
public class PedidosController : Controller
{
    private readonly IPedidoRepository _pedidoRepo;

    public PedidosController(IPedidoRepository pedidoRepo)
    {
        _pedidoRepo = pedidoRepo;
    }

    // LISTAR MEUS PEDIDOS
    public async Task<IActionResult> Index()
    {
        var usuarioId = GetUsuarioId();

        // Pega todos os pedidos. (Se você já tiver um método GetPedidosPorUsuarioAsync no repositório, use-o).
        // Aqui estou usando o método genérico e filtrando pelo ID do usuário logado.
        var todosPedidos = await _pedidoRepo.GetTodosPedidosAsync();
        var meusPedidos = todosPedidos
            .Where(p => p.UsuarioId == usuarioId)
            .OrderByDescending(p => p.DataPedido)
            .ToList();

        return View(meusPedidos);
    }

    // VER DETALHES DE UM PEDIDO
    public async Task<IActionResult> Detalhes(int id)
    {
        var pedido = await _pedidoRepo.GetPedidoPorIdAsync(id);

        // Regra de segurança: O pedido deve existir e pertencer a quem está logado
        if (pedido == null || pedido.UsuarioId != GetUsuarioId())
            return NotFound();

        return View(pedido);
    }

    private int GetUsuarioId()
    {
        var claim = User.FindFirstValue("UsuarioSistemaId");
        return int.Parse(claim ?? "0");
    }
}