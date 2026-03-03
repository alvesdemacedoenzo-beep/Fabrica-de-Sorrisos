using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using FabricaDeSorrisos.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Repositories;

public class SuporteRepository : ISuporteRepository
{
    private readonly AppDbContext _context;
    // O texto exato que define se o chat está fechado
    private const string TEXTO_ENCERRAMENTO = "Atendimento encerrado. Caso precise de mais ajuda, envie uma nova mensagem.";

    public SuporteRepository(AppDbContext context)
    {
        _context = context;
    }

    // --- MÉTODOS BÁSICOS ---
    public async Task<List<MensagemSuporte>> GetMensagensDoUsuarioAsync(int usuarioId)
    {
        return await _context.MensagensSuporte
            .Where(m => m.UsuarioId == usuarioId)
            .OrderBy(m => m.DataEnvio)
            .ToListAsync();
    }

    public async Task<List<int>> GetUsuariosComMensagensPendentesAsync()
    {
        return await _context.MensagensSuporte
            .Where(m => !m.RespondidoPorAdmin)
            .Select(m => m.UsuarioId)
            .Distinct()
            .ToListAsync();
    }

    public async Task SalvarMensagemAsync(MensagemSuporte mensagem)
    {
        _context.MensagensSuporte.Add(mensagem);
        await _context.SaveChangesAsync();
    }

    public async Task MarcarComoRespondidaAsync(int usuarioId)
    {
        await Task.CompletedTask;
    }

    // --- MÉTODOS AVANÇADOS (LÓGICA CORRIGIDA AQUI) ---
    public async Task<List<DtoConversa>> GetConversasPorStatusAsync(bool apenasPendentes)
    {
        // 1. Pega a última mensagem de cada usuário
        var ultimasMensagens = await _context.MensagensSuporte
            .GroupBy(m => m.UsuarioId)
            .Select(g => g.OrderByDescending(m => m.DataEnvio).First())
            .ToListAsync();

        // 2. FILTRO NOVO:
        // PENDENTES (Ativos) = A última mensagem NÃO É a de encerramento.
        // ENCERRADOS = A última mensagem É a de encerramento.
        var filtradas = ultimasMensagens
            .Where(m => apenasPendentes
                ? m.Texto != TEXTO_ENCERRAMENTO
                : m.Texto == TEXTO_ENCERRAMENTO)
            .ToList();

        if (!filtradas.Any()) return new List<DtoConversa>();

        // 3. Busca Nomes
        var userIds = filtradas.Select(m => m.UsuarioId).Distinct().ToList();
        var nomesUsuarios = await _context.UsuariosDoSistema
            .Where(u => userIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => u.NomeCompleto);

        // 4. Monta DTO
        var resultado = new List<DtoConversa>();

        foreach (var msg in filtradas)
        {
            string nomeExibicao = nomesUsuarios.ContainsKey(msg.UsuarioId)
                ? nomesUsuarios[msg.UsuarioId]
                : $"Cliente #{msg.UsuarioId}";

            resultado.Add(new DtoConversa
            {
                UsuarioId = msg.UsuarioId,
                Nome = nomeExibicao,
                UltimaMensagem = msg.Texto.Length > 40 ? msg.Texto.Substring(0, 40) + "..." : msg.Texto,
                Data = msg.DataEnvio,
                // Badge de não lida apenas se o Admin não respondeu a última
                TemMensagemNaoLida = !msg.RespondidoPorAdmin
            });
        }

        return resultado.OrderByDescending(x => x.Data).ToList();
    }

    public async Task<int> ContarMensagensNaoLidasClienteAsync(int usuarioId)
    {
        return await _context.MensagensSuporte
            .Where(m => m.UsuarioId == usuarioId && m.RespondidoPorAdmin)
            .CountAsync();
    }
}