namespace FabricaDeSorrisos.Domain.Entities;

public class Favorito
{
    public int Id { get; set; }
    public DateTime DataFavoritado { get; set; } = DateTime.UtcNow;

    // Qual brinquedo foi favoritado?
    public int BrinquedoId { get; set; }
    public Brinquedo? Brinquedo { get; set; }

    // Quem favoritou?
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}