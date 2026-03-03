using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Persistence;

// Herdamos de IdentityDbContext para ganhar as tabelas de login prontas
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Mapeamento das Tabelas do Domínio
    public DbSet<Brinquedo> Brinquedos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<FaixaEtaria> FaixasEtarias { get; set; }

    public DbSet<TipoUsuario> TiposUsuarios { get; set; }
    public DbSet<Usuario> UsuariosDoSistema { get; set; } // Nome diferente para não confundir com Identity

    public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }

    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<Favorito> Favoritos { get; set; }
    public DbSet<MensagemSuporte> MensagensSuporte { get; set; }
    public DbSet<Personagem> Personagens { get; set; }
    public DbSet<SubCategoria> SubCategorias { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // --- CORREÇÃO DO ERRO DE DECIMAL ---
        // Define que todo campo decimal do Pedido terá 18 dígitos no total e 2 casas decimais (ex: 150.50)
        builder.Entity<Pedido>()
            .Property(p => p.ValorTotal)
            .HasColumnType("decimal(18,2)");

        builder.Entity<Pedido>()
            .Property(p => p.ValorFrete)
            .HasColumnType("decimal(18,2)");

        // Se tiver outros decimais (como Preco no Brinquedo), é bom garantir também:
        builder.Entity<Brinquedo>()
            .Property(b => b.Preco)
            .HasColumnType("decimal(18,2)");

        builder.Entity<PedidoItem>()
            .Property(i => i.PrecoUnitario)
            .HasColumnType("decimal(18,2)");
    }
}
