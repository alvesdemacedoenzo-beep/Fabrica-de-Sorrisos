using FabricaDeSorrisos.Infrastructure.Persistence;
using FabricaDeSorrisos.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FabricaDeSorrisos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public class UpdateUsuarioRequest
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public string TipoUsuario { get; set; }
        }

        public UsuariosController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var usuarios = await _context.UsuariosDoSistema
                .Include(u => u.TipoUsuario)
                .Select(u => new
                {
                    Id = u.Id,
                    Nome = u.NomeCompleto,
                    Email = u.Email,
                    TipoUsuario = u.TipoUsuario != null
                        ? u.TipoUsuario.Nome
                        : string.Empty
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.UsuariosDoSistema.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(usuario.IdentityUserId))
            {
                var identityUser = await _userManager.FindByIdAsync(usuario.IdentityUserId);
                if (identityUser != null)
                {
                    var del = await _userManager.DeleteAsync(identityUser);
                    if (!del.Succeeded)
                        return BadRequest(del.Errors);
                }
            }

            _context.UsuariosDoSistema.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUsuarioRequest model)
        {
            var usuario = await _context.UsuariosDoSistema.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) return NotFound();

            usuario.NomeCompleto = model.Nome;
            usuario.Email = model.Email;

            var tipoNome = model.TipoUsuario switch
            {
                "Admin" => "Administrador",
                "Gerente" => "Gerente",
                _ => "Cliente"
            };

            var tipo = await _context.TiposUsuarios.FirstOrDefaultAsync(t => t.Nome == tipoNome);
            if (tipo == null) return StatusCode(500, "Tipo de usuário não encontrado.");
            usuario.TipoUsuarioId = tipo.Id;

            if (!string.IsNullOrWhiteSpace(usuario.IdentityUserId))
            {
                var identityUser = await _userManager.FindByIdAsync(usuario.IdentityUserId);
                if (identityUser != null)
                {
                    identityUser.Email = model.Email;
                    identityUser.UserName = model.Email;
                    await _userManager.UpdateAsync(identityUser);

                    if (!string.IsNullOrWhiteSpace(model.Senha))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
                        var r = await _userManager.ResetPasswordAsync(identityUser, token, model.Senha);
                        if (!r.Succeeded) return BadRequest(r.Errors);
                    }

                    var roles = await _userManager.GetRolesAsync(identityUser);
                    if (roles.Any())
                        await _userManager.RemoveFromRolesAsync(identityUser, roles);
                    var addRole = await _userManager.AddToRoleAsync(identityUser, model.TipoUsuario);
                    if (!addRole.Succeeded) return BadRequest(addRole.Errors);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new { usuario.Id });
        }
    }
}
