using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabricaDeSorrisos.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Administrador,ADMINISTRADOR")]

public class MarcasController : Controller
{
    private readonly IMarcaRepository _repository;
    private readonly IWebHostEnvironment _env; // Para salvar a foto na pasta

    // Injeção de Dependência direta (Padrão Saphira)
    public MarcasController(IMarcaRepository repository, IWebHostEnvironment env)
    {
        _repository = repository;
        _env = env;
    }

    // LISTAGEM
    public async Task<IActionResult> Index()
    {
        var marcas = await _repository.GetAllAsync();
        return View(marcas);
    }

    public IActionResult Create()
    {
        // Passamos uma marca novinha (vazia) para a View não reclamar de Null
        return View(new FabricaDeSorrisos.Domain.Entities.Marca());
    }

    // AÇÃO DE SALVAR (POST)
    [HttpPost]
    public async Task<IActionResult> Create(Marca marca, IFormFile? arquivo)
    {
        // Validação simples
        if (!ModelState.IsValid) return View(marca);

        // Upload da Imagem
        if (arquivo != null)
        {
            marca.LogotipoUrl = await SalvarArquivo(arquivo);
        }

        await _repository.AddAsync(marca);
        return RedirectToAction(nameof(Index));
    }

    // TELA DE EDITAR
    public async Task<IActionResult> Edit(int id)
    {
        var marca = await _repository.GetByIdAsync(id);
        if (marca == null) return NotFound();
        return View(marca);
    }

    // AÇÃO DE EDITAR (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(Marca marca, IFormFile? arquivo)
    {
        // Busca a marca original do banco para não perder dados
        var marcaOriginal = await _repository.GetByIdAsync(marca.Id);
        if (marcaOriginal == null) return NotFound();

        // Atualiza os dados
        marcaOriginal.Nome = marca.Nome;

        // Se mandou nova imagem, troca. Se não, mantém a antiga.
        if (arquivo != null)
        {
            // (Opcional) Aqui poderia deletar a imagem antiga do disco antes
            marcaOriginal.LogotipoUrl = await SalvarArquivo(arquivo);
        }

        await _repository.UpdateAsync(marcaOriginal);
        return RedirectToAction(nameof(Index));
    }

    // AÇÃO DE DELETAR
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var marca = await _repository.GetByIdAsync(id);
        if (marca != null)
        {
            await _repository.DeleteAsync(marca);
        }
        return RedirectToAction(nameof(Index));
    }

    // Método Privado para salvar o arquivo no disco (wwwroot)
    private async Task<string> SalvarArquivo(IFormFile arquivo)
    {
        var nomeArquivo = Guid.NewGuid() + "_" + arquivo.FileName; // Nome único
        var pastaDestino = Path.Combine(_env.WebRootPath, "imagens", "marcas");

        if (!Directory.Exists(pastaDestino)) Directory.CreateDirectory(pastaDestino);

        using (var stream = new FileStream(Path.Combine(pastaDestino, nomeArquivo), FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        return $"/imagens/marcas/{nomeArquivo}";
    }
}