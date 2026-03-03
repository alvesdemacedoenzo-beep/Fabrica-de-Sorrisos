using FabricaDeSorrisos.Application.Abstractions.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabricaDeSorrisos.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Administrador,ADMINISTRADOR")]

public class FeedbackController : Controller
{
    private readonly IFeedbackRepository _repo;

    public FeedbackController(IFeedbackRepository repo)
    {
        _repo = repo;
    }

    // LISTAR TODOS
    public async Task<IActionResult> Index()
    {
        var comentarios = await _repo.GetAllComentariosAsync();
        return View(comentarios);
    }

    // RESPONDER (POST)
    [HttpPost]
    public async Task<IActionResult> Responder(int id, string respostaTexto)
    {
        if (!string.IsNullOrWhiteSpace(respostaTexto))
        {
            await _repo.ResponderComentarioAsync(id, respostaTexto);
        }
        return RedirectToAction(nameof(Index));
    }

    // EXCLUIR (POST)
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteComentarioAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
