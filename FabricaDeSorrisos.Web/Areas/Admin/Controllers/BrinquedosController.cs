using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabricaDeSorrisos.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Administrador,ADMINISTRADOR")]

public class BrinquedosController : Controller
{
    private readonly IBrinquedoRepository _brinquedoRepo;
    private readonly IMarcaRepository _marcaRepo;
    private readonly ICategoriaRepository _categoriaRepo;

    // 1. NOVO REPOSITÓRIO ADICIONADO
    private readonly ISubCategoriaRepository _subCategoriaRepo;

    private readonly IFaixaEtariaRepository _faixaRepo;
    private readonly IPersonagemRepository _personagemRepo;
    private readonly IWebHostEnvironment _env;

    public BrinquedosController(
        IBrinquedoRepository brinquedoRepo,
        IMarcaRepository marcaRepo,
        ICategoriaRepository categoriaRepo,

        // 2. INJEÇÃO NO CONSTRUTOR
        ISubCategoriaRepository subCategoriaRepo,

        IFaixaEtariaRepository faixaRepo,
        IPersonagemRepository personagemRepo,
        IWebHostEnvironment env)
    {
        _brinquedoRepo = brinquedoRepo;
        _marcaRepo = marcaRepo;
        _categoriaRepo = categoriaRepo;
        _subCategoriaRepo = subCategoriaRepo; // Atribuição
        _faixaRepo = faixaRepo;
        _personagemRepo = personagemRepo;
        _env = env;
    }

    // 1. LISTAGEM
    public async Task<IActionResult> Index()
    {
        var brinquedos = await _brinquedoRepo.GetAllAsync();
        return View(brinquedos);
    }

    // 2. CRIAR (GET)
    public async Task<IActionResult> Create()
    {
        var viewModel = new BrinquedoFormViewModel();
        await CarregarListas(viewModel); // Preenche os <select>
        return View(viewModel);
    }

    // 3. CRIAR (POST)
    [HttpPost]
    public async Task<IActionResult> Create(BrinquedoFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await CarregarListas(model);
            return View(model);
        }

        string? urlImagem = null;
        if (model.ImagemUpload != null)
        {
            urlImagem = await SalvarArquivo(model.ImagemUpload);
        }

        var novoBrinquedo = new Brinquedo
        {
            Nome = model.Nome,
            Descricao = model.Descricao,
            Preco = model.Preco,
            Estoque = model.Estoque,
            Ativo = model.Ativo,
            ImagemUrl = urlImagem,
            MarcaId = model.MarcaId,
            CategoriaId = model.CategoriaId,
            SubCategoriaId = model.SubCategoriaId,
            FaixaEtariaId = model.FaixaEtariaId,
            PersonagemId = model.PersonagemId
        };

        await _brinquedoRepo.AddAsync(novoBrinquedo);
        return RedirectToAction(nameof(Index));
    }

    // 4. EDITAR (GET)
    public async Task<IActionResult> Edit(int id)
    {
        var b = await _brinquedoRepo.GetByIdAsync(id);
        if (b == null) return NotFound();

        var viewModel = new BrinquedoFormViewModel
        {
            Id = b.Id,
            Nome = b.Nome,
            Descricao = b.Descricao,
            Preco = b.Preco,
            Estoque = b.Estoque,
            Ativo = b.Ativo,
            ImagemUrlAtual = b.ImagemUrl,
            MarcaId = b.MarcaId,
            CategoriaId = b.CategoriaId,
            SubCategoriaId = b.SubCategoriaId,
            FaixaEtariaId = b.FaixaEtariaId,
            PersonagemId = b.PersonagemId
        };

        await CarregarListas(viewModel);
        return View(viewModel);
    }

    // 5. EDITAR (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(BrinquedoFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await CarregarListas(model);
            return View(model);
        }

        var brinquedoOriginal = await _brinquedoRepo.GetByIdAsync(model.Id);
        if (brinquedoOriginal == null) return NotFound();

        // Atualiza campos
        brinquedoOriginal.Nome = model.Nome;
        brinquedoOriginal.Descricao = model.Descricao;
        brinquedoOriginal.Preco = model.Preco;
        brinquedoOriginal.Estoque = model.Estoque;
        brinquedoOriginal.Ativo = model.Ativo;
        brinquedoOriginal.MarcaId = model.MarcaId;
        brinquedoOriginal.CategoriaId = model.CategoriaId;
        brinquedoOriginal.SubCategoriaId = model.SubCategoriaId;
        brinquedoOriginal.FaixaEtariaId = model.FaixaEtariaId;
        brinquedoOriginal.PersonagemId = model.PersonagemId;

        // Troca imagem se enviou nova
        if (model.ImagemUpload != null)
        {
            brinquedoOriginal.ImagemUrl = await SalvarArquivo(model.ImagemUpload);
        }

        await _brinquedoRepo.UpdateAsync(brinquedoOriginal);
        return RedirectToAction(nameof(Index));
    }

    // 6. ALTERNAR STATUS (SOFT DELETE)
    [HttpPost]
    public async Task<IActionResult> AlternarStatus(int id)
    {
        var brinquedo = await _brinquedoRepo.GetByIdAsync(id);
        if (brinquedo == null) return NotFound();

        brinquedo.Ativo = !brinquedo.Ativo; // Inverte
        await _brinquedoRepo.UpdateAsync(brinquedo);

        return RedirectToAction(nameof(Index));
    }

    // --- MÉTODOS AUXILIARES ---

    private async Task CarregarListas(BrinquedoFormViewModel model)
    {
        model.Marcas = await _marcaRepo.GetAllAsync();
        model.Categorias = await _categoriaRepo.GetAllAsync();
        model.FaixasEtarias = await _faixaRepo.GetAllAsync();
        model.Personagens = await _personagemRepo.GetAllAsync();
        // 3. CARREGAR A LISTA NOVA
        model.SubCategorias = await _subCategoriaRepo.GetAllAsync();
    }

    private async Task<string> SalvarArquivo(IFormFile arquivo)
    {
        var nomeArquivo = Guid.NewGuid() + "_" + arquivo.FileName;
        var pasta = Path.Combine(_env.WebRootPath, "imagens", "produtos");
        if (!Directory.Exists(pasta)) Directory.CreateDirectory(pasta);

        using (var stream = new FileStream(Path.Combine(pasta, nomeArquivo), FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }
        return $"/imagens/produtos/{nomeArquivo}";
    }
}