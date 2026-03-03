using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabricaDeSorrisos.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Administrador,ADMINISTRADOR")]

public class CategoriasController : Controller
{
    private readonly ICategoriaRepository _repository;

    // Injeção direta do Repositório (Padrão Saphira)
    public CategoriasController(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    // LISTAR
    public async Task<IActionResult> Index()
    {
        var lista = await _repository.GetAllAsync();
        return View(lista);
    }

    // CRIAR (Tela)
    public IActionResult Create()
    {
        return View(new FabricaDeSorrisos.Domain.Entities.Categoria());
    }

    // CRIAR (Salvar)
    [HttpPost]
    public async Task<IActionResult> Create(Categoria categoria)
    {
        if (!ModelState.IsValid) return View(categoria);

        await _repository.AddAsync(categoria);
        return RedirectToAction(nameof(Index));
    }

    // EDITAR (Tela)
    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        if (categoria == null) return NotFound();
        return View(categoria);
    }

    // EDITAR (Salvar)
    [HttpPost]
    public async Task<IActionResult> Edit(Categoria categoria)
    {
        if (!ModelState.IsValid) return View(categoria);

        await _repository.UpdateAsync(categoria);
        return RedirectToAction(nameof(Index));
    }

    // EXCLUIR (Tela de Confirmação)
    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        if (categoria == null) return NotFound();
        return View(categoria);
    }

    // EXCLUIR (Ação Real)
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        if (categoria != null)
        {
            await _repository.DeleteAsync(categoria);
        }
        return RedirectToAction(nameof(Index));
    }
}