using FabricaDeSorrisos.Application.DTOs;

namespace FabricaDeSorrisos.Application.Abstractions.Services;

public interface ICatalogLookupService
{
    Task<List<MarcaDto>> GetMarcasAsync();
    Task<List<CategoriaDto>> GetCategoriasAsync(); // Já traz subcategorias dentro
    Task<List<PersonagemDto>> GetPersonagensAsync();
    Task<List<FaixaEtariaDto>> GetFaixasEtariasAsync();
}
