using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class EfFaixaEtariaRepository : IFaixaEtariaRepository
{
    private readonly AppDbContext _context;

    public EfFaixaEtariaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FaixaEtaria>> GetAllAsync()
    {
        // Ordena por ID para aparecer na ordem certa (0-18 meses primeiro, etc)
        return await _context.FaixasEtarias
            .OrderBy(f => f.Id)
            .ToListAsync();
    }

    public async Task<FaixaEtaria?> GetByIdAsync(int id)
    {
        return await _context.FaixasEtarias.FindAsync(id);
    }
}