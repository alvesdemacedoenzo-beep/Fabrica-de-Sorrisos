using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class EfPersonagemRepository : IPersonagemRepository
{
    private readonly AppDbContext _context;

    public EfPersonagemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Personagem>> GetAllAsync()
    {
        // Trazemos a lista ordenada pelo nome para facilitar a exibição
        return await _context.Personagens
            .OrderBy(p => p.Nome)
            .ToListAsync();
    }

    public async Task<Personagem?> GetByIdAsync(int id)
    {
        return await _context.Personagens.FindAsync(id);
    }

    public async Task AddAsync(Personagem personagem)
    {
        _context.Personagens.Add(personagem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Personagem personagem)
    {
        _context.Personagens.Update(personagem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Personagem personagem)
    {
        _context.Personagens.Remove(personagem);
        await _context.SaveChangesAsync();
    }
}
