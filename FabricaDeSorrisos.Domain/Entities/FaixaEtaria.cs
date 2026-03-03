namespace FabricaDeSorrisos.Domain.Entities;

public class FaixaEtaria
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty; // Ex: "3 a 5 anos"
    public ICollection<Brinquedo>? Brinquedos { get; set; }
}
