namespace FabricaDeSorrisos.Application.DTOs;

public class AvaliacaoDto
{
    public string UsuarioNome { get; set; } = string.Empty; // Mostra só o nome, não o ID
    public int Nota { get; set; } // 1 a 5
    public DateTime Data { get; set; }
}