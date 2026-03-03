namespace FabricaDeSorrisos.Domain.Entities;

public class PedidoItem
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido? Pedido { get; set; }

    public int BrinquedoId { get; set; }
    public Brinquedo? Brinquedo { get; set; }

    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}
