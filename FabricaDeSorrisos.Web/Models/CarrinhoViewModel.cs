using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Web.Models;

public class CarrinhoViewModel
{
    public List<CarrinhoItem> Itens { get; set; } = new();

    // Calcula o total automaticamente
    public decimal Total => Itens.Sum(i => i.Quantidade * i.Brinquedo.Preco);
}
