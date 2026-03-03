using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class EfFeedbackRepository : IFeedbackRepository
{
    private readonly AppDbContext _context;

    public EfFeedbackRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddComentarioAsync(Comentario comentario)
    {
        _context.Comentarios.Add(comentario);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Comentario>> GetComentariosByBrinquedoIdAsync(int brinquedoId)
    {
        return await _context.Comentarios
            .Include(c => c.Usuario) // Traz o nome do usuário
            .Where(c => c.BrinquedoId == brinquedoId)
            .OrderByDescending(c => c.DataComentario)
            .ToListAsync();
    }

    public async Task AddAvaliacaoAsync(Avaliacao avaliacao)
    {
        _context.Avaliacoes.Add(avaliacao);
        await _context.SaveChangesAsync();
    }

    public async Task<double> GetMediaAvaliacaoAsync(int brinquedoId)
    {
        var notas = await _context.Avaliacoes
            .Where(a => a.BrinquedoId == brinquedoId)
            .Select(a => a.Nota)
            .ToListAsync();

        if (!notas.Any()) return 0;
        return notas.Average();
    }

    public async Task<int> GetQuantidadeAvaliacoesAsync(int brinquedoId)
    {
        return await _context.Avaliacoes.CountAsync(a => a.BrinquedoId == brinquedoId);
    }

    public async Task<bool> UsuarioJaAvaliouAsync(int usuarioId, int brinquedoId)
    {
        return await _context.Avaliacoes.AnyAsync(a => a.UsuarioId == usuarioId && a.BrinquedoId == brinquedoId);
    }

    // Implemente os métodos novos:

    public async Task<List<Comentario>> GetAllComentariosAsync()
    {
        return await _context.Comentarios
            .Include(c => c.Usuario)
            .Include(c => c.Brinquedo) // Importante para saber de qual produto é
            .OrderByDescending(c => c.DataComentario)
            .ToListAsync();
    }

    public async Task DeleteComentarioAsync(int id)
    {
        var comentario = await _context.Comentarios.FindAsync(id);
        if (comentario != null)
        {
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ResponderComentarioAsync(int id, string resposta)
    {
        var comentario = await _context.Comentarios.FindAsync(id);
        if (comentario != null)
        {
            comentario.Resposta = resposta;
            comentario.DataResposta = DateTime.Now;
            _context.Comentarios.Update(comentario);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<bool> DeleteComentarioPeloUsuarioAsync(int comentarioId, int usuarioId)
    {
        // Busca o comentário
        var comentario = await _context.Comentarios.FindAsync(comentarioId);

        // Verifica se existe E se o dono é o mesmo usuário que está logado
        if (comentario != null && comentario.UsuarioId == usuarioId)
        {
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
            return true;
        }

        // Se não for o dono, não faz nada e retorna falso
        return false;
    }

}