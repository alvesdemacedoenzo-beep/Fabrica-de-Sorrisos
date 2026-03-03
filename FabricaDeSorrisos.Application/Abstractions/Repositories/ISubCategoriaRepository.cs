using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface ISubCategoriaRepository
{
    Task<List<SubCategoria>> GetAllAsync();
    Task<SubCategoria?> GetByIdAsync(int id);
    Task<List<SubCategoria>> GetByCategoriaIdAsync(int categoriaId); // Útil para filtrar no futuro
    Task AddAsync(SubCategoria subCategoria);
    Task UpdateAsync(SubCategoria subCategoria);
    Task DeleteAsync(SubCategoria subCategoria);
}