using FabricaDeSorrisos.Domain.Entities;
using FabricaDeSorrisos.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FabricaDeSorrisos.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    private record SeedUser(string Email, string Password, string Role, string TipoUsuarioNome, string NomeCompleto);

    private static readonly SeedUser[] InitialUsers =
    {
        new("adm@fabricadesorrisos.com", "Admin@123", "Admin", "Administrador", "Administrador do Sistema"),
        new("gerente@fabricadesorrisos.com", "Gerente@123", "Gerente", "Gerente", "Gerente Padrão"),
        new("cliente@fabricadesorrisos.com", "Cliente@123", "Cliente", "Cliente", "Cliente Padrão")
    };

    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
    {
        await SeedIdentityAsync(userManager, roleManager, context);
        await SeedFaixasEtariasAsync(context);
        await SeedMarcasAsync(context);
        await SeedPersonagensAsync(context);
        await SeedCategoriasAsync(context);
        await SeedBrinquedosAsync(context);
    }

    private static async Task SeedIdentityAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
    {
        var roles = new[] { "Admin", "Gerente", "Cliente" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        if (!context.TiposUsuarios.Any())
        {
            context.TiposUsuarios.AddRange(
                new TipoUsuario { Nome = "Administrador" },
                new TipoUsuario { Nome = "Gerente" },
                new TipoUsuario { Nome = "Cliente" }
            );
            await context.SaveChangesAsync();
        }

        foreach (var seedUser in InitialUsers)
        {
            var user = await userManager.FindByEmailAsync(seedUser.Email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = seedUser.Email, Email = seedUser.Email, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, seedUser.Password);
                if (!result.Succeeded) throw new Exception($"Erro ao criar {seedUser.Email}");
            }

            if (!await userManager.IsInRoleAsync(user, seedUser.Role))
                await userManager.AddToRoleAsync(user, seedUser.Role);

            if (!context.UsuariosDoSistema.Any(u => u.IdentityUserId == user.Id))
            {
                var tipo = await context.TiposUsuarios.FirstOrDefaultAsync(t => t.Nome == seedUser.TipoUsuarioNome);
                if (tipo != null)
                {
                    context.UsuariosDoSistema.Add(new Usuario { NomeCompleto = seedUser.NomeCompleto, Email = seedUser.Email, Cpf = "00000000000", IdentityUserId = user.Id, TipoUsuario = tipo });
                    await context.SaveChangesAsync();
                }
            }
        }
    }

    private static async Task SeedFaixasEtariasAsync(AppDbContext context)
    {
        if (!context.FaixasEtarias.Any())
        {
            context.FaixasEtarias.AddRange(
                new FaixaEtaria { Descricao = "0 a 18 meses (Bebês)" },
                new FaixaEtaria { Descricao = "1 a 3 anos (Primeira Infância)" },
                new FaixaEtaria { Descricao = "3 a 5 anos (Pré-escolar)" },
                new FaixaEtaria { Descricao = "5 a 7 anos (Alfabetização)" },
                new FaixaEtaria { Descricao = "7 a 10 anos (Crianças maiores)" },
                new FaixaEtaria { Descricao = "+10 anos (Pré-adolescentes)" },
                new FaixaEtaria { Descricao = "Uso Animal (Pets)" }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedMarcasAsync(AppDbContext context)
    {
        if (!context.Marcas.Any())
        {
            context.Marcas.AddRange(
                new Marca { Nome = "Lego" },
                new Marca { Nome = "Mattel" },
                new Marca { Nome = "Hasbro" },
                new Marca { Nome = "Fisher-Price" },
                new Marca { Nome = "Estrela" },
                new Marca { Nome = "Candide" },
                new Marca { Nome = "Grow" },
                new Marca { Nome = "Linha Pet" },
                new Marca { Nome = "DC Comics" },
                new Marca { Nome = "Sanrio" },
                new Marca { Nome = "Spalding" },
                new Marca { Nome = "Nike" }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedPersonagensAsync(AppDbContext context)
    {
        if (!context.Personagens.Any())
        {
            context.Personagens.AddRange(
                new Personagem { Nome = "Sem Personagem" },
                new Personagem { Nome = "Barbie" },
                new Personagem { Nome = "Hot Wheels" },
                new Personagem { Nome = "Homem-Aranha" },
                new Personagem { Nome = "Frozen" },
                new Personagem { Nome = "Star Wars" },
                new Personagem { Nome = "Patrulha Canina" },
                new Personagem { Nome = "Peppa Pig" }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCategoriasAsync(AppDbContext context)
    {
        if (!context.Categorias.Any())
        {
            var catBrinquedos = new Categoria { Nome = "Brinquedos" };
            var catEducativos = new Categoria { Nome = "Educativos" };
            var catJogos = new Categoria { Nome = "Jogos" };
            var catPets = new Categoria { Nome = "Pets" };
            var catFantasias = new Categoria { Nome = "Fantasias" };
            var catPelucias = new Categoria { Nome = "Pelucias" };
            var catEsportes = new Categoria { Nome = "Esportes" };

            context.Categorias.AddRange(catBrinquedos, catEducativos, catJogos, catPets, catFantasias, catPelucias, catEsportes);
            await context.SaveChangesAsync();

            context.SubCategorias.AddRange(
                new SubCategoria { Nome = "Bonecas e Acessórios", CategoriaId = catBrinquedos.Id },
                new SubCategoria { Nome = "Veículos e Pistas", CategoriaId = catBrinquedos.Id },
                new SubCategoria { Nome = "Blocos de Montar", CategoriaId = catBrinquedos.Id },
                new SubCategoria { Nome = "Ação e Aventura", CategoriaId = catBrinquedos.Id },
                new SubCategoria { Nome = "Quebra-Cabeças", CategoriaId = catEducativos.Id },
                new SubCategoria { Nome = "Alfabetização e Números", CategoriaId = catEducativos.Id },
                new SubCategoria { Nome = "Tabuleiro", CategoriaId = catJogos.Id },
                new SubCategoria { Nome = "Cartas", CategoriaId = catJogos.Id },
                new SubCategoria { Nome = "Pássaros", CategoriaId = catPets.Id },
                new SubCategoria { Nome = "Gatos", CategoriaId = catPets.Id },
                new SubCategoria { Nome = "Cachorros", CategoriaId = catPets.Id },
                new SubCategoria { Nome = "Roedores", CategoriaId = catPets.Id },
                new SubCategoria { Nome = "Heróis", CategoriaId = catFantasias.Id },
                new SubCategoria { Nome = "Heroínas", CategoriaId = catFantasias.Id },
                new SubCategoria { Nome = "Princesas", CategoriaId = catFantasias.Id },
                new SubCategoria { Nome = "Personagens de Pelúcia", CategoriaId = catPelucias.Id },
                new SubCategoria { Nome = "Pelúcias Tradicionais", CategoriaId = catPelucias.Id },
                new SubCategoria { Nome = "Almofadas de Pescoço", CategoriaId = catPelucias.Id },
                new SubCategoria { Nome = "Basquete", CategoriaId = catEsportes.Id },
                new SubCategoria { Nome = "Artes Marciais", CategoriaId = catEsportes.Id },
                new SubCategoria { Nome = "Futebol", CategoriaId = catEsportes.Id }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedBrinquedosAsync(AppDbContext context)
    {
        if (!context.Brinquedos.Any())
        {
            var lego = await context.Marcas.FirstAsync(m => m.Nome == "Lego");
            var mattel = await context.Marcas.FirstAsync(m => m.Nome == "Mattel");
            var hasbro = await context.Marcas.FirstAsync(m => m.Nome == "Hasbro");
            var estrela = await context.Marcas.FirstAsync(m => m.Nome == "Estrela");
            var fisher = await context.Marcas.FirstAsync(m => m.Nome == "Fisher-Price");
            var marcaPet = await context.Marcas.FirstAsync(m => m.Nome == "Linha Pet");
            var dc = await context.Marcas.FirstAsync(m => m.Nome == "DC Comics");
            var nike = await context.Marcas.FirstAsync(m => m.Nome == "Nike");
            var spalding = await context.Marcas.FirstAsync(m => m.Nome == "Spalding");

            var starWars = await context.Personagens.FirstAsync(p => p.Nome == "Star Wars");
            var barbie = await context.Personagens.FirstAsync(p => p.Nome == "Barbie");
            var hotWheels = await context.Personagens.FirstAsync(p => p.Nome == "Hot Wheels");
            var aranha = await context.Personagens.FirstAsync(p => p.Nome == "Homem-Aranha");
            var semPers = await context.Personagens.FirstAsync(p => p.Nome == "Sem Personagem");

            var catBrinquedos = await context.Categorias.FirstAsync(c => c.Nome == "Brinquedos");
            var catJogos = await context.Categorias.FirstAsync(c => c.Nome == "Jogos");
            var catEducativos = await context.Categorias.FirstAsync(c => c.Nome == "Educativos");
            var catPets = await context.Categorias.FirstAsync(c => c.Nome == "Pets");
            var catFantasias = await context.Categorias.FirstAsync(c => c.Nome == "Fantasias");
            var catPelucias = await context.Categorias.FirstAsync(c => c.Nome == "Pelucias");
            var catEsportes = await context.Categorias.FirstAsync(c => c.Nome == "Esportes");

            var subBlocos = await context.SubCategorias.FirstAsync(s => s.Nome == "Blocos de Montar");
            var subBonecas = await context.SubCategorias.FirstAsync(s => s.Nome == "Bonecas e Acessórios");
            var subVeiculos = await context.SubCategorias.FirstAsync(s => s.Nome == "Veículos e Pistas");
            var subAcao = await context.SubCategorias.FirstAsync(s => s.Nome == "Ação e Aventura");
            var subTabuleiro = await context.SubCategorias.FirstAsync(s => s.Nome == "Tabuleiro");
            var subEducativo = await context.SubCategorias.FirstAsync(s => s.Nome.Contains("Alfabetização"));
            var subPassaros = await context.SubCategorias.FirstAsync(s => s.Nome == "Pássaros");
            var subGatos = await context.SubCategorias.FirstAsync(s => s.Nome == "Gatos");
            var subCachorros = await context.SubCategorias.FirstAsync(s => s.Nome == "Cachorros");
            var subRoedores = await context.SubCategorias.FirstAsync(s => s.Nome == "Roedores");
            var subHerois = await context.SubCategorias.FirstAsync(s => s.Nome == "Heróis");
            var subHeroinas = await context.SubCategorias.FirstAsync(s => s.Nome == "Heroínas");
            var subPrincesas = await context.SubCategorias.FirstAsync(s => s.Nome == "Princesas");
            var subPersPelucia = await context.SubCategorias.FirstAsync(s => s.Nome == "Personagens de Pelúcia");
            var subPeluciaTrad = await context.SubCategorias.FirstAsync(s => s.Nome == "Pelúcias Tradicionais");
            var subAlmofada = await context.SubCategorias.FirstAsync(s => s.Nome == "Almofadas de Pescoço");
            var subBasquete = await context.SubCategorias.FirstAsync(s => s.Nome == "Basquete");
            var subArtesMarciais = await context.SubCategorias.FirstAsync(s => s.Nome == "Artes Marciais");
            var subFutebol = await context.SubCategorias.FirstAsync(s => s.Nome == "Futebol");

            var faixa5a7 = await context.FaixasEtarias.FirstAsync(f => f.Descricao.Contains("5 a 7"));
            var faixa7a10 = await context.FaixasEtarias.FirstAsync(f => f.Descricao.Contains("7 a 10"));
            var faixa10mais = await context.FaixasEtarias.FirstAsync(f => f.Descricao.Contains("+10"));
            var faixaBebe = await context.FaixasEtarias.FirstAsync(f => f.Descricao.Contains("1 a 3"));
            var faixaPet = await context.FaixasEtarias.FirstAsync(f => f.Descricao.Contains("Uso Animal"));

            var produtos = new List<Brinquedo>
            {
                // ORIGINAIS E JOGOS
                new Brinquedo { Nome = "Lego Star Wars Millennium Falcon", Descricao = "Nave espacial lendária em blocos.", Preco = 899.90m, Estoque = 10, Ativo = true, MarcaId = lego.Id, CategoriaId = catBrinquedos.Id, SubCategoriaId = subBlocos.Id, PersonagemId = starWars.Id, FaixaEtariaId = faixa10mais.Id, ImagemUrl = "/imagens/produtos/legostarwars.jpg" },
                new Brinquedo { Nome = "Barbie Casa dos Sonhos", Descricao = "Casa completa com 3 andares.", Preco = 1299.90m, Estoque = 5, Ativo = true, MarcaId = mattel.Id, CategoriaId = catBrinquedos.Id, SubCategoriaId = subBonecas.Id, PersonagemId = barbie.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/barbiecasadossonhos.png" },
                new Brinquedo { Nome = "Pista Hot Wheels Ataque do Tubarão", Descricao = "Desafio de velocidade radical.", Preco = 249.90m, Estoque = 20, Ativo = true, MarcaId = mattel.Id, CategoriaId = catBrinquedos.Id, SubCategoriaId = subVeiculos.Id, PersonagemId = hotWheels.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/hotwheels.webp" },
                new Brinquedo { Nome = "Banco Imobiliário Clássico", Descricao = "O clássico jogo de estratégia financeira.", Preco = 129.90m, Estoque = 30, Ativo = true, MarcaId = estrela.Id, CategoriaId = catJogos.Id, SubCategoriaId = subTabuleiro.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa7a10.Id, ImagemUrl = "/imagens/produtos/bancoimobiliario.jpg" },
                new Brinquedo { Nome = "Boneco Homem-Aranha Titan Hero", Descricao = "Figura de ação articulada 30cm.", Preco = 99.90m, Estoque = 50, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catBrinquedos.Id, SubCategoriaId = subAcao.Id, PersonagemId = aranha.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/homemaranha.jpg" },
                new Brinquedo { Nome = "Cachorrinho Aprender e Brincar", Descricao = "Brinquedo educativo com músicas.", Preco = 199.90m, Estoque = 15, Ativo = true, MarcaId = fisher.Id, CategoriaId = catEducativos.Id, SubCategoriaId = subEducativo.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaBebe.Id, ImagemUrl = "/imagens/produtos/cachorrinho.jpg" },

                // --- TODOS OS 17 ITENS DE PETS RESTAURADOS ---
                new Brinquedo { Nome = "Bicicleta para pássaros", Descricao = "Treinamento interativo.", Preco = 45.90m, Estoque = 20, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subPassaros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/bicicleta.webp" },
                new Brinquedo { Nome = "Skate para pássaros", Descricao = "Mini skate divertido.", Preco = 35.90m, Estoque = 25, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subPassaros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/skate.webp" },
                new Brinquedo { Nome = "Kit pega argola e palito", Descricao = "Estimula a inteligência.", Preco = 55.00m, Estoque = 15, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subPassaros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/palitoEArgola.webp" },
                new Brinquedo { Nome = "Banheira para pássaros", Descricao = "Higiene prática.", Preco = 29.90m, Estoque = 30, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subPassaros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/banheira.jpg" },
                new Brinquedo { Nome = "Playground gigante pássaros", Descricao = "Centro de diversão completo.", Preco = 189.90m, Estoque = 10, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subPassaros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/playgroundPassaro.webp" },
                new Brinquedo { Nome = "Playground twister", Descricao = "Diversão em formato twister.", Preco = 129.90m, Estoque = 12, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subPassaros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/playground.jpg" },
                new Brinquedo { Nome = "Torre bolinhas corre corre", Descricao = "Trilhos para gatos.", Preco = 79.90m, Estoque = 22, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subGatos.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/corre.webp" },
                new Brinquedo { Nome = "Túnel com bolinha", Descricao = "Curiosidade felina.", Preco = 89.90m, Estoque = 18, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subGatos.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/tunelGaro.webp" },
                new Brinquedo { Nome = "Kit brinquedos gatos", Descricao = "3 unidades de diversão.", Preco = 39.90m, Estoque = 40, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subGatos.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/bolaGato.webp" },
                new Brinquedo { Nome = "Arranhador estrela mola", Descricao = "Resistente e divertido.", Preco = 115.00m, Estoque = 15, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subGatos.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/arranhador.webp" },
                new Brinquedo { Nome = "Bola interativa", Descricao = "Move-se sozinha.", Preco = 65.90m, Estoque = 25, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subGatos.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/interativa.jpg" },
                new Brinquedo { Nome = "Bola de tênis", Descricao = "Clássico para cães.", Preco = 19.90m, Estoque = 50, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subCachorros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/bolaDog.webp" },
                new Brinquedo { Nome = "Osso rosa", Descricao = "Borracha TPR atóxica.", Preco = 25.90m, Estoque = 35, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subCachorros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/osso.webp" },
                new Brinquedo { Nome = "Galo que apita", Descricao = "Brinquedo sonoro divertido.", Preco = 42.50m, Estoque = 28, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subCachorros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/galo.webp" },
                new Brinquedo { Nome = "Macaco pelúcia apito", Descricao = "Macia e segura para cães.", Preco = 55.90m, Estoque = 20, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subCachorros.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/macaco.webp" },
                new Brinquedo { Nome = "Toca para roedores", Descricao = "Escuro e seguro.", Preco = 38.00m, Estoque = 18, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subRoedores.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/toca.webp" },
                new Brinquedo { Nome = "Cenoura roedores", Descricao = "Desgaste natural dentes.", Preco = 22.90m, Estoque = 40, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subRoedores.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/cenoura.webp" },
                new Brinquedo { Nome = "Roda roedores", Descricao = "Exercício silencioso.", Preco = 65.90m, Estoque = 15, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subRoedores.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/roda.png" },
                new Brinquedo { Nome = "Moinho interativo", Descricao = "Madeira sustentável.", Preco = 75.00m, Estoque = 10, Ativo = true, MarcaId = marcaPet.Id, CategoriaId = catPets.Id, SubCategoriaId = subRoedores.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaPet.Id, ImagemUrl = "/imagens/produtos/moinho.png" },

                // --- NOVOS 27 ITENS (FANTASIAS, PELUCIAS, ESPORTES) ---
                new Brinquedo { Nome = "Almofada Hello Kitty", Descricao = "Conforto Sanrio.", Preco = 69.90m, Estoque = 15, Ativo = true, MarcaId = estrela.Id, CategoriaId = catPelucias.Id, SubCategoriaId = subAlmofada.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaBebe.Id, ImagemUrl = "/imagens/produtos/hellok.webp" },
                new Brinquedo { Nome = "Almofada Mickey Mouse", Descricao = "Estilo Disney.", Preco = 69.90m, Estoque = 15, Ativo = true, MarcaId = estrela.Id, CategoriaId = catPelucias.Id, SubCategoriaId = subAlmofada.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaBebe.Id, ImagemUrl = "/imagens/produtos/mickey.webp" },
                new Brinquedo { Nome = "Almofada Stitch", Descricao = "Aconchego espacial.", Preco = 75.00m, Estoque = 10, Ativo = true, MarcaId = estrela.Id, CategoriaId = catPelucias.Id, SubCategoriaId = subAlmofada.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaBebe.Id, ImagemUrl = "/imagens/produtos/stichalmofada.webp" },
                new Brinquedo { Nome = "Pelúcia Furby", Descricao = "Amigo interativo.", Preco = 249.90m, Estoque = 8, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catPelucias.Id, SubCategoriaId = subPersPelucia.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/furby.webp" },
                new Brinquedo { Nome = "Pelúcia Sonic", Descricao = "Velocidade em pelúcia.", Preco = 119.90m, Estoque = 20, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catPelucias.Id, SubCategoriaId = subPersPelucia.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/sonic.webp" },
                new Brinquedo { Nome = "Pelúcia Stitch", Descricao = "Fofura azul.", Preco = 129.90m, Estoque = 12, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catPelucias.Id, SubCategoriaId = subPersPelucia.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/stich.webp" },
                new Brinquedo { Nome = "Pelúcia Urso Rosa", Descricao = "Clássico para abraçar.", Preco = 89.90m, Estoque = 25, Ativo = true, MarcaId = estrela.Id, CategoriaId = catPelucias.Id, SubCategoriaId = subPeluciaTrad.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaBebe.Id, ImagemUrl = "/imagens/produtos/rosa.webp" },
                new Brinquedo { Nome = "Pelúcia Urso Panda", Descricao = "Acompanha penico.", Preco = 149.90m, Estoque = 10, Ativo = true, MarcaId = estrela.Id, CategoriaId = catPelucias.Id, SubCategoriaId = subPeluciaTrad.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaBebe.Id, ImagemUrl = "/imagens/produtos/panda.webp" },
                new Brinquedo { Nome = "Pelúcia Capivara", Descricao = "Amigável e macia.", Preco = 79.90m, Estoque = 30, Ativo = true, MarcaId = estrela.Id, CategoriaId = catPelucias.Id, SubCategoriaId = subPeluciaTrad.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixaBebe.Id, ImagemUrl = "/imagens/produtos/capivara.webp" },
                new Brinquedo { Nome = "Fantasia Homem de Ferro", Descricao = "Traje heróico Marvel.", Preco = 199.90m, Estoque = 10, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catFantasias.Id, SubCategoriaId = subHerois.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/ferro.webp" },
                new Brinquedo { Nome = "Fantasia Homem Aranha", Descricao = "Máscara inclusa.", Preco = 179.90m, Estoque = 15, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catFantasias.Id, SubCategoriaId = subHerois.Id, PersonagemId = aranha.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/aranha.webp" },
                new Brinquedo { Nome = "Fantasia Capitão América", Descricao = "Uniforme com escudo.", Preco = 189.90m, Estoque = 12, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catFantasias.Id, SubCategoriaId = subHerois.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/capitao.webp" },
                new Brinquedo { Nome = "Fantasia Lady Bug", Descricao = "Aventuras em Paris.", Preco = 159.90m, Estoque = 18, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catFantasias.Id, SubCategoriaId = subHeroinas.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/ladybug.webp" },
                new Brinquedo { Nome = "Fantasia Vestido Batman", Descricao = "Versão charmosa DC.", Preco = 169.90m, Estoque = 10, Ativo = true, MarcaId = dc.Id, CategoriaId = catFantasias.Id, SubCategoriaId = subHeroinas.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/batman.webp" },
                new Brinquedo { Nome = "Fantasia Mulher Maravilha", Descricao = "Clássico Themyscira.", Preco = 185.00m, Estoque = 12, Ativo = true, MarcaId = dc.Id, CategoriaId = catFantasias.Id, SubCategoriaId = subHeroinas.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/mulher.webp" },
                new Brinquedo { Nome = "Fantasia Princesa Moana", Descricao = "Aventura nas ilhas.", Preco = 199.90m, Estoque = 10, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catFantasias.Id, SubCategoriaId = subPrincesas.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/moana.webp" },
                new Brinquedo { Nome = "Fantasia Rapunzel", Descricao = "Vestido longo encantado.", Preco = 210.00m, Estoque = 10, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catFantasias.Id, SubCategoriaId = subPrincesas.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/rapunzel.webp" },
                new Brinquedo { Nome = "Fantasia Elsa", Descricao = "Rainha da Neve.", Preco = 229.90m, Estoque = 15, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catFantasias.Id, SubCategoriaId = subPrincesas.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/elsa.webp" },
                new Brinquedo { Nome = "Dinossauro Boxeador", Descricao = "Inflável de bater divertido.", Preco = 89.90m, Estoque = 20, Ativo = true, MarcaId = estrela.Id, CategoriaId = catEsportes.Id, SubCategoriaId = subArtesMarciais.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/dinossauro.webp" },
                new Brinquedo { Nome = "Alvos de Pancada", Descricao = "Círculos para treinamento.", Preco = 115.00m, Estoque = 15, Ativo = true, MarcaId = hasbro.Id, CategoriaId = catEsportes.Id, SubCategoriaId = subArtesMarciais.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/pancada.webp" },
                new Brinquedo { Nome = "Luvas de Boxe Infantil", Descricao = "Proteção confortável.", Preco = 75.00m, Estoque = 30, Ativo = true, MarcaId = estrela.Id, CategoriaId = catEsportes.Id, SubCategoriaId = subArtesMarciais.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/luvas.webp" },
                new Brinquedo { Nome = "Bola Futebol Palmeiras", Descricao = "Oficial para craques.", Preco = 99.90m, Estoque = 50, Ativo = true, MarcaId = nike.Id, CategoriaId = catEsportes.Id, SubCategoriaId = subFutebol.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa7a10.Id, ImagemUrl = "/imagens/produtos/palmeiras.webp" },
                new Brinquedo { Nome = "Bola Futebol Real Madry", Descricao = "Edição especial torcedor.", Preco = 109.90m, Estoque = 40, Ativo = true, MarcaId = nike.Id, CategoriaId = catEsportes.Id, SubCategoriaId = subFutebol.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa7a10.Id, ImagemUrl = "/imagens/produtos/realmadry.webp" },
                new Brinquedo { Nome = "Kit 2 Traves", Descricao = "Fácil de montar.", Preco = 145.00m, Estoque = 25, Ativo = true, MarcaId = estrela.Id, CategoriaId = catEsportes.Id, SubCategoriaId = subFutebol.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa7a10.Id, ImagemUrl = "/imagens/produtos/kit2traves.webp" },
                new Brinquedo { Nome = "Jogo Basquete Mesa", Descricao = "Escala reduzida divertida.", Preco = 59.90m, Estoque = 35, Ativo = true, MarcaId = estrela.Id, CategoriaId = catEsportes.Id, SubCategoriaId = subBasquete.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa7a10.Id, ImagemUrl = "/imagens/produtos/basquetemesa.webp" },
                new Brinquedo { Nome = "Cesta Basquete Princesas", Descricao = "Ajustável para crianças.", Preco = 129.90m, Estoque = 12, Ativo = true, MarcaId = mattel.Id, CategoriaId = catEsportes.Id, SubCategoriaId = subBasquete.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa5a7.Id, ImagemUrl = "/imagens/produtos/cestaprincesas.webp" },
                new Brinquedo { Nome = "Bola Basquete Azul", Descricao = "Resistente para quadras.", Preco = 85.00m, Estoque = 45, Ativo = true, MarcaId = spalding.Id, CategoriaId = catEsportes.Id, SubCategoriaId = subBasquete.Id, PersonagemId = semPers.Id, FaixaEtariaId = faixa7a10.Id, ImagemUrl = "/imagens/produtos/bolaazul.webp" }
            };

            context.Brinquedos.AddRange(produtos);
            await context.SaveChangesAsync();
        }
    }
}