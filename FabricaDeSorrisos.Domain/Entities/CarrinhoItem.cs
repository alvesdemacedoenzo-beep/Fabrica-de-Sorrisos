namespace FabricaDeSorrisos.Domain.Entities;

public class CarrinhoItem
{
    public int Id { get; set; }
    public int BrinquedoId { get; set; }
    public Brinquedo? Brinquedo { get; set; }

    public int UsuarioId { get; set; } // Dono do carrinho
    public Usuario? Usuario { get; set; }

    public int Quantidade { get; set; }
}
