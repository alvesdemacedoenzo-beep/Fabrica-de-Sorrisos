using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class EfBrinquedoRepository : IBrinquedoRepository
{
    private readonly AppDbContext _context;

    public EfBrinquedoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Brinquedo?> GetByIdAsync(int id)
    {
        return await _context.Brinquedos
            .Include(b => b.Marca)
            .Include(b => b.Categoria)
            .Include(b => b.SubCategoria)
            .Include(b => b.Personagem)
            .Include(b => b.FaixaEtaria)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Brinquedo>> GetAllAsync()
    {
        return await _context.Brinquedos
            .Include(b => b.Marca)
            .Include(b => b.Categoria)
            .AsNoTracking() // Mais rápido para leitura
            .ToListAsync();
    }

    public async Task AddAsync(Brinquedo brinquedo)
    {
        _context.Brinquedos.Add(brinquedo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Brinquedo brinquedo)
    {
        _context.Brinquedos.Update(brinquedo);
        await _context.SaveChangesAsync();
    }

    //public async Task DeleteAsync(Brinquedo brinquedo)
    //{
    //    _context.Brinquedos.Remove(brinquedo);
    //    await _context.SaveChangesAsync();
    //}

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Brinquedos.AnyAsync(b => b.Id == id);
    }
}
