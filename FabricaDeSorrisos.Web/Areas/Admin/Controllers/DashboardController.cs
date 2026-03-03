using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Identity;
using FabricaDeSorrisos.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Administrador,ADMINISTRADOR,Gerente")]

public class DashboardController : Controller
{
    private readonly IBrinquedoRepository _brinquedoRepo;
    private readonly IPedidoRepository _pedidoRepo;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardController(
        IBrinquedoRepository brinquedoRepo,
        IPedidoRepository pedidoRepo,
        UserManager<ApplicationUser> userManager)
    {
        _brinquedoRepo = brinquedoRepo;
        _pedidoRepo = pedidoRepo;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var brinquedos = await _brinquedoRepo.GetAllAsync();
        var todosPedidos = await _pedidoRepo.GetTodosPedidosAsync();
        var qtdUsuarios = await _userManager.Users.CountAsync();

        // --- LÓGICA: BRINQUEDOS MAIS VENDIDOS ---
        var maisPopulares = new List<ItemPopular>();
        if (todosPedidos.Any())
        {
            maisPopulares = todosPedidos
                .SelectMany(p => p.Itens ?? new List<PedidoItem>())
                .GroupBy(i => i.Brinquedo?.Nome ?? "Item")
                .Select(g => new ItemPopular
                {
                    Nome = g.Key,
                    Quantidade = g.Sum(x => x.Quantidade)
                })
                .OrderByDescending(x => x.Quantidade)
                .Take(5) // Top 5
                .ToList();
        }

        var recentes = new List<ItemRecente>();
        if (todosPedidos.Any())
        {
            recentes = todosPedidos
                .OrderByDescending(p => p.DataPedido)
                .Take(5)
                .Select(p => new ItemRecente
                {
                    Nome = p.Usuario != null ? p.Usuario.NomeCompleto : $"Pedido #{p.Id}",
                    Data = p.DataPedido.ToString("dd/MM/yyyy")
                })
                .ToList();
        }

        var categoriasData = brinquedos
            .GroupBy(b => b.Categoria?.Nome ?? "Outros")
            .Select(g => new { Categoria = g.Key, Qtd = g.Count() })
            .ToList();

        var viewModel = new DashboardViewModel
        {
            QtdBrinquedos = brinquedos.Count,
            QtdUsuarios = qtdUsuarios,
            QtdPedidos = todosPedidos.Count,
            FaturamentoTotal = todosPedidos.Sum(p => p.ValorTotal),

            MaisPopulares = maisPopulares,
            MaisRecentes = recentes,

            GraficoColunasLabels = categoriasData.Select(x => x.Categoria).ToArray(),
            GraficoColunasValores = categoriasData.Select(x => x.Qtd).ToArray(),

            // AGORA MANDAMOS OS DADOS DOS BRINQUEDOS PARA O SEGUNDO GRÁFICO
            GraficoBrinquedosLabels = maisPopulares.Select(x => x.Nome).ToArray(),
            GraficoBrinquedosValores = maisPopulares.Select(x => x.Quantidade).ToArray()
        };

        return View(viewModel);
    }
}