using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Necessário para SelectList

namespace FabricaDeSorrisos.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Administrador,ADMINISTRADOR")]
public class SubCategoriasController : Controller
{
    private readonly ISubCategoriaRepository _repository;
    private readonly ICategoriaRepository _categoriaRepo; // Injetado para carregar o Dropdown

    public SubCategoriasController(ISubCategoriaRepository repository, ICategoriaRepository categoriaRepo)
    {
        _repository = repository;
        _categoriaRepo = categoriaRepo;
    }

    // LISTAR
    public async Task<IActionResult> Index()
    {
        var lista = await _repository.GetAllAsync();
        return View(lista);
    }

    // CRIAR (Tela)
    public async Task<IActionResult> Create()
    {
        await CarregarCategorias(); // Preenche a ViewBag com as categorias
        return View();
    }

    // CRIAR (Salvar)
    [HttpPost]
    public async Task<IActionResult> Create(SubCategoria subCategoria)
    {
        if (!ModelState.IsValid)
        {
            await CarregarCategorias();
            return View(subCategoria);
        }

        await _repository.AddAsync(subCategoria);
        return RedirectToAction(nameof(Index));
    }

    // EDITAR (Tela)
    public async Task<IActionResult> Edit(int id)
    {
        var subCategoria = await _repository.GetByIdAsync(id);
        if (subCategoria == null) return NotFound();

        await CarregarCategorias();
        return View(subCategoria);
    }

    // EDITAR (Salvar)
    [HttpPost]
    public async Task<IActionResult> Edit(SubCategoria subCategoria)
    {
        if (!ModelState.IsValid)
        {
            await CarregarCategorias();
            return View(subCategoria);
        }

        await _repository.UpdateAsync(subCategoria);
        return RedirectToAction(nameof(Index));
    }

    // EXCLUIR
    public async Task<IActionResult> Delete(int id)
    {
        var subCategoria = await _repository.GetByIdAsync(id);
        if (subCategoria == null) return NotFound();
        return View(subCategoria);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var subCategoria = await _repository.GetByIdAsync(id);
        if (subCategoria != null)
        {
            await _repository.DeleteAsync(subCategoria);
        }
        return RedirectToAction(nameof(Index));
    }

    // Método Auxiliar para preencher o Dropdown (Select)
    private async Task CarregarCategorias()
    {
        var categorias = await _categoriaRepo.GetAllAsync();
        ViewBag.CategoriaId = new SelectList(categorias, "Id", "Nome");
    }
}