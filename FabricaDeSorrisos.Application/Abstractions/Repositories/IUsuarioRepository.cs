using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface IUsuarioRepository
{
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task DeleteAsync(Usuario usuario);

    // NOVOS MÉTODOS PARA O CRUD
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task<List<TipoUsuario>> GetTiposUsuariosAsync(); // Para o Dropdown
}