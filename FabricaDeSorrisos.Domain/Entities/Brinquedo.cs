namespace FabricaDeSorrisos.Domain.Entities;

public class Brinquedo
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public int Estoque { get; set; }
    public bool Ativo { get; set; } = true;

    // Chaves Estrangeiras
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public int MarcaId { get; set; }
    public Marca? Marca { get; set; }

    public int FaixaEtariaId { get; set; }
    public FaixaEtaria? FaixaEtaria { get; set; }


    // Adicione essas propriedades no Brinquedo.cs:

    // Personagem (Opcional)
    public int? PersonagemId { get; set; }
    public Personagem? Personagem { get; set; }

    // SubCategoria (Opcional - o brinquedo já tem a Categoria principal obrigatória)
    public int? SubCategoriaId { get; set; }
    public SubCategoria? SubCategoria { get; set; }


    // Listas para funcionalidades futuras
    public ICollection<Avaliacao>? Avaliacoes { get; set; }
    public ICollection<Comentario>? Comentarios { get; set; }

    // Dentro da classe Brinquedo, adicione:
    public ICollection<Favorito>? FavoritadosPor { get; set; }
}