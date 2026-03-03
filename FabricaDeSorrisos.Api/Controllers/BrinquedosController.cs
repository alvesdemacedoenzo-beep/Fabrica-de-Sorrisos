using FabricaDeSorrisos.Application.Abstractions.Services;
using FabricaDeSorrisos.Application.Filters;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace FabricaDeSorrisos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrinquedosController : ControllerBase
{
    private readonly IBrinquedoQueryService _queryService;
    private readonly AppDbContext _context;

    public BrinquedosController(IBrinquedoQueryService queryService, AppDbContext context)
    {
        _queryService = queryService;
        _context = context;
    }

    // GET: api/brinquedos?pageIndex=1&termoBusca=lego&marcaId=2...
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BrinquedoFilter filter)
    {
        var result = await _queryService.GetAllAsync(filter);
        return Ok(result);
    }

    // GET: api/brinquedos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var brinquedo = await _queryService.GetDetalhesByIdAsync(id);
        if (brinquedo == null) return NotFound();
        return Ok(brinquedo);
    }

    // GET: api/brinquedos/sugestoes
    [HttpGet("sugestoes")]
    public async Task<IActionResult> GetSugestoes([FromQuery] string termo)
    {
        if (string.IsNullOrWhiteSpace(termo) || termo.Length < 2)
            return Ok(new List<string>());

        var sugestoes = await _queryService.GetSugestoesNomesAsync(termo);
        return Ok(sugestoes);
    }

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrinquedoRequest request)
    {
        if (request == null) return BadRequest();

        var brinquedo = new Brinquedo
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            Preco = request.Preco,
            Estoque = request.Estoque,
            MarcaId = request.MarcaId,
            CategoriaId = request.CategoriaId,
            FaixaEtariaId = request.FaixaEtariaId,
            PersonagemId = request.PersonagemId,
            Ativo = request.Ativo
        };

        if (!string.IsNullOrWhiteSpace(request.ImagemBase64) && !string.IsNullOrWhiteSpace(request.ImagemNomeArquivo))
        {
            var relPath = SaveImageToWebRoot(request.ImagemBase64, request.ImagemNomeArquivo);
            if (relPath != null) brinquedo.ImagemUrl = relPath;
        }

        _context.Brinquedos.Add(brinquedo);
        await _context.SaveChangesAsync();
        return Ok(new { brinquedo.Id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBrinquedoRequest request)
    {
        var brinquedo = await _context.Brinquedos.FindAsync(id);
        if (brinquedo == null) return NotFound();

        if (request.Nome != null) brinquedo.Nome = request.Nome;
        if (request.Descricao != null) brinquedo.Descricao = request.Descricao;
        if (request.Preco.HasValue) brinquedo.Preco = request.Preco.Value;
        if (request.Estoque.HasValue) brinquedo.Estoque = request.Estoque.Value;
        if (request.Ativo.HasValue) brinquedo.Ativo = request.Ativo.Value;
        if (request.MarcaId.HasValue) brinquedo.MarcaId = request.MarcaId.Value;
        if (request.CategoriaId.HasValue) brinquedo.CategoriaId = request.CategoriaId.Value;
        if (request.FaixaEtariaId.HasValue) brinquedo.FaixaEtariaId = request.FaixaEtariaId.Value;
        if (request.PersonagemId.HasValue) brinquedo.PersonagemId = request.PersonagemId.Value;
        if (request.SubCategoriaId.HasValue) brinquedo.SubCategoriaId = request.SubCategoriaId.Value;

        if (!string.IsNullOrWhiteSpace(request.ImagemBase64) && !string.IsNullOrWhiteSpace(request.ImagemNomeArquivo))
        {
            var relPath = SaveImageToWebRoot(request.ImagemBase64, request.ImagemNomeArquivo);
            if (relPath != null) brinquedo.ImagemUrl = relPath;
        }

        await _context.SaveChangesAsync();
        return Ok(new { brinquedo.Id });
    }

    [HttpPost("{id}/ativar")]
    public async Task<IActionResult> Ativar(int id)
    {
        var brinquedo = await _context.Brinquedos.FindAsync(id);
        if (brinquedo == null) return NotFound();
        brinquedo.Ativo = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id}/desativar")]
    public async Task<IActionResult> Desativar(int id)
    {
        var brinquedo = await _context.Brinquedos.FindAsync(id);
        if (brinquedo == null) return NotFound();
        brinquedo.Ativo = false;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private string? SaveImageToWebRoot(string base64, string originalFileName)
    {
        try
        {
            var webRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "FabricaDeSorrisos.Web", "wwwroot"));
            var destDir = Path.Combine(webRoot, "imagens", "produtos");
            Directory.CreateDirectory(destDir);

            var ext = Path.GetExtension(originalFileName);
            if (string.IsNullOrWhiteSpace(ext)) ext = ".jpg";
            var safeExt = ext.ToLowerInvariant();
            var fileName = $"{Guid.NewGuid():N}{safeExt}";
            var destPath = Path.Combine(destDir, fileName);

            var bytes = Convert.FromBase64String(base64);
            System.IO.File.WriteAllBytes(destPath, bytes);

            return "/imagens/produtos/" + fileName;
        }
        catch
        {
            return null;
        }
    }
}
