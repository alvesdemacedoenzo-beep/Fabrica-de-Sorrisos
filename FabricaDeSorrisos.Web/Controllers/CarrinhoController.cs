using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FabricaDeSorrisos.Web.Controllers;

[Authorize]
public class CarrinhoController : Controller
{
    private readonly ICarrinhoRepository _carrinhoRepo;
    private readonly IPedidoRepository _pedidoRepo;
    private readonly IBrinquedoRepository _brinquedoRepo;
    private readonly IUsuarioRepository _usuarioRepo; // <--- NOVA INJEÇÃO

    public CarrinhoController(
        ICarrinhoRepository carrinhoRepo,
        IPedidoRepository pedidoRepo,
        IBrinquedoRepository brinquedoRepo,
        IUsuarioRepository usuarioRepo) // <--- NOVO PARÂMETRO
    {
        _carrinhoRepo = carrinhoRepo;
        _pedidoRepo = pedidoRepo;
        _brinquedoRepo = brinquedoRepo;
        _usuarioRepo = usuarioRepo;
    }

    // --- PARTE 1: CARRINHO ---

    public async Task<IActionResult> Index()
    {
        var usuarioId = GetUsuarioId();
        var itens = await _carrinhoRepo.GetCarrinhoDoUsuarioAsync(usuarioId);

        var viewModel = new CarrinhoViewModel { Itens = itens };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AtualizarQuantidade(int brinquedoId, int quantidade)
    {
        var usuarioId = GetUsuarioId();
        var item = await _carrinhoRepo.GetItemAsync(usuarioId, brinquedoId);

        if (item != null)
        {
            if (quantidade <= 0)
            {
                await _carrinhoRepo.DeleteAsync(item);
            }
            else
            {
                // Validação de Estoque
                var brinquedo = await _brinquedoRepo.GetByIdAsync(brinquedoId);
                if (brinquedo != null && quantidade > brinquedo.Estoque)
                {
                    item.Quantidade = brinquedo.Estoque;
                    TempData["Aviso"] = $"Estoque máximo atingido para {brinquedo.Nome}.";
                }
                else
                {
                    item.Quantidade = quantidade;
                }
                await _carrinhoRepo.UpdateAsync(item);
            }
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remover(int itemId)
    {
        var usuarioId = GetUsuarioId();
        var item = await _carrinhoRepo.GetItemAsync(usuarioId, itemId);
        if (item != null) await _carrinhoRepo.DeleteAsync(item);

        return RedirectToAction(nameof(Index));
    }

    // --- PARTE 2: CHECKOUT (PAGAMENTO) ---

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var usuarioId = GetUsuarioId();
        var itens = await _carrinhoRepo.GetCarrinhoDoUsuarioAsync(usuarioId);

        if (!itens.Any()) return RedirectToAction(nameof(Index));

        // Tenta buscar dados do usuário para pré-preencher (Opcional)
        var usuario = await _usuarioRepo.GetByIdAsync(usuarioId);

        var viewModel = new CheckoutViewModel
        {
            Itens = itens,
            ValorFrete = 0,
            CPF = usuario?.Cpf // Se já tiver CPF, já traz preenchido
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult CalcularFrete([FromBody] string cep)
    {
        decimal frete = 0;
        if (string.IsNullOrEmpty(cep) || cep.Length < 8) return BadRequest("CEP inválido");

        var ultimoDigito = int.Parse(cep.Substring(cep.Length - 1));
        frete = ultimoDigito % 2 == 0 ? 15.00m : 25.50m;

        return Ok(new { valor = frete, prazo = "5 dias úteis" });
    }

    [HttpPost]
    public async Task<IActionResult> ProcessarPedido(CheckoutViewModel model)
    {
        // 1. VALIDAÇÃO
        if (!ModelState.IsValid)
        {
            var uId = GetUsuarioId();
            model.Itens = await _carrinhoRepo.GetCarrinhoDoUsuarioAsync(uId);
            return View("Checkout", model);
        }

        var usuarioId = GetUsuarioId();
        var itensCarrinho = await _carrinhoRepo.GetCarrinhoDoUsuarioAsync(usuarioId);

        if (!itensCarrinho.Any()) return RedirectToAction(nameof(Index));

        // --- PASSO NOVO: SALVAR O CPF NO CADASTRO DO USUÁRIO ---
        var usuario = await _usuarioRepo.GetByIdAsync(usuarioId);
        if (usuario != null)
        {
            // Atualiza o CPF com o que veio do formulário
            usuario.Cpf = model.CPF;
            await _usuarioRepo.UpdateAsync(usuario);
        }
        // -------------------------------------------------------

        var enderecoCompleto = $"{model.Logradouro}, {model.Numero} - {model.Bairro}, {model.Cidade}/{model.UF}";
        if (!string.IsNullOrEmpty(model.Complemento))
        {
            enderecoCompleto += $" ({model.Complemento})";
        }

        // 2. Cria o Pedido
        var pedido = new Pedido
        {
            UsuarioId = usuarioId,
            DataPedido = DateTime.Now,
            Status = "Aguardando Pagamento",
            ValorFrete = model.ValorFrete,
            ValorTotal = itensCarrinho.Sum(i => i.Quantidade * i.Brinquedo.Preco) + model.ValorFrete,
            EnderecoEntrega = enderecoCompleto,
            CEP = model.CEP,
            FormaPagamento = model.FormaPagamento,
            Itens = new List<PedidoItem>()
        };

        // 3. Transfere Itens e Baixa Estoque
        foreach (var item in itensCarrinho)
        {
            var brinquedo = await _brinquedoRepo.GetByIdAsync(item.BrinquedoId);

            if (brinquedo == null || brinquedo.Estoque < item.Quantidade)
            {
                TempData["Erro"] = $"Ops! O produto {item.Brinquedo.Nome} acabou agora.";
                return RedirectToAction(nameof(Index));
            }

            var pedidoItem = new PedidoItem
            {
                BrinquedoId = item.BrinquedoId,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.Brinquedo.Preco
            };
            pedido.Itens.Add(pedidoItem);

            brinquedo.Estoque -= item.Quantidade;
            await _brinquedoRepo.UpdateAsync(brinquedo);
        }

        await _pedidoRepo.AddAsync(pedido);
        await _carrinhoRepo.LimparCarrinhoAsync(usuarioId);

        return RedirectToAction(nameof(PedidoConfirmado), new { id = pedido.Id });
    }

    // --- PARTE 3: SUCESSO ---

    public async Task<IActionResult> PedidoConfirmado(int id)
    {
        var pedido = await _pedidoRepo.GetPedidoPorIdAsync(id);

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