using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Identity;
using FabricaDeSorrisos.Infrastructure.Persistence;
using FabricaDeSorrisos.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _context;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
    }

    // --- LOGIN ---
    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarMe, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            // =====================================================================
            // SINCRONIZAÇÃO DINÂMICA E EXATA
            // =====================================================================
            var usuarioSistema = await _context.UsuariosDoSistema.FirstOrDefaultAsync(u => u.IdentityUserId == user.Id);

            if (usuarioSistema != null)
            {
                var tipoUsuario = await _context.TiposUsuarios.FindAsync(usuarioSistema.TipoUsuarioId);

                if (tipoUsuario != null)
                {
                    // Agora usamos O NOME EXATO que está cadastrado no banco de dados!
                    string roleCorreta = tipoUsuario.Nome;

                    // Verifica se a permissão já existe no C#. Se não existir, ele cria.
                    if (!await _roleManager.RoleExistsAsync(roleCorreta))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(roleCorreta));
                    }

                    // Garante que a role Cliente também exista
                    if (!await _roleManager.RoleExistsAsync("Cliente"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Cliente"));
                    }

                    var rolesAtuais = await _userManager.GetRolesAsync(user);

                    // Se o usuário não tiver a role correta, atualizamos agora!
                    if (!rolesAtuais.Contains(roleCorreta))
                    {
                        if (rolesAtuais.Any())
                        {
                            await _userManager.RemoveFromRolesAsync(user, rolesAtuais);
                        }

                        await _userManager.AddToRoleAsync(user, roleCorreta);
                        await _signInManager.RefreshSignInAsync(user);
                    }
                }
            }
            // =====================================================================

            // Verifica as variações possíveis para garantir o redirecionamento
            bool isAdministrativo = await _userManager.IsInRoleAsync(user, "Administrador") ||
                                    await _userManager.IsInRoleAsync(user, "ADMINISTRADOR") ||
                                    await _userManager.IsInRoleAsync(user, "Admin") ||
                                    await _userManager.IsInRoleAsync(user, "Gerente");

            if (isAdministrativo)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Login inválido. Verifique e-mail e senha.");
        return View(model);
    }

    // --- REGISTRO (CRIAR CONTA) ---
    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Senha);

        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("Cliente"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Cliente"));
            }

            await _userManager.AddToRoleAsync(user, "Cliente");

            var tipoCliente = await _context.TiposUsuarios.FirstOrDefaultAsync(t => t.Nome == "Cliente");

            if (tipoCliente != null)
            {
                var usuarioSistema = new Usuario
                {
                    NomeCompleto = model.NomeCompleto,
                    Email = model.Email,
                    Cpf = "00000000000",
                    IdentityUserId = user.Id,
                    TipoUsuarioId = tipoCliente.Id
                };

                _context.UsuariosDoSistema.Add(usuarioSistema);
                await _context.SaveChangesAsync();
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    // --- LOGOUT ---
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        TempData["Erro"] = "Acesso restrito. Você não tem permissão para entrar nessa página.";
        return RedirectToAction("Index", "Home");
    }
}