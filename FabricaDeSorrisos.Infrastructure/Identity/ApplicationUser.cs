using Microsoft.AspNetCore.Identity;

namespace FabricaDeSorrisos.Infrastructure.Identity; // Mudou de Entities para Identity

public class ApplicationUser : IdentityUser
{
    // O IdentityUser já traz Id, UserName, Email, PasswordHash, etc.
    // Aqui poderíamos adicionar campos extras APENAS de login (ex: Token de Refresh), 
    // mas por enquanto deixe vazio.
}
