using FabricaDeSorrisos.Infrastructure;
using FabricaDeSorrisos.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. SERVIÇOS DO CONTAINER (DI)
// ==========================================

// Adiciona o MVC (Controllers e Views)
builder.Services.AddControllersWithViews();

// INJEÇÃO DA INFRAESTRUTURA
// Carrega o Banco de Dados, Repositórios e Configuração Básica do Identity
builder.Services.AddInfrastructure(builder.Configuration);

// CONFIGURAÇÃO DE SENHA FORTE (Identity)
// Estamos reconfigurando aqui para garantir que as regras sejam aplicadas
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8; // Mínimo 8 caracteres
    options.Password.RequireDigit = true; // Requer número
    options.Password.RequireLowercase = true; // Requer minúscula
    options.Password.RequireUppercase = true; // Requer maiúscula
    options.Password.RequireNonAlphanumeric = true; // Requer caractere especial (@, #, etc)
    options.User.RequireUniqueEmail = true; // E-mail único
});

// REGISTRO DA FÁBRICA DE CLAIMS (CUSTOMIZADA)
// Isso ensina o Identity a colocar o ID da tabela 'UsuariosDoSistema' dentro do Cookie
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsPrincipalFactory>();

// ==========================================
// 2. CONSTRUÇÃO DO APP
// ==========================================
var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor default de HSTS é 30 dias.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// HABILITA AUTENTICAÇÃO E AUTORIZAÇÃO
app.UseAuthentication(); // Quem é você? (Login)
app.UseAuthorization();  // O que você pode fazer? (Permissões)

// ==========================================
// 3. ROTAS
// ==========================================

// Rota de Admin (Áreas)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Rota Padrão (Home)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ==========================================
// 4. DATABASE SEEDER (POPULAR BANCO)
// ==========================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Pega os serviços necessários
        var context = services.GetRequiredService<FabricaDeSorrisos.Infrastructure.Persistence.AppDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        var logger = services.GetRequiredService<ILogger<Program>>();
        DbConnection conn = context.Database.GetDbConnection();
        logger.LogInformation("Conectando ao banco: {DataSource} / {Database}", conn.DataSource, conn.Database);
        await context.Database.MigrateAsync();

        // Roda o Seeder (Cria Admin, Faixas Etárias, etc.)
        await FabricaDeSorrisos.Infrastructure.Persistence.Seed.DatabaseSeeder.SeedAsync(userManager, roleManager, context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao rodar o DatabaseSeeder.");
    }
}

app.Run();
