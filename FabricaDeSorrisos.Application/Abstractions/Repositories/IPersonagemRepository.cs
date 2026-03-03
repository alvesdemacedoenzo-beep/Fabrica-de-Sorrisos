using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface IPersonagemRepository
{
    Task<List<Personagem>> GetAllAsync();
    Task<Personagem?> GetByIdAsync(int id);
    Task AddAsync(Personagem personagem);
    Task UpdateAsync(Personagem personagem);
    Task DeleteAsync(Personagem personagem);
}
