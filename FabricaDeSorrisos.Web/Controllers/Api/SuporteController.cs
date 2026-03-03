using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FabricaDeSorrisos.Web.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SuporteController : ControllerBase
{
    private readonly ISuporteRepository _repo;

    // ESTA FRASE DEVE SER IDÊNTICA À QUE O REPOSITÓRIO USA PARA FILTRAR
    private const string TEXTO_ENCERRAMENTO = "Atendimento encerrado. Caso precise de mais ajuda, envie uma nova mensagem.";

    public SuporteController(ISuporteRepository repo)
    {
        _repo = repo;
    }

    // --- NOVOS ENDPOINTS (ADMIN 2.0) ---

    // 1. LISTA DE CONVERSAS (PENDENTES OU ENCERRADAS)
    [Authorize(Roles = "Admin,Gerente")]
    [HttpGet("conversas")]
    public async Task<IActionResult> GetConversas([FromQuery] string status)
    {
        // Se status for "pendente", busca apenasPendentes = true (Chats ativos)
        // Se for "encerrado", busca apenasPendentes = false (Chats finalizados)
        bool apenasPendentes = (status == "pendente");

        var lista = await _repo.GetConversasPorStatusAsync(apenasPendentes);
        return Ok(lista);
    }

    // 2. ENCERRAR ATENDIMENTO
    [Authorize(Roles = "Admin,Gerente")]
    [HttpPost("encerrar")]
    public async Task<IActionResult> EncerrarAtendimento([FromBody] int usuarioId)
    {
        // Envia a mensagem padrão que o sistema usa para identificar o fim do chat
        var msgEncerramento = new MensagemSuporte
        {
            UsuarioId = usuarioId,
            Texto = TEXTO_ENCERRAMENTO, // Usa a constante para garantir o filtro
            DataEnvio = DateTime.UtcNow,
            RespondidoPorAdmin = true
        };

        await _repo.SalvarMensagemAsync(msgEncerramento);
        return Ok(new { success = true });
    }

    // --- ENDPOINTS CLIENTE ---

    // 3. NOTIFICAÇÕES (BADGE)
    [HttpGet("notificacoes")]
    public async Task<IActionResult> GetNotificacoes()
    {
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");
        if (string.IsNullOrEmpty(usuarioIdClaim)) return Ok(0);

        int count = await _repo.ContarMensagensNaoLidasClienteAsync(int.Parse(usuarioIdClaim));
        return Ok(count);
    }

    // --- MÉTODOS DE CHAT (MANTIDOS) ---

    // HISTÓRICO DE MENSAGENS
    [HttpGet("historico")]
    public async Task<IActionResult> GetHistorico(int? targetUserId)
    {
        // 1. Admin vendo chat de cliente específico
        if (targetUserId.HasValue && (User.IsInRole("Admin") || User.IsInRole("Gerente")))
        {
            var msgs = await _repo.GetMensagensDoUsuarioAsync(targetUserId.Value);
            return Ok(msgs);
        }

        // 2. Cliente vendo seu próprio chat
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");
        if (string.IsNullOrEmpty(usuarioIdClaim)) return Unauthorized();

        var mensagens = await _repo.GetMensagensDoUsuarioAsync(int.Parse(usuarioIdClaim));
        return Ok(mensagens);
    }

    // ENVIO DE MENSAGENS
    [HttpPost("enviar")]
    public async Task<IActionResult> Enviar([FromBody] DtoMensagem envio)
    {
        var usuarioIdClaim = User.FindFirstValue("UsuarioSistemaId");
        if (string.IsNullOrEmpty(usuarioIdClaim)) return Unauthorized();

        bool temPermissaoAdmin = User.IsInRole("Admin") || User.IsInRole("Gerente");

        // LÓGICA DE ENVIO INTELIGENTE:
        // Só conta como "Resposta de Admin" se vier do Painel (com DestinatarioId).
        // Se vier do Widget do site (DestinatarioId = 0), conta como mensagem de Cliente (Pendente),
        // mesmo que quem enviou seja um Admin fazendo testes.
        bool ehRespostaDoPainel = temPermissaoAdmin && envio.DestinatarioId > 0;

        var novaMsg = new MensagemSuporte
        {
            Texto = envio.Texto,
            DataEnvio = DateTime.UtcNow,
            // Se for resposta do painel, o ID é do cliente alvo. Se for do site, é o próprio ID logado.
            UsuarioId = ehRespostaDoPainel ? envio.DestinatarioId : int.Parse(usuarioIdClaim),

            // Define o status da mensagem
            RespondidoPorAdmin = ehRespostaDoPainel
        };

        await _repo.SalvarMensagemAsync(novaMsg);
        return Ok(new { success = true });
    }
}

public class DtoMensagem
{
    public string Texto { get; set; }
    public int DestinatarioId { get; set; }
}