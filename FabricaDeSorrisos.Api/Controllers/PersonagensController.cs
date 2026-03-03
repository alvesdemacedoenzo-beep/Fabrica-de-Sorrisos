using FabricaDeSorrisos.Application.Abstractions.Services;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonagensController : ControllerBase
{
    private readonly ICatalogLookupService _service;
    private readonly AppDbContext _context;

    public PersonagensController(ICatalogLookupService service, AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetPersonagensAsync());
    }

    public class PersonagemRequest
    {
        public string Nome { get; set; } = string.Empty;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PersonagemRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest("Nome é obrigatório");
        var entity = new Personagem { Nome = request.Nome.Trim() };
        _context.Personagens.Add(entity);
        await _context.SaveChangesAsync();
        return Ok(new { entity.Id, entity.Nome });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PersonagemRequest request)
    {
        var entity = await _context.Personagens.FindAsync(id);
        if (entity == null) return NotFound();
        if (!string.IsNullOrWhiteSpace(request.Nome))
            entity.Nome = request.Nome.Trim();
        await _context.SaveChangesAsync();
        return Ok(new { entity.Id, entity.Nome });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Personagens.FindAsync(id);
        if (entity == null) return NotFound();
        _context.Personagens.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
