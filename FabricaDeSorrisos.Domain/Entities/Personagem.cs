namespace FabricaDeSorrisos.Domain.Entities;

public class Personagem
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // Ex: Mickey, Batman
    public string? ImagemUrl { get; set; } // Foto do personagem para o menu

    public ICollection<Brinquedo>? Brinquedos { get; set; }
}
