using FabricaDeSorrisos.Application.Abstractions.Services;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace FabricaDeSorrisos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasController : ControllerBase
{
    private readonly ICatalogLookupService _service;
    private readonly AppDbContext _context;

    public MarcasController(ICatalogLookupService service, AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetMarcasAsync();
        return Ok(result);
    }

    public class MarcaRequest
    {
        public string Nome { get; set; } = string.Empty;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MarcaRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest("Nome é obrigatório");

        var marca = new Marca { Nome = request.Nome.Trim() };
        _context.Marcas.Add(marca);
        await _context.SaveChangesAsync();
        return Ok(new { marca.Id, marca.Nome });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MarcaRequest request)
    {
        var marca = await _context.Marcas.FindAsync(id);
        if (marca == null) return NotFound();
        if (!string.IsNullOrWhiteSpace(request.Nome))
            marca.Nome = request.Nome.Trim();
        await _context.SaveChangesAsync();
        return Ok(new { marca.Id, marca.Nome });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var marca = await _context.Marcas.FindAsync(id);
        if (marca == null) return NotFound();
        _context.Marcas.Remove(marca);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
