namespace FabricaDeSorrisos.Domain.Entities;

public class Usuario

{

    public int Id { get; set; }

    public string NomeCompleto { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Este campo conecta este perfil ao Login do ASP.NET Identity (na Infra)

    public string? IdentityUserId { get; set; }

    public int TipoUsuarioId { get; set; }

    public TipoUsuario? TipoUsuario { get; set; }

    // Histórico do cliente

    public ICollection<Pedido>? Pedidos { get; set; }

    public int PontosFidelidade { get; set; } // Para o joguinho

    public ICollection<Favorito>? Favoritos { get; set; }
}
