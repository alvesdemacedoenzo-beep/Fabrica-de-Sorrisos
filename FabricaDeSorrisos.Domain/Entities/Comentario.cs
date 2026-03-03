namespace FabricaDeSorrisos.Domain.Entities;

public class Comentario
{
    public int Id { get; set; }
    public string Texto { get; set; } = string.Empty;
    public DateTime DataComentario { get; set; } = DateTime.UtcNow;

    // --- NOVOS CAMPOS PARA O ADMIN ---
    public string? Resposta { get; set; } // O texto da resposta do Admin
    public DateTime? DataResposta { get; set; }

    public int BrinquedoId { get; set; }
    public Brinquedo? Brinquedo { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}
