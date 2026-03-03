# Fábrica de Sorrisos

Plataforma completa para catálogo e gestão de brinquedos, composta por:
- Aplicativo Desktop (Windows Forms) para administração
- Site Web (ASP.NET Core MVC) com área pública e área Admin
- API REST (ASP.NET Core) com autenticação JWT
- Camadas de Aplicação, Domínio e Infraestrutura (EF Core, Identity)

## Visão Geral
- Catálogo com categorias, subcategorias, marcas, personagens e faixas etárias
- Gestão de usuários, produtos, imagens e status (ativo/inativo)
- Busca, filtros e paginação
- Upload e exibição de imagens
- Autenticação via JWT na API e consumo pelo Desktop/Web

## Arquitetura e Projetos
- FabricaDeSorrisos.Domain: entidades e regras básicas
- FabricaDeSorrisos.Application: contratos de serviços e DTOs
- FabricaDeSorrisos.Infrastructure: EF Core, Identity, repositórios, serviços
- FabricaDeSorrisos.Api: API REST
- FabricaDeSorrisos.Web: site MVC com área Admin
- FabricaDeSorrisos.UI: aplicativo Desktop Windows Forms

## Requisitos
- .NET 10 (SDK preview)
- SQL Server LocalDB (connection string padrão já definida)
- Windows para o App Desktop

## Configuração de URLs
- API: http://localhost:5259
- Web: http://localhost:5179
- APISettings no Desktop:
  - BaseUrl: http://localhost:5259/api/
  - WebBaseUrl: http://localhost:5179
  - WebBaseUrlHttps: https://localhost:5179

## Como Executar
1. API
   - Entre em FabricaDeSorrisos.Api
   - `dotnet run`
   - Swagger habilitado em desenvolvimento
2. Web
   - Entre em FabricaDeSorrisos.Web
   - `dotnet run`
   - A área Admin fica em /Admin
3. Desktop (UI)
   - Entre em FabricaDeSorrisos.UI
   - `dotnet run`
   - Consome a API e mostra a interface de gestão

## Banco de Dados
- ConnectionStrings definidas em appsettings.Development.json (API/Web)
- Migrações e seed efetuados pela camada de Infraestrutura

## Autenticação
- API com JWT (chave, issuer, audience e expiração em appsettings.Development.json)
- Desktop injeta Bearer Token em chamadas (ApiClient + UserSession)
- CORS liberado para o host do Web/Dev

## Principais Endpoints
- Brinquedos
  - GET /api/brinquedos?pageIndex=&pageSize=&termoBusca=&incluirInativos=
  - GET /api/brinquedos/{id}
  - POST /api/brinquedos
  - PUT /api/brinquedos/{id}
  - Campos de atualização: Nome, Descricao, Preco, Estoque, Ativo, MarcaId, CategoriaId, FaixaEtariaId, PersonagemId, SubCategoriaId, ImagemBase64, ImagemNomeArquivo
- Categorias
  - GET /api/categorias
  - POST /api/categorias
  - PUT /api/categorias/{id}
  - DELETE /api/categorias/{id}
- Subcategorias
  - GET /api/subcategorias
  - POST /api/subcategorias
  - PUT /api/subcategorias/{id}
  - DELETE /api/subcategorias/{id}
- Marcas
  - GET /api/marcas
- Personagens
  - GET /api/personagens
- Usuários
  - GET /api/usuarios
  - PUT /api/usuarios/{id}

## Funcionalidades do Desktop
- Brinquedos
  - Listagem com ordenação, filtros e busca
  - Criar/Editar com imagem, preço, estoque, vínculos de catálogo
  - Alternar status ativo/inativo
- Catálogo
  - Categorias e Subcategorias com busca e criação
  - Marcas e Personagens
- Usuários
  - Listagem por perfis, edição e remoção, validações

## Convenções de UI
- Windows Forms com Guna.UI2.WinForms
- Grids com alinhamento consistente, formatação monetária (“R$” e duas casas)
- Cores de seleção padronizadas
- Campos de preço aceitando dígitos e vírgula

## Boas Práticas e Segurança
- Não guardar segredos no repositório (usar appsettings/segredos locais)
- JWT com expiração e validações
- CORS restrito a origens conhecidas durante desenvolvimento

## Comandos Úteis
- Build: `dotnet build FabricaDeSorrisos.UI/FabricaDeSorrisos.UI.csproj`
- Build API: `dotnet build FabricaDeSorrisos.Api/FabricaDeSorrisos.Api.csproj`
- Build Web: `dotnet build FabricaDeSorrisos.Web/FabricaDeSorrisos.Web.csproj`
- Run API: `dotnet run --project FabricaDeSorrisos.Api`
- Run Web: `dotnet run --project FabricaDeSorrisos.Web`
- Run UI: `dotnet run --project FabricaDeSorrisos.UI`

## Problemas Comuns
- API não iniciada: o Desktop e o Web dependem da API em http://localhost:5259
- Imagens não aparecem: verifique as URLs configuradas e o arquivo salvo
- CORS bloqueando: ajuste as origens permitidas no appsettings e política CORS

## Roadmap
- Checkout e carrinho no Web
- Relatórios e dashboards na área Admin
- Testes automatizados