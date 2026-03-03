namespace FabricaDeSorrisos.Domain.Entities;

public class SubCategoria
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // Ex: Action Figures, Quebra-Cabeças

    // Quem é o pai dela? (Ex: A Sub "Action Figure" pertence à Categoria "Bonecos")
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public ICollection<Brinquedo>? Brinquedos { get; set; }
}
