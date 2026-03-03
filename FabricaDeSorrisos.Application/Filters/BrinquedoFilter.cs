using FabricaDeSorrisos.Application.DTOs;

namespace FabricaDeSorrisos.Application.Filters;

public class BrinquedoFilter : PagingParams
{
    // O que o usuário digita na barra de busca (Nome ou Marca)
    public string? TermoBusca { get; set; }

    // Filtros de IDs (para os checkboxes e menus)
    public int? MarcaId { get; set; }
    public int? CategoriaId { get; set; }
    public int? SubCategoriaId { get; set; }
    public int? PersonagemId { get; set; }
    public int? FaixaEtariaId { get; set; }

    // Ordenação (Preço, Nome, Recentes)
    public string SortBy { get; set; } = "Nome"; // Padrão
    public bool IsDesc { get; set; } = false;

    // Administração: incluir também itens inativos
    public bool IncluirInativos { get; set; } = false;
}
