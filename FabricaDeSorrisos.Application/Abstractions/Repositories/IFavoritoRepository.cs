using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface IFavoritoRepository
{
    // Pega todos os favoritos de um usuário
    Task<List<Favorito>> GetByUsuarioIdAsync(int usuarioId);

    // Verifica se já favoritou (para pintar o coraçãozinho)
    Task<Favorito?> GetByUsuarioEBrinquedoAsync(int usuarioId, int brinquedoId);

    Task AddAsync(Favorito favorito);
    Task DeleteAsync(Favorito favorito);
}
