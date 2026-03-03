using System.Security.Claims;
using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FabricaDeSorrisos.Infrastructure.Identity;

public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    private readonly AppDbContext _context;

    public CustomClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor,
        AppDbContext context)
        : base(userManager, roleManager, optionsAccessor)
    {
        _context = context;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        // 1. Gera as claims padrões (Nome, Email, Role)
        var identity = await base.GenerateClaimsAsync(user);

        // 2. Busca o ID do usuário na tabela de domínio
        var usuarioSistema = await _context.UsuariosDoSistema
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.IdentityUserId == user.Id);

        // 3. Se achou, grava o ID num "crachá" (Claim) extra
        if (usuarioSistema != null)
        {
            identity.AddClaim(new Claim("UsuarioSistemaId", usuarioSistema.Id.ToString()));
            identity.AddClaim(new Claim("NomeCompleto", usuarioSistema.NomeCompleto)); // Bônus: Nome real
        }

        return identity;
    }
}