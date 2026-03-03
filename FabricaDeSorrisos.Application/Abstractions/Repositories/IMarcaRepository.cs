using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface IMarcaRepository
{
    Task<List<Marca>> GetAllAsync();
    Task<Marca?> GetByIdAsync(int id);
    Task AddAsync(Marca marca);
    Task UpdateAsync(Marca marca);
    Task DeleteAsync(Marca marca);
}