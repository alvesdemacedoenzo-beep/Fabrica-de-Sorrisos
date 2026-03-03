using FabricaDeSorrisos.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;

namespace FabricaDeSorrisos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICatalogLookupService _service;
    private readonly AppDbContext _context;

    public CategoriasController(ICatalogLookupService service, AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetCategoriasAsync();
        return Ok(result);
    }

    public class CategoriaRequest
    {
        public string Nome { get; set; } = string.Empty;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoriaRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest("Nome é obrigatório");

        var cat = new Categoria { Nome = request.Nome.Trim() };
        _context.Categorias.Add(cat);
        await _context.SaveChangesAsync();
        return Ok(new { cat.Id, cat.Nome });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoriaRequest request)
    {
        var cat = await _context.Categorias.FindAsync(id);
        if (cat == null) return NotFound();
        if (!string.IsNullOrWhiteSpace(request.Nome))
            cat.Nome = request.Nome.Trim();
        await _context.SaveChangesAsync();
        return Ok(new { cat.Id, cat.Nome });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cat = await _context.Categorias.FindAsync(id);
        if (cat == null) return NotFound();
        _context.Categorias.Remove(cat);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
