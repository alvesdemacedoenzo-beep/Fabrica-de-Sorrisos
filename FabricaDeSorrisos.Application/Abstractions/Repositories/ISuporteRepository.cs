using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Application.DTOs; // <--- Importante para reconhecer o DTO

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface ISuporteRepository
{
    // Métodos Básicos (Chat Cliente)
    Task<List<MensagemSuporte>> GetMensagensDoUsuarioAsync(int usuarioId);
    Task SalvarMensagemAsync(MensagemSuporte mensagem);

    // Métodos Admin (Listagem e Notificações)
    Task<List<int>> GetUsuariosComMensagensPendentesAsync(); // Mantido para compatibilidade

    // NOVO: Busca conversas completas com Nome e Status (Pendente/Encerrado)
    Task<List<DtoConversa>> GetConversasPorStatusAsync(bool apenasPendentes);

    // NOVO: Conta mensagens não lidas para o badge do cliente
    Task<int> ContarMensagensNaoLidasClienteAsync(int usuarioId);

    Task MarcarComoRespondidaAsync(int usuarioId);
}