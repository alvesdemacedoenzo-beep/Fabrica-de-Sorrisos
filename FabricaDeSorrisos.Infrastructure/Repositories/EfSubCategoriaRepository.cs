using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class EfSubCategoriaRepository : ISubCategoriaRepository
{
    private readonly AppDbContext _context;
    public EfSubCategoriaRepository(AppDbContext context) => _context = context;

    public async Task<List<SubCategoria>> GetAllAsync()
    {
        // Traz o nome da Categoria pai para mostrar na tabela (Ex: Bonecas > Barbie)
        return await _context.SubCategorias
            .Include(s => s.Categoria)
            .OrderBy(s => s.Categoria.Nome).ThenBy(s => s.Nome)
            .ToListAsync();
    }

    public async Task<SubCategoria?> GetByIdAsync(int id)
    {
        return await _context.SubCategorias
            .Include(s => s.Categoria)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<SubCategoria>> GetByCategoriaIdAsync(int categoriaId)
    {
        return await _context.SubCategorias
            .Where(s => s.CategoriaId == categoriaId)
            .OrderBy(s => s.Nome)
            .ToListAsync();
    }

    public async Task AddAsync(SubCategoria subCategoria)
    {
        _context.SubCategorias.Add(subCategoria);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SubCategoria subCategoria)
    {
        _context.SubCategorias.Update(subCategoria);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SubCategoria subCategoria)
    {
        _context.SubCategorias.Remove(subCategoria);
        await _context.SaveChangesAsync();
    }
}