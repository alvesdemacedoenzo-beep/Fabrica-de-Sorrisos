using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FabricaDeSorrisos.Web.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize] // Segurança: Só quem tá logado pode comprar
public class CarrinhoController : ControllerBase
{
    private readonly ICarrinhoRepository _carrinhoRepo;
    private readonly IBrinquedoRepository _brinquedoRepo;

    public CarrinhoController(ICarrinhoRepository carrinhoRepo, IBrinquedoRepository brinquedoRepo)
    {
        _carrinhoRepo = carrinhoRepo;
        _brinquedoRepo = brinquedoRepo;
    }

    // AÇÃO: ADICIONAR AO CARRINHO
    // Rota: POST /api/carrinho/adicionar/5
    [HttpPost("adicionar/{brinquedoId}")]
    public async Task<IActionResult> Adicionar(int brinquedoId)
    {
        // 1. Descobrir quem é o usuário (usando o Cookie Mágico que configuramos)
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");

        if (string.IsNullOrEmpty(usuarioIdClaim))
            return Unauthorized(new { success = false, message = "Sessão expirada. Faça login novamente." });

        int usuarioId = int.Parse(usuarioIdClaim);

        // 2. Validar o Brinquedo
        var brinquedo = await _brinquedoRepo.GetByIdAsync(brinquedoId);
        if (brinquedo == null) return NotFound(new { success = false, message = "Brinquedo não encontrado." });

        if (!brinquedo.Ativo) return BadRequest(new { success = false, message = "Produto indisponível." });
        if (brinquedo.Estoque <= 0) return BadRequest(new { success = false, message = "Produto esgotado!" });

        // 3. Verificar se já existe no carrinho
        var itemExistente = await _carrinhoRepo.GetItemAsync(usuarioId, brinquedoId);

        if (itemExistente != null)
        {
            // Se já tem, aumenta a quantidade
            itemExistente.Quantidade++;
            await _carrinhoRepo.UpdateAsync(itemExistente);
        }
        else
        {
            // Se não tem, cria novo
            var novoItem = new CarrinhoItem
            {
                UsuarioId = usuarioId,
                BrinquedoId = brinquedoId,
                Quantidade = 1
            };
            await _carrinhoRepo.AddAsync(novoItem);
        }

        // 4. Calcular novo total de itens para atualizar o ícone
        var carrinhoAtual = await _carrinhoRepo.GetCarrinhoDoUsuarioAsync(usuarioId);
        int totalItens = carrinhoAtual.Sum(i => i.Quantidade);

        return Ok(new { success = true, totalItens = totalItens, message = "Adicionado com sucesso!" });
    }

    // AÇÃO: RESUMO (Para atualizar o ícone ao carregar a página)
    [HttpGet("resumo")]
    public async Task<IActionResult> Resumo()
    {
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");
        if (string.IsNullOrEmpty(usuarioIdClaim)) return Ok(new { totalItens = 0 });

        int usuarioId = int.Parse(usuarioIdClaim);
        var carrinho = await _carrinhoRepo.GetCarrinhoDoUsuarioAsync(usuarioId);

        return Ok(new { totalItens = carrinho.Sum(c => c.Quantidade) });
    }
}

