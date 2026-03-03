namespace FabricaDeSorrisos.Application.DTOs;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}

public class PagingParams
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 12; // Padrão 12 brinquedos por página
}