using FabricaDeSorrisos.Application.DTOs;
using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Web.Models;

public class ProdutoDetalhesViewModel
{
    public BrinquedoDto Produto { get; set; }
    public List<BrinquedoDto> Relacionados { get; set; } = new();
    // --- NOVOS CAMPOS ---
    public List<Comentario> Comentarios { get; set; } = new();
    public double MediaNota { get; set; }
    public int TotalAvaliacoes { get; set; }
    // Propriedade para o formulário de novo comentário/avaliação
    public int? MinhaNota { get; set; }
    public string? MeuComentario { get; set; }
}
