using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using FabricaDeSorrisos.UI.ViewModels.Brinquedos;
using FabricaDeSorrisos.UI.Api;
using FabricaDeSorrisos.UI.Services;
using FabricaDeSorrisos.UI.Models.Services;

namespace FabricaDeSorrisos.UI.Forms.Brinquedos
{
    public partial class frmEditarBrinquedos : Form
    {
        private BrinquedoViewModel _model;
        private static readonly HttpClient _imgClient = new HttpClient();
        public string? NovaImagemBase64 { get; private set; }
        public string? NovaImagemNomeArquivo { get; private set; }
        private readonly CatalogService _catalog = new CatalogService();
        private readonly SubCategoriaService _subService = new SubCategoriaService();
        private List<CatalogService.MarcaItem> _marcas = new();
        private List<CatalogService.CategoriaItem> _categorias = new();
        private List<CatalogService.FaixaEtariaItem> _faixas = new();
        private List<CatalogService.PersonagemItem> _personagens = new();
        private List<SubCategoriaService.SubCategoriaItem> _subcats = new();

        public frmEditarBrinquedos()
        {
            InitializeComponent();
            WireEvents();
        }

        public frmEditarBrinquedos(BrinquedoViewModel model)
        {
            InitializeComponent();
            _model = model;
            WireEvents();
            PreencherCampos();
        }

        private void WireEvents()
        {
            guna2CircleButton1.Click += Guna2CircleButton1_Click;
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.Handled = true;
                    Close();
                }
            };
        }

        private void Guna2CircleButton1_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private async void PreencherCampos()
        {
            if (_model == null) return;
            txtNome.Text = _model.Nome;
            txtEstoque.Text = _model.Estoque.ToString();
            txtPreço.Text = _model.Preco.ToString("0.00");
            txtDescricao.Text = _model.Descricao ?? string.Empty;

            _marcas = await _catalog.GetMarcasAsync();
            _categorias = await _catalog.GetCategoriasAsync();
            _faixas = await _catalog.GetFaixasAsync();
            _personagens = await _catalog.GetPersonagensAsync();
            _subcats = await _subService.GetAllAsync();

            cbMarca.DisplayMember = "Nome";
            cbMarca.ValueMember = "Id";
            cbMarca.DataSource = _marcas;
            var marcaIdx = _marcas.FindIndex(m => string.Equals(m.Nome, _model.Marca, StringComparison.OrdinalIgnoreCase));
            if (marcaIdx >= 0) cbMarca.SelectedIndex = marcaIdx;

            cbCategoria.DisplayMember = "Nome";
            cbCategoria.ValueMember = "Id";
            cbCategoria.DataSource = _categorias;
            var catIdx = _categorias.FindIndex(c => string.Equals(c.Nome, _model.Categoria, StringComparison.OrdinalIgnoreCase));
            if (catIdx >= 0) cbCategoria.SelectedIndex = catIdx;
            cbCategoria.SelectedIndexChanged += (s, e) => AtualizarSubCategorias();

            cbFaixaEtaria.DisplayMember = "Descricao";
            cbFaixaEtaria.ValueMember = "Id";
            cbFaixaEtaria.DataSource = _faixas;
            var faixaIdx = _faixas.FindIndex(f => string.Equals(f.Descricao, _model.FaixaEtaria, StringComparison.OrdinalIgnoreCase));
            if (faixaIdx >= 0) cbFaixaEtaria.SelectedIndex = faixaIdx;

            cbPersonagem.DisplayMember = "Nome";
            cbPersonagem.ValueMember = "Id";
            cbPersonagem.DataSource = _personagens;
            var persIdx = _personagens.FindIndex(p => string.Equals(p.Nome, _model.Personagem ?? "", StringComparison.OrdinalIgnoreCase));
            if (persIdx >= 0) cbPersonagem.SelectedIndex = persIdx;

            AtualizarSubCategorias();
            if (!string.IsNullOrWhiteSpace(_model.SubCategoria))
            {
                var subIdx = (cbSubCategoria.DataSource as List<SubCategoriaService.SubCategoriaItem>)?
                    .FindIndex(s => string.Equals(s.Nome, _model.SubCategoria, StringComparison.OrdinalIgnoreCase)) ?? -1;
                if (subIdx >= 0) cbSubCategoria.SelectedIndex = subIdx;
            }

            await CarregarImagemAtualAsync();
            btnTrocarImagem.Click += BtnTrocarImagem_Click;
        }

        private async Task CarregarImagemAtualAsync()
        {
            if (string.IsNullOrWhiteSpace(_model?.ImagemUrl))
            {
                picImagem.Image = null;
                return;
            }
            var path = _model.ImagemUrl.Trim();
            if (path.StartsWith("~/")) path = path.Substring(1);
            if (!path.StartsWith("/")) path = "/" + path;
            var apiBase = new Uri(ApiSettings.BaseUrl);
            var apiHost = $"{apiBase.Scheme}://{apiBase.Host}{(apiBase.IsDefaultPort ? "" : ":" + apiBase.Port)}";
            var candidates = _model.ImagemUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? new[] { _model.ImagemUrl }
                : new[]
                {
                    ApiSettings.WebBaseUrl.TrimEnd('/') + path,
                    ApiSettings.WebBaseUrlHttps.TrimEnd('/') + path,
                    apiHost.TrimEnd('/') + "/static" + path
                };
            foreach (var url in candidates)
            {
                try
                {
                    var bytes = await _imgClient.GetByteArrayAsync(url);
                    using var ms = new MemoryStream(bytes);
                    using var temp = Image.FromStream(ms);
                    picImagem.Image = new Bitmap(temp);
                    break;
                }
                catch
                {
                }
            }
        }

        private void BtnTrocarImagem_Click(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Imagens|*.jpg;*.jpeg;*.png;*.webp;*.bmp",
                Title = "Selecione a nova imagem"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var bytes = File.ReadAllBytes(dlg.FileName);
                using var ms = new MemoryStream(bytes);
                using var temp = Image.FromStream(ms);
                picImagem.Image = new Bitmap(temp);
                NovaImagemBase64 = Convert.ToBase64String(bytes);
                NovaImagemNomeArquivo = Path.GetFileName(dlg.FileName);
            }
        }

        private async void btnEditar_Click(object? sender, EventArgs e)
        {
            if (_model == null) return;
            if (!decimal.TryParse(txtPreço.Text, out var preco)) preco = _model.Preco;
            if (!int.TryParse(txtEstoque.Text, out var estoque)) estoque = _model.Estoque;

            var service = new FabricaDeSorrisos.UI.Models.Services.BrinquedoService();
            var req = new FabricaDeSorrisos.UI.Models.Services.BrinquedoService.UpdateBrinquedoRequest
            {
                Nome = txtNome.Text?.Trim(),
                Descricao = txtDescricao.Text?.Trim(),
                Preco = preco,
                Estoque = estoque,
                MarcaId = cbMarca.SelectedValue is int m ? m : (cbMarca.SelectedValue != null ? Convert.ToInt32(cbMarca.SelectedValue) : null),
                CategoriaId = cbCategoria.SelectedValue is int c ? c : (cbCategoria.SelectedValue != null ? Convert.ToInt32(cbCategoria.SelectedValue) : null),
                FaixaEtariaId = cbFaixaEtaria.SelectedValue is int f ? f : (cbFaixaEtaria.SelectedValue != null ? Convert.ToInt32(cbFaixaEtaria.SelectedValue) : null),
                PersonagemId = cbPersonagem.SelectedValue is int p ? p : (cbPersonagem.SelectedValue != null ? Convert.ToInt32(cbPersonagem.SelectedValue) : null),
                SubCategoriaId = cbSubCategoria.SelectedValue is int s ? s : (cbSubCategoria.SelectedValue != null ? Convert.ToInt32(cbSubCategoria.SelectedValue) : null),
                ImagemBase64 = NovaImagemBase64,
                ImagemNomeArquivo = NovaImagemNomeArquivo
            };

            var ok = await service.AtualizarAsync(_model.Id, req);
            if (!ok)
            {
                MessageBox.Show("Falha ao salvar alterações do brinquedo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Alterações salvas com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void AtualizarSubCategorias()
        {
            try
            {
                int categoriaId = cbCategoria.SelectedValue is int i ? i :
                    (cbCategoria.SelectedValue != null ? Convert.ToInt32(cbCategoria.SelectedValue) : 0);
                var lista = _subcats.Where(s => s.CategoriaId == categoriaId).ToList();
                cbSubCategoria.DisplayMember = "Nome";
                cbSubCategoria.ValueMember = "Id";
                cbSubCategoria.DataSource = lista;
            }
            catch
            {
                cbSubCategoria.DataSource = _subcats;
            }
        }
    }
}
