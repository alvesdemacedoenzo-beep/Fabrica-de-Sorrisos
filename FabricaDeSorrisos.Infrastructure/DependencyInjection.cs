using FabricaDeSorrisos.Application.Abstractions.Repositories;
using FabricaDeSorrisos.Application.Abstractions.Services;
using FabricaDeSorrisos.Infrastructure.Identity;
using FabricaDeSorrisos.Infrastructure.Persistence;
using FabricaDeSorrisos.Infrastructure.Repositories;
using FabricaDeSorrisos.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FabricaDeSorrisos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Configurar o Banco de Dados SQL Server
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // --- REGRAS DE SENHA ---
            options.Password.RequiredLength = 8; // Mínimo de 8 caracteres
            options.Password.RequireDigit = true; // Precisa de Número (0-9)
            options.Password.RequireLowercase = true; // Precisa de Minúscula (a-z)
            options.Password.RequireUppercase = true; // Precisa de Maiúscula (A-Z)
            options.Password.RequireNonAlphanumeric = true; // Precisa de Caractere Especial (!@#$%^&*)

            // Outras configurações úteis (Opcional)
            options.User.RequireUniqueEmail = true; // Não deixa cadastrar mesmo e-mail 2x
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        // Configurações de senha (opcional, para facilitar testes deixa fraca)
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3; // Senha curta para testar
        });


        // 3. Registrar Repositórios
        services.AddScoped<IBrinquedoRepository, EfBrinquedoRepository>();
        services.AddScoped<ICategoriaRepository, EfCategoriaRepository>();
        services.AddScoped<IMarcaRepository, EfMarcaRepository>();
        services.AddScoped<IPersonagemRepository, EfPersonagemRepository>();
        services.AddScoped<IFavoritoRepository, EfFavoritoRepository>();
        services.AddScoped<IFaixaEtariaRepository, EfFaixaEtariaRepository>();
        services.AddScoped<ICarrinhoRepository, EfCarrinhoRepository>();
        // Adicione esta linha junto com os outros Repositórios
        services.AddScoped<ISubCategoriaRepository, EfSubCategoriaRepository>();
        // Adicione os de Avaliação e Favorito se criou os arquivos
        services.AddScoped<IUsuarioRepository, EfUsuarioRepository>();
        services.AddScoped<IPedidoRepository, EfPedidoRepository>();
        services.AddScoped<IFeedbackRepository, EfFeedbackRepository>();

        // 4. Registrar Serviços de Leitura
        services.AddScoped<IBrinquedoQueryService, BrinquedoQueryService>();
        services.AddScoped<ICatalogLookupService, CatalogLookupService>();
        services.AddScoped<ISuporteRepository, SuporteRepository>();


        return services;
    }
}
