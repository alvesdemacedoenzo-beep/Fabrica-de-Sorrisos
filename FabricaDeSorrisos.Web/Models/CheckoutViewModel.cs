using FabricaDeSorrisos.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FabricaDeSorrisos.Web.Models;

public class CheckoutViewModel
{
    public List<CarrinhoItem> Itens { get; set; } = new();
    public decimal Subtotal => Itens.Sum(i => i.Quantidade * i.Brinquedo.Preco);
    public decimal ValorFrete { get; set; }
    public string FormaPagamento { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório")]
    public string CEP { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório")]
    public string CPF { get; set; } // NOVO

    // Vamos quebrar o endereço para preencher automático
    [Required(ErrorMessage = "A Rua é obrigatória")]
    public string Logradouro { get; set; } // Rua

    [Required(ErrorMessage = "O Número é obrigatório")]
    public string Numero { get; set; }

    [Required(ErrorMessage = "O Bairro é obrigatório")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "A Cidade é obrigatória")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "O Estado é obrigatório")]
    public string UF { get; set; }

    public string? Complemento { get; set; }
}