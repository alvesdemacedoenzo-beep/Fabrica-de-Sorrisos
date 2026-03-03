using FabricaDeSorrisos.Infrastructure;
using FabricaDeSorrisos.Infrastructure.Identity;
using FabricaDeSorrisos.Infrastructure.Persistence;
using FabricaDeSorrisos.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

var configuredUrls = builder.Configuration["ASPNETCORE_URLS"];
if (string.IsNullOrWhiteSpace(configuredUrls))
{
    builder.WebHost.UseUrls("http://localhost:5259");
}

// 1. Services (Controllers, Swagger)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<JwtTokenService>();

// 2. Infraestrutura (Banco, EF, Identity, JWT, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

// 3. CORS (para Web/Desktop consumirem a API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb", policy =>
    {
        policy.WithOrigins(
                "https://localhost:7001",
                "http://localhost:5001",
                "https://localhost:5001")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 4. Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS precisa vir antes de Auth
app.UseCors("AllowWeb");

// Servir arquivos estáticos do projeto Web (wwwroot) como fallback em /static
try
{
    var webRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "FabricaDeSorrisos.Web", "wwwroot"));
    if (Directory.Exists(webRoot))
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(webRoot),
            RequestPath = "/static"
        });
    }
}
catch { }

app.UseAuthentication();
app.UseAuthorization();

// 5. Seed (popula o banco com Admin, Roles, etc.)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var context = services.GetRequiredService<AppDbContext>();

        var startLogger = services.GetRequiredService<ILogger<Program>>();
        DbConnection conn = context.Database.GetDbConnection();
        startLogger.LogInformation("Conectando ao banco: {DataSource} / {Database}", conn.DataSource, conn.Database);

        await context.Database.MigrateAsync();
        await DatabaseSeeder.SeedAsync(userManager, roleManager, context);
    }
    catch (Exception ex)
    {
        var errLogger = services.GetRequiredService<ILogger<Program>>();
        errLogger.LogError(ex, "Erro ao executar o seed do banco de dados.");
    }
}

// 6. Controllers
app.MapControllers();

app.Run();
