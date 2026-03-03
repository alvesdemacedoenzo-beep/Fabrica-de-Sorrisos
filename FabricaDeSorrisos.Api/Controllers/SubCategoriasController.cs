using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubCategoriasController : ControllerBase
{
    private readonly AppDbContext _context;
    public SubCategoriasController(AppDbContext context) => _context = context;

    public class SubCategoriaRequest
    {
        public string Nome { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var list = await _context.SubCategorias
            .Select(s => new { s.Id, s.Nome, s.CategoriaId })
            .ToListAsync();
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SubCategoriaRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest("Nome é obrigatório");
        if (request.CategoriaId <= 0)
            return BadRequest("CategoriaId é obrigatório");

        var entity = new SubCategoria { Nome = request.Nome.Trim(), CategoriaId = request.CategoriaId };
        _context.SubCategorias.Add(entity);
        await _context.SaveChangesAsync();
        return Ok(new { entity.Id, entity.Nome, entity.CategoriaId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SubCategoriaRequest request)
    {
        var entity = await _context.SubCategorias.FindAsync(id);
        if (entity == null) return NotFound();
        if (!string.IsNullOrWhiteSpace(request.Nome))
            entity.Nome = request.Nome.Trim();
        await _context.SaveChangesAsync();
        return Ok(new { entity.Id, entity.Nome, entity.CategoriaId });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.SubCategorias.FindAsync(id);
        if (entity == null) return NotFound();
        _context.SubCategorias.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
