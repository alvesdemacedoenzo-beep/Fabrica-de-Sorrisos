using FabricaDeSorrisos.Application.DTOs;
using FabricaDeSorrisos.Application.Filters;

namespace FabricaDeSorrisos.Application.Abstractions.Services;

public interface IBrinquedoQueryService
{
    // Busca principal do site (retorna paginação)
    Task<PagedResult<BrinquedoDto>> GetAllAsync(BrinquedoFilter filter);

    // Busca os detalhes para a página do produto (retorna DTO leve)
    Task<BrinquedoDto?> GetDetalhesByIdAsync(int id);

    // Sugestão da barra de pesquisa (autocomplete)
    Task<List<string>> GetSugestoesNomesAsync(string termo);
}
