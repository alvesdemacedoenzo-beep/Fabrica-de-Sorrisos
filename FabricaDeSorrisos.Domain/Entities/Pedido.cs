using FabricaDeSorrisos.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; } // "Aguardando Pagamento", "Pago", etc.

    // NOVOS CAMPOS PARA O CHECKOUT
    public string? EnderecoEntrega { get; set; }
    public string? CEP { get; set; }
    public decimal ValorFrete { get; set; }
    public string? FormaPagamento { get; set; } // "Pix", "Cartao", "Boleto"

    // ADICIONE ESSA LINHA SE NÃO TIVER:
    public virtual Usuario Usuario { get; set; }

    public List<PedidoItem> Itens { get; set; } = new();
}
