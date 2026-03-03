using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class EfMarcaRepository : IMarcaRepository
{
    private readonly AppDbContext _context;
    public EfMarcaRepository(AppDbContext context) => _context = context;

    public async Task<List<Marca>> GetAllAsync() => await _context.Marcas.ToListAsync();
    public async Task<Marca?> GetByIdAsync(int id) => await _context.Marcas.FindAsync(id);

    public async Task AddAsync(Marca marca) { _context.Marcas.Add(marca); await _context.SaveChangesAsync(); }
    public async Task UpdateAsync(Marca marca) { _context.Marcas.Update(marca); await _context.SaveChangesAsync(); }
    public async Task DeleteAsync(Marca marca) { _context.Marcas.Remove(marca); await _context.SaveChangesAsync(); }
}
