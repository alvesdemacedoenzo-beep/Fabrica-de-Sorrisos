using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Application.DTOs;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using FabricaDeSorrisos.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace FabricaDeSorrisos.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBrinquedoRepository _brinquedoRepo;
    private readonly IMarcaRepository _marcaRepo;
    private readonly ICategoriaRepository _categoriaRepo;
    private readonly IFaixaEtariaRepository _faixaRepo;
    private readonly IPersonagemRepository _personagemRepo;
    private readonly IFeedbackRepository _feedbackRepo;
    private readonly AppDbContext _context;

    public HomeController(
        ILogger<HomeController> logger,
        IBrinquedoRepository brinquedoRepo,
        IMarcaRepository marcaRepo,
        ICategoriaRepository categoriaRepo,
        IFaixaEtariaRepository faixaRepo,
        IPersonagemRepository personagemRepo,
        IFeedbackRepository feedbackRepo,
        AppDbContext context)
    {
        _logger = logger;
        _brinquedoRepo = brinquedoRepo;
        _marcaRepo = marcaRepo;
        _categoriaRepo = categoriaRepo;
        _faixaRepo = faixaRepo;
        _personagemRepo = personagemRepo;
        _feedbackRepo = feedbackRepo;
        _context = context;
    }

    // =============================================================
    // 1. HOME (VITRINE)
    // =============================================================
    public async Task<IActionResult> Index()
    {
        var destaques = await _brinquedoRepo.GetAllAsync();
        var destaquesAtivos = destaques.Where(b => b.Ativo).ToList();

        var viewModel = new CatalogViewModel
        {
            Produtos = new PagedResult<BrinquedoDto>
            {
                Items = destaquesAtivos.Take(6).Select(b => new BrinquedoDto
                {
                    Id = b.Id,
                    Nome = b.Nome,
                    Preco = b.Preco,
                    ImagemUrl = b.ImagemUrl,
                    Estoque = b.Estoque,
                    Ativo = b.Ativo,
                    Marca = b.Marca?.Nome,
                    Categoria = b.Categoria?.Nome
                }).ToList()
            }
        };

        return View(viewModel);
    }

    // =============================================================
    // 2. BUSCA (CATÁLOGO)
    // =============================================================
    [HttpGet]
    public async Task<IActionResult> Busca(
            int pageIndex = 1,
            string? termoBusca = null,
            int? marcaId = null,
            int? categoriaId = null,
            int? subCategoriaId = null,
            int? faixaEtariaId = null,
            int? personagemId = null)
    {
        var todosBrinquedos = await _brinquedoRepo.GetAllAsync();
        var query = todosBrinquedos.Where(b => b.Ativo).AsEnumerable();

        if (!string.IsNullOrEmpty(termoBusca))
        {
            query = query.Where(b =>
                (b.Nome != null && b.Nome.Contains(termoBusca, StringComparison.OrdinalIgnoreCase)) ||
                (b.Marca != null && b.Marca.Nome.Contains(termoBusca, StringComparison.OrdinalIgnoreCase)) ||
                (b.Categoria != null && b.Categoria.Nome.Contains(termoBusca, StringComparison.OrdinalIgnoreCase))
            );
        }

        if (marcaId.HasValue) query = query.Where(b => b.MarcaId == marcaId.Value);
        if (categoriaId.HasValue) query = query.Where(b => b.CategoriaId == categoriaId.Value);
        if (subCategoriaId.HasValue) query = query.Where(b => b.SubCategoriaId == subCategoriaId.Value);
        if (faixaEtariaId.HasValue) query = query.Where(b => b.FaixaEtariaId == faixaEtariaId.Value);
        if (personagemId.HasValue) query = query.Where(b => b.PersonagemId == personagemId.Value);

        var listaCategorias = (await _categoriaRepo.GetAllAsync()).Select(c => new CategoriaDto { Id = c.Id, Nome = c.Nome }).ToList();
        var listaSubCategorias = new List<SubCategoriaDto>();

        if (categoriaId.HasValue)
        {
            listaSubCategorias = await _context.SubCategorias
                .Where(s => s.CategoriaId == categoriaId.Value)
                .Select(s => new SubCategoriaDto { Id = s.Id, Nome = s.Nome })
                .ToListAsync();
        }

        int pageSize = 12;
        int totalItems = query.Count();
        var itensPagina = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        var viewModel = new CatalogViewModel
        {
            Marcas = (await _marcaRepo.GetAllAsync()).Select(m => new MarcaDto { Id = m.Id, Nome = m.Nome }).ToList(),
            FaixasEtarias = (await _faixaRepo.GetAllAsync()).Select(f => new FaixaEtariaDto { Id = f.Id, Descricao = f.Descricao }).ToList(),
            Personagens = (await _personagemRepo.GetAllAsync()).Select(p => new PersonagemDto { Id = p.Id, Nome = p.Nome }).ToList(),
            Categorias = listaCategorias,
            SubCategorias = listaSubCategorias,

            TermoBusca = termoBusca,
            MarcaId = marcaId,
            CategoriaId = categoriaId,
            SubCategoriaId = subCategoriaId,
            FaixaEtariaId = faixaEtariaId,
            PersonagemId = personagemId,
            PageIndex = pageIndex,

            Produtos = new PagedResult<BrinquedoDto>
            {
                Items = itensPagina.Select(b => new BrinquedoDto
                {
                    Id = b.Id,
                    Nome = b.Nome,
                    Preco = b.Preco,
                    ImagemUrl = b.ImagemUrl,
                    Estoque = b.Estoque,
                    Ativo = b.Ativo,
                    Marca = b.Marca?.Nome ?? "",
                    Categoria = b.Categoria?.Nome ?? ""
                }).ToList(),
                TotalCount = totalItems,
                PageIndex = pageIndex,
                PageSize = pageSize
            }
        };

        return View("Busca", viewModel);
    }

    // =============================================================
    // 3. AUTOCOMPLETE
    // =============================================================
    [HttpGet]
    public async Task<IActionResult> BuscarSugestoes(string termo)
    {
        if (string.IsNullOrEmpty(termo) || termo.Length < 2) return Json(new List<string>());

        var query = await _context.Brinquedos
            .Include(b => b.Marca).Include(b => b.Categoria).Include(b => b.Personagem)
            .Where(b => b.Ativo && (
                b.Nome.Contains(termo) || b.Marca.Nome.Contains(termo) ||
                b.Categoria.Nome.Contains(termo) || b.Personagem.Nome.Contains(termo)))
            .Select(b => new { b.Nome, Marca = b.Marca.Nome, Categoria = b.Categoria.Nome })
            .Take(20).ToListAsync();

        var sugestoes = new List<string>();
        foreach (var item in query)
        {
            if (item.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase)) sugestoes.Add(item.Nome);
            if (item.Marca.Contains(termo, StringComparison.OrdinalIgnoreCase)) sugestoes.Add(item.Marca);
            if (item.Categoria.Contains(termo, StringComparison.OrdinalIgnoreCase)) sugestoes.Add(item.Categoria);
        }
        return Json(sugestoes.Distinct().Take(6));
    }

    // =============================================================
    // 4. DETALHES DO PRODUTO (PDP)
    // =============================================================
    [HttpGet]
    public async Task<IActionResult> Detalhes(int id)
    {
        var produto = await _context.Brinquedos
            .Include(b => b.Marca)
            .Include(b => b.Categoria)
            .Include(b => b.SubCategoria)
            .Include(b => b.FaixaEtaria)
            .Include(b => b.Personagem)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (produto == null) return NotFound();

        var relacionados = await _context.Brinquedos
            .Where(b => b.CategoriaId == produto.CategoriaId && b.Id != id && b.Ativo)
            .Take(4)
            .Select(b => new BrinquedoDto
            {
                Id = b.Id,
                Nome = b.Nome,
                Preco = b.Preco,
                ImagemUrl = b.ImagemUrl,
                Estoque = b.Estoque,
                Ativo = b.Ativo,
                Marca = b.Marca.Nome,
                Categoria = b.Categoria.Nome
            })
            .ToListAsync();

        // Dados de Feedback
        var comentarios = await _feedbackRepo.GetComentariosByBrinquedoIdAsync(id);
        var media = await _feedbackRepo.GetMediaAvaliacaoAsync(id);
        var qtd = await _feedbackRepo.GetQuantidadeAvaliacoesAsync(id);

        var viewModel = new ProdutoDetalhesViewModel
        {
            Produto = new BrinquedoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemUrl = produto.ImagemUrl,
                Estoque = produto.Estoque,
                Marca = produto.Marca?.Nome,
                Categoria = produto.Categoria?.Nome
            },
            Relacionados = relacionados,
            Comentarios = comentarios,
            MediaNota = media,
            TotalAvaliacoes = qtd
        };

        ViewBag.FaixaEtaria = produto.FaixaEtaria?.Descricao;
        ViewBag.Personagem = produto.Personagem?.Nome;
        ViewBag.SubCategoria = produto.SubCategoria?.Nome;

        return View(viewModel);
    }

    // =============================================================
    // 5. ENVIAR FEEDBACK
    // =============================================================
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> EnviarFeedback(int brinquedoId, int? nota, string? comentarioTexto)
    {
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");

        if (string.IsNullOrEmpty(usuarioIdClaim))
            return RedirectToAction("Login", "Account");

        int usuarioId = int.Parse(usuarioIdClaim);

        // 1. Salvar Avaliação
        if (nota.HasValue && nota > 0)
        {
            if (!await _feedbackRepo.UsuarioJaAvaliouAsync(usuarioId, brinquedoId))
            {
                await _feedbackRepo.AddAvaliacaoAsync(new Avaliacao
                {
                    BrinquedoId = brinquedoId,
                    UsuarioId = usuarioId,
                    Nota = nota.Value,
                    DataAvaliacao = DateTime.Now
                });
            }
        }

        // 2. Salvar Comentário
        if (!string.IsNullOrWhiteSpace(comentarioTexto))
        {
            await _feedbackRepo.AddComentarioAsync(new Comentario
            {
                BrinquedoId = brinquedoId,
                UsuarioId = usuarioId,
                Texto = comentarioTexto,
                DataComentario = DateTime.Now
            });
        }

        return RedirectToAction(nameof(Detalhes), new { id = brinquedoId });
    }

    // =============================================================
    // 6. EXCLUIR COMENTÁRIO (Pelo Próprio Usuário) - NOVO!
    // =============================================================
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ExcluirComentario(int comentarioId, int brinquedoId)
    {
        // 1. Identifica quem está logado
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");
        if (string.IsNullOrEmpty(usuarioIdClaim)) return RedirectToAction("Login", "Account");
        int usuarioId = int.Parse(usuarioIdClaim);

        // 2. Tenta excluir (O Repositório garante que só apaga se for dono)
        // Certifique-se de que você já adicionou DeleteComentarioPeloUsuarioAsync no FeedbackRepository
        await _feedbackRepo.DeleteComentarioPeloUsuarioAsync(comentarioId, usuarioId);

        // 3. Volta para a página do produto
        return RedirectToAction(nameof(Detalhes), new { id = brinquedoId });
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
