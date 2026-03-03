namespace FabricaDeSorrisos.Application.DTOs;

public class CategoriaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    // Lista simples com os nomes das subcategorias
    public List<SubCategoriaDto> SubCategorias { get; set; } = new();
}

public class SubCategoriaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}
