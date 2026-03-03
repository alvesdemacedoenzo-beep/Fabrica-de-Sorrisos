using FabricaDeSorrisos.Application.Abstractions.Services;
using FabricaDeSorrisos.Application.DTOs;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Services;

public class CatalogLookupService : ICatalogLookupService
{
    private readonly AppDbContext _context;

    public CatalogLookupService(AppDbContext context) => _context = context;

    public async Task<List<CategoriaDto>> GetCategoriasAsync()
    {
        return await _context.Categorias
            .Include(c => c.SubCategorias)
            .Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nome = c.Nome,
                SubCategorias = c.SubCategorias.Select(s => new SubCategoriaDto { Id = s.Id, Nome = s.Nome }).ToList()
            })
            .ToListAsync();
    }

    public async Task<List<MarcaDto>> GetMarcasAsync()
    {
        return await _context.Marcas
            .Select(m => new MarcaDto { Id = m.Id, Nome = m.Nome, LogotipoUrl = m.LogotipoUrl })
            .ToListAsync();
    }

    public async Task<List<PersonagemDto>> GetPersonagensAsync()
    {
        return await _context.Personagens
            .Select(p => new PersonagemDto { Id = p.Id, Nome = p.Nome, ImagemUrl = p.ImagemUrl })
            .ToListAsync();
    }

    public async Task<List<FaixaEtariaDto>> GetFaixasEtariasAsync()
    {
        return await _context.FaixasEtarias
            .Select(f => new FaixaEtariaDto { Id = f.Id, Descricao = f.Descricao })
            .ToListAsync();
    }
}