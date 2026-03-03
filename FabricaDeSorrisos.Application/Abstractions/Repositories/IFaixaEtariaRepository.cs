using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface IFaixaEtariaRepository
{
    // Precisamos disso para preencher os Dropdowns e Filtros
    Task<List<FaixaEtaria>> GetAllAsync();

    // Precisamos disso para exibir detalhes, se necessário
    Task<FaixaEtaria?> GetByIdAsync(int id);
}