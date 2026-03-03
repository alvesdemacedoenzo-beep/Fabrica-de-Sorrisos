using FabricaDeSorrisos.Application.DTOs; // <--- ADICIONADO PARA ENCONTRAR OS DTOs

namespace FabricaDeSorrisos.Web.Models;

public class CatalogViewModel
{
    // A lista de produtos da página atual
    public PagedResult<BrinquedoDto>? Produtos { get; set; }

    // Listas para preencher os selects/checkboxes do filtro lateral
    public List<MarcaDto> Marcas { get; set; } = new();
    public List<CategoriaDto> Categorias { get; set; } = new();
    public List<FaixaEtariaDto> FaixasEtarias { get; set; } = new();
    public List<PersonagemDto> Personagens { get; set; } = new();
    // ADICIONE ESTA LINHA:
    public List<SubCategoriaDto> SubCategorias { get; set; } = new();

    // O que o usuário selecionou (para manter marcado na tela)
    public string? TermoBusca { get; set; }
    public int? MarcaId { get; set; }
    public int? CategoriaId { get; set; }
    public int? SubCategoriaId { get; set; }
    public int? FaixaEtariaId { get; set; }
    public int? PersonagemId { get; set; }
    public int PageIndex { get; set; } = 1;
}