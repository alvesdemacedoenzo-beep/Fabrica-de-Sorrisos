namespace FabricaDeSorrisos.Domain.Entities;

public class MensagemSuporte
{
    public int Id { get; set; }
    public string Texto { get; set; } = string.Empty;
    public DateTime DataEnvio { get; set; } = DateTime.UtcNow;

    public int UsuarioId { get; set; } // Quem mandou ou recebeu
    public Usuario? Usuario { get; set; }

    public bool RespondidoPorAdmin { get; set; } // Se true, foi o suporte que escreveu
}