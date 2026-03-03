using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabricaDeSorrisos.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Administrador,ADMINISTRADOR")]

public class PersonagensController : Controller
{
    private readonly IPersonagemRepository _repository;
    private readonly IWebHostEnvironment _env; // Para salvar a imagem na pasta

    public PersonagensController(IPersonagemRepository repository, IWebHostEnvironment env)
    {
        _repository = repository;
        _env = env;
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
        return View(new FabricaDeSorrisos.Domain.Entities.Personagem());
    }

    // CRIAR (Salvar)
    [HttpPost]
    public async Task<IActionResult> Create(Personagem personagem, IFormFile? arquivo)
    {
        if (!ModelState.IsValid) return View(personagem);

        if (arquivo != null)
        {
            personagem.ImagemUrl = await SalvarArquivo(arquivo);
        }

        await _repository.AddAsync(personagem);
        return RedirectToAction(nameof(Index));
    }

    // EDITAR (Tela)
    public async Task<IActionResult> Edit(int id)
    {
        var personagem = await _repository.GetByIdAsync(id);
        if (personagem == null) return NotFound();
        return View(personagem);
    }

    // EDITAR (Salvar)
    [HttpPost]
    public async Task<IActionResult> Edit(Personagem personagem, IFormFile? arquivo)
    {
        // Busca o original para não perder a foto antiga se não enviar nova
        var original = await _repository.GetByIdAsync(personagem.Id);
        if (original == null) return NotFound();

        original.Nome = personagem.Nome;

        if (arquivo != null)
        {
            original.ImagemUrl = await SalvarArquivo(arquivo);
        }

        await _repository.UpdateAsync(original);
        return RedirectToAction(nameof(Index));
    }

    // EXCLUIR (Tela)
    public async Task<IActionResult> Delete(int id)
    {
        var personagem = await _repository.GetByIdAsync(id);
        if (personagem == null) return NotFound();
        return View(personagem);
    }

    // EXCLUIR (Confirmação)
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var personagem = await _repository.GetByIdAsync(id);
        if (personagem != null)
        {
            await _repository.DeleteAsync(personagem);
        }
        return RedirectToAction(nameof(Index));
    }

    // Método Auxiliar de Upload (Salva em wwwroot/imagens/personagens)
    private async Task<string> SalvarArquivo(IFormFile arquivo)
    {
        var nomeArquivo = Guid.NewGuid() + "_" + arquivo.FileName;
        var pastaDestino = Path.Combine(_env.WebRootPath, "imagens", "personagens");

        if (!Directory.Exists(pastaDestino)) Directory.CreateDirectory(pastaDestino);

        using (var stream = new FileStream(Path.Combine(pastaDestino, nomeArquivo), FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        return $"/imagens/personagens/{nomeArquivo}";
    }
}