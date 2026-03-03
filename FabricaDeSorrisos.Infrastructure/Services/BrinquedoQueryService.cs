using FabricaDeSorrisos.Application.Abstractions.Services;
using FabricaDeSorrisos.Application.DTOs;
using FabricaDeSorrisos.Application.Filters;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Services;

public class BrinquedoQueryService : IBrinquedoQueryService
{
    private readonly AppDbContext _context;

    public BrinquedoQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<BrinquedoDto>> GetAllAsync(BrinquedoFilter filter)
    {
        // 1. Começa a query base
        var query = _context.Brinquedos.AsNoTracking().AsQueryable();

        // Exibe apenas ativos por padrão; admins podem incluir inativos via filtro
        if (!filter.IncluirInativos)
        {
            query = query.Where(b => b.Ativo == true);
        }

        // 2. Aplica os filtros se eles existirem (Dinâmico)
        if (!string.IsNullOrWhiteSpace(filter.TermoBusca))
        {
            query = query.Where(b => b.Nome.Contains(filter.TermoBusca) ||
                                     b.Marca.Nome.Contains(filter.TermoBusca));
        }

        if (filter.MarcaId.HasValue) query = query.Where(b => b.MarcaId == filter.MarcaId);
        if (filter.CategoriaId.HasValue) query = query.Where(b => b.CategoriaId == filter.CategoriaId);
        if (filter.SubCategoriaId.HasValue) query = query.Where(b => b.SubCategoriaId == filter.SubCategoriaId);
        if (filter.PersonagemId.HasValue) query = query.Where(b => b.PersonagemId == filter.PersonagemId);
        if (filter.FaixaEtariaId.HasValue) query = query.Where(b => b.FaixaEtariaId == filter.FaixaEtariaId);

        // 3. Ordenação
        query = filter.SortBy switch
        {
            "Preco" => filter.IsDesc ? query.OrderByDescending(b => b.Preco) : query.OrderBy(b => b.Preco),
            _ => filter.IsDesc ? query.OrderByDescending(b => b.Nome) : query.OrderBy(b => b.Nome) // Default: Nome
        };

        // 4. Paginação (Pula X e Pega Y)
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((filter.PageIndex - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(b => new BrinquedoDto // Projeção direta para DTO (mais rápido)
            {
                Id = b.Id,
                Nome = b.Nome,
                Descricao = b.Descricao,
                Preco = b.Preco,
                ImagemUrl = b.ImagemUrl,
                Estoque = b.Estoque,
                Ativo = b.Ativo,
                Marca = b.Marca.Nome,
                Categoria = b.Categoria.Nome,
                SubCategoria = b.SubCategoria != null ? b.SubCategoria.Nome : "",
                Personagem = b.Personagem != null ? b.Personagem.Nome : null,
                FaixaEtaria = b.FaixaEtaria.Descricao,
                // MediaAvaliacoes = b.Avaliacoes.Any() ? b.Avaliacoes.Average(a => a.Nota) : 0
            })
            .ToListAsync();

        return new PagedResult<BrinquedoDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = filter.PageIndex,
            PageSize = filter.PageSize
        };
    }

    public async Task<BrinquedoDto?> GetDetalhesByIdAsync(int id)
    {
        // Busca um único item transformando em DTO
        return await _context.Brinquedos
            .Where(b => b.Id == id)
            .Select(b => new BrinquedoDto
            {
                Id = b.Id,
                Nome = b.Nome,
                Descricao = b.Descricao,
                Preco = b.Preco,
                ImagemUrl = b.ImagemUrl,
                Estoque = b.Estoque,
                Ativo = b.Ativo,
                Marca = b.Marca.Nome,
                Categoria = b.Categoria.Nome,
                SubCategoria = b.SubCategoria != null ? b.SubCategoria.Nome : "",
                Personagem = b.Personagem != null ? b.Personagem.Nome : null,
                FaixaEtaria = b.FaixaEtaria.Descricao
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<string>> GetSugestoesNomesAsync(string termo)
    {
        // Autocomplete simples
        return await _context.Brinquedos
            .AsNoTracking()
            .Where(b => b.Nome.Contains(termo))
            .Take(5)
            .Select(b => b.Nome)
            .ToListAsync();
    }
}
