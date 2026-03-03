using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class EfFavoritoRepository : IFavoritoRepository
{
    private readonly AppDbContext _context;

    public EfFavoritoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Favorito>> GetByUsuarioIdAsync(int usuarioId)
    {
        // ATUALIZADO: Agora filtramos também por Brinquedo.Ativo
        // Assim, se o admin desativar o brinquedo, ele some dos favoritos do cliente
        return await _context.Favoritos
            .Include(f => f.Brinquedo)
            .Where(f => f.UsuarioId == usuarioId && f.Brinquedo.Ativo)
            .OrderByDescending(f => f.DataFavoritado)
            .ToListAsync();
    }

    public async Task<Favorito?> GetByUsuarioEBrinquedoAsync(int usuarioId, int brinquedoId)
    {
        return await _context.Favoritos
            .FirstOrDefaultAsync(f => f.UsuarioId == usuarioId && f.BrinquedoId == brinquedoId);
    }

    public async Task AddAsync(Favorito favorito)
    {
        _context.Favoritos.Add(favorito);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Favorito favorito)
    {
        _context.Favoritos.Remove(favorito);
        await _context.SaveChangesAsync();
    }
}