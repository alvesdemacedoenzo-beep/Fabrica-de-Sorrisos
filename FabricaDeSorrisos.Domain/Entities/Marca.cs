namespace FabricaDeSorrisos.Domain.Entities;

public class Marca
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? LogotipoUrl { get; set; }
    public ICollection<Brinquedo>? Brinquedos { get; set; }
}