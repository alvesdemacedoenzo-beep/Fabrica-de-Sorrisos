using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface IBrinquedoRepository
{
    // Métodos de Leitura
    Task<Brinquedo?> GetByIdAsync(int id);
    Task<List<Brinquedo>> GetAllAsync(); // Para listagens simples (ex: Admin)

    // Métodos de Escrita (CRUD)
    Task AddAsync(Brinquedo brinquedo);
    Task UpdateAsync(Brinquedo brinquedo);
    //Task DeleteAsync(Brinquedo brinquedo);

    // Verificações
    Task<bool> ExistsAsync(int id);
}
