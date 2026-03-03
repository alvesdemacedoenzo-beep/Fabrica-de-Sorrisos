namespace FabricaDeSorrisos.Application.DTOs;

public class BrinquedoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public int Estoque { get; set; }
    public bool Ativo { get; set; }

    // Dados "achatados" para exibição fácil
    public string Categoria { get; set; } = string.Empty;
    public string SubCategoria { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string FaixaEtaria { get; set; } = string.Empty;
    public string? Personagem { get; set; }
    public double MediaAvaliacoes { get; set; } // Ex: 4.5
}
