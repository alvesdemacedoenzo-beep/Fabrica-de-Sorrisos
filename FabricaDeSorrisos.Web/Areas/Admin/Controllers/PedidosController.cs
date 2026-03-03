using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FabricaDeSorrisos.Web.Areas.Admin.Models;

namespace FabricaDeSorrisos.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Administrador,ADMINISTRADOR")]

public class PedidosController : Controller
{
    private readonly IPedidoRepository _pedidoRepo;

    // Removemos o UserManager, não precisa mais dele!
    public PedidosController(IPedidoRepository pedidoRepo)
    {
        _pedidoRepo = pedidoRepo;
    }

    // 1. LISTA SEPARADA POR STATUS
    public async Task<IActionResult> Index()
    {
        var todosPedidos = await _pedidoRepo.GetTodosPedidosAsync();

        var viewModel = new PedidosIndexViewModel
        {
            // Filtra quem está aguardando
            Pendentes = todosPedidos.Where(p => p.Status == "Aguardando Pagamento").ToList(),

            // Todo o resto vai para finalizados (Pagamento Confirmado, Entregue, etc)
            Finalizados = todosPedidos.Where(p => p.Status != "Aguardando Pagamento").ToList()
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Detalhes(int id)
    {
        // Como atualizamos o Repository, esse pedido já vem com pedido.Usuario preenchido
        var pedido = await _pedidoRepo.GetPedidoPorIdAsync(id);

        if (pedido == null) return NotFound();

        // Agora pegamos direto do objeto, sem consultar Identity
        // Nota: Certifique-se que sua entidade 'Usuario' tem as propriedades Nome/Email
        if (pedido.Usuario != null)
        {
            ViewBag.NomeCliente = pedido.Usuario.NomeCompleto;
            ViewBag.EmailCliente = pedido.Usuario.Email;
        }
        else
        {
            ViewBag.NomeCliente = "Cliente não identificado";
            ViewBag.EmailCliente = "-";
        }

        return View(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmarPagamento(int id)
    {
        var pedido = await _pedidoRepo.GetPedidoPorIdAsync(id);

        if (pedido == null) return NotFound();

        // MUDANÇA DE STATUS
        pedido.Status = "Pagamento Confirmado";

        // Salva no banco
        await _pedidoRepo.UpdateAsync(pedido);

        TempData["Sucesso"] = "Pedido atualizado com sucesso!";

        // Volta para a tela de detalhes para você ver a mudança
        return RedirectToAction(nameof(Detalhes), new { id = pedido.Id });
    }
}