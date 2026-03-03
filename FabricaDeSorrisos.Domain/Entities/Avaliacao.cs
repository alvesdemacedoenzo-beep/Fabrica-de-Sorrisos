namespace FabricaDeSorrisos.Domain.Entities;

public class Avaliacao
{
    public int Id { get; set; }
    public int Nota { get; set; } // 1 a 5
    public DateTime DataAvaliacao { get; set; } = DateTime.UtcNow;

    public int BrinquedoId { get; set; }
    public Brinquedo? Brinquedo { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}
