using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface IFeedbackRepository
{
    // Comentários
    Task AddComentarioAsync(Comentario comentario);
    Task<List<Comentario>> GetComentariosByBrinquedoIdAsync(int brinquedoId);

    // Avaliações
    Task AddAvaliacaoAsync(Avaliacao avaliacao);
    Task<double> GetMediaAvaliacaoAsync(int brinquedoId);
    Task<int> GetQuantidadeAvaliacoesAsync(int brinquedoId);

    // Verifica se usuário já avaliou este produto (para não avaliar 2x)
    Task<bool> UsuarioJaAvaliouAsync(int usuarioId, int brinquedoId);

    // Adicione estes métodos novos no final da interface:
    Task<List<Comentario>> GetAllComentariosAsync();
    Task DeleteComentarioAsync(int id);
    Task ResponderComentarioAsync(int id, string resposta);

    Task<bool> DeleteComentarioPeloUsuarioAsync(int comentarioId, int usuarioId);
}
