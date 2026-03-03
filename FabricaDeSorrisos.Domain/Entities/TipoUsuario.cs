namespace FabricaDeSorrisos.Domain.Entities;

public class TipoUsuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty; // Admin, Gerente, Cliente
    public ICollection<Usuario>? Usuarios { get; set; }
}
