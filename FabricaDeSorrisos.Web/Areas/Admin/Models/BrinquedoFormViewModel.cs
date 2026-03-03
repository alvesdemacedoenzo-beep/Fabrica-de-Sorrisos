using System.ComponentModel.DataAnnotations;
using FabricaDeSorrisos.Domain.Entities; // Para usar as listas de entidades direto

namespace FabricaDeSorrisos.Web.Areas.Admin.Models;

public class BrinquedoFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória")]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 10000, ErrorMessage = "Preço inválido")]
    public decimal Preco { get; set; }

    [Required]
    public int Estoque { get; set; }

    public bool Ativo { get; set; } = true;

    // Upload
    public IFormFile? ImagemUpload { get; set; }
    public string? ImagemUrlAtual { get; set; }

    // Chaves Estrangeiras
    [Required(ErrorMessage = "Selecione a Marca")]
    public int MarcaId { get; set; }

    [Required(ErrorMessage = "Selecione a Categoria")]
    public int CategoriaId { get; set; }

    public int? SubCategoriaId { get; set; } // Será carregado via JavaScript depois (Fase avançada), por enquanto opcional


    [Required(ErrorMessage = "Selecione a Faixa Etária")]
    public int FaixaEtariaId { get; set; }

    public int? PersonagemId { get; set; }

    // Listas para os Selects (Preenchidas pelo Controller)
    public IEnumerable<Marca> Marcas { get; set; } = new List<Marca>();
    public IEnumerable<Categoria> Categorias { get; set; } = new List<Categoria>();
    public IEnumerable<FaixaEtaria> FaixasEtarias { get; set; } = new List<FaixaEtaria>();
    public IEnumerable<Personagem> Personagens { get; set; } = new List<Personagem>();
    // ADICIONE ESSA LINHA:
    public IEnumerable<SubCategoria> SubCategorias { get; set; } = new List<SubCategoria>();
}