using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class EfCategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public EfCategoriaRepository(AppDbContext context) => _context = context;

    public async Task<List<Categoria>> GetAllAsync()
    {
        return await _context.Categorias
            .Include(c => c.SubCategorias) // Importante!
            .ToListAsync();
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        return await _context.Categorias
            .Include(c => c.SubCategorias)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Categoria categoria)
    {
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
    }
}
