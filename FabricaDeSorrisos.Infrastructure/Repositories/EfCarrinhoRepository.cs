using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class EfCarrinhoRepository : ICarrinhoRepository
{
    private readonly AppDbContext _context;

    public EfCarrinhoRepository(AppDbContext context) => _context = context;

    public async Task<List<CarrinhoItem>> GetCarrinhoDoUsuarioAsync(int usuarioId)
    {
        return await _context.CarrinhoItens
            .Include(c => c.Brinquedo) // Traz os dados do produto junto
            .Where(c => c.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<CarrinhoItem?> GetItemAsync(int usuarioId, int brinquedoId)
    {
        return await _context.CarrinhoItens
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId && c.BrinquedoId == brinquedoId);
    }

    public async Task AddAsync(CarrinhoItem item)
    {
        _context.CarrinhoItens.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CarrinhoItem item)
    {
        _context.CarrinhoItens.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CarrinhoItem item)
    {
        _context.CarrinhoItens.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task LimparCarrinhoAsync(int usuarioId)
    {
        var itens = await _context.CarrinhoItens.Where(c => c.UsuarioId == usuarioId).ToListAsync();
        _context.CarrinhoItens.RemoveRange(itens);
        await _context.SaveChangesAsync();
    }
}
