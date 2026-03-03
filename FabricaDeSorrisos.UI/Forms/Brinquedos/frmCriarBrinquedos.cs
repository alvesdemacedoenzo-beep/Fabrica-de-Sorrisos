using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FabricaDeSorrisos.UI.Models.Services;
using FabricaDeSorrisos.UI.Services;
using System.Globalization;
using Guna.UI2.WinForms;

namespace FabricaDeSorrisos.UI
{
    public partial class frmCriarBrinquedos : Form
    {
        private readonly CatalogService _catalogService = new CatalogService();
        private readonly BrinquedoService _brinquedoService = new BrinquedoService();
        private PictureBox _imgPreview;
        private string? _imagemPath;
        private System.Collections.Generic.List<SubCategoriaService.SubCategoriaItem> _todasSubCats = new();

        public frmCriarBrinquedos()
        {
            InitializeComponent();
            Load += FrmCriarBrinquedos_Load;
            btnCriar.Click += BtnCriar_Click;
            btnFechar.Click += BtnFechar_Click;
            btnEscolherImagem.Click += BtnEscolherImagem_Click;
            _imgPreview = new PictureBox
            {
                Width = 260,
                Height = 160,
                Left = 666,
                Top = 110,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            Controls.Add(_imgPreview);
        }

        private async void FrmCriarBrinquedos_Load(object sender, EventArgs e)
        {
            var marcas = await _catalogService.GetMarcasAsync();
            cbMarca.DisplayMember = "Nome";
            cbMarca.ValueMember = "Id";
            cbMarca.DataSource = marcas;

            var categorias = await _catalogService.GetCategoriasAsync();
            cbCategoria.DisplayMember = "Nome";
            cbCategoria.ValueMember = "Id";
            cbCategoria.DataSource = categorias;
            cbCategoria.SelectedIndexChanged += (s, ev) => AtualizarSubCategoriasFiltradas();

            var faixas = await _catalogService.GetFaixasAsync();
            cbFaixaEtaria.DisplayMember = "Descricao";
            cbFaixaEtaria.ValueMember = "Id";
            cbFaixaEtaria.DataSource = faixas;

            var personagens = await _catalogService.GetPersonagensAsync();
            cbPersonagem.DisplayMember = "Nome";
            cbPersonagem.ValueMember = "Id";
            cbPersonagem.DataSource = personagens;
            txtPreco.KeyPress += TxtPreco_KeyPress;

            var subService = new SubCategoriaService();
            _todasSubCats = await subService.GetAllAsync();
            cbSubCategorias.DisplayMember = "Nome";
            cbSubCategorias.ValueMember = "Id";
            AtualizarSubCategoriasFiltradas();
        }

        private void BtnFechar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void TxtPreco_KeyPress(object? sender, KeyPressEventArgs e)
        {
            var ch = e.KeyChar;
            if (char.IsControl(ch)) return;
            if (ch == '.')
            {
                e.Handled = true;
                return;
            }
            if (ch == ',')
            {
                var tbText = (sender as Control)?.Text ?? "";
                if (tbText.Contains(",")) e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch)) e.Handled = true;
        }

        private async void BtnCriar_Click(object? sender, EventArgs e)
        {
            var nome = txtNomeProduto.Text?.Trim();
            var desc = txtDescricao.Text?.Trim();
            var precoTexto = txtPreco.Text?.Trim() ?? "";
            if (precoTexto.Any(char.IsLetter))
            {
                MessageBox.Show("Preço deve conter apenas números e vírgula.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (precoTexto.Contains("."))
            {
                MessageBox.Show("Use vírgula como separador decimal no preço (ex: 120,50).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var culture = new CultureInfo("pt-BR");
            var precoOk = decimal.TryParse(precoTexto, NumberStyles.Number, culture, out var preco) && preco > 0;
            var estoqueOk = int.TryParse(txtEstoque.Text, out var estoque) && estoque >= 0;
            var marcaId = cbMarca.SelectedValue as int? ?? (cbMarca.SelectedValue is null ? (int?)null : Convert.ToInt32(cbMarca.SelectedValue));
            var categoriaId = cbCategoria.SelectedValue as int? ?? (cbCategoria.SelectedValue is null ? (int?)null : Convert.ToInt32(cbCategoria.SelectedValue));
            var faixaId = cbFaixaEtaria.SelectedValue as int? ?? (cbFaixaEtaria.SelectedValue is null ? (int?)null : Convert.ToInt32(cbFaixaEtaria.SelectedValue));
            var personagemId = cbPersonagem.SelectedValue as int? ?? (cbPersonagem.SelectedValue is null ? (int?)null : Convert.ToInt32(cbPersonagem.SelectedValue));

            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(desc) ||
                !precoOk ||
                !estoqueOk ||
                marcaId is null ||
                categoriaId is null ||
                faixaId is null ||
                personagemId is null ||
                string.IsNullOrWhiteSpace(_imagemPath))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? base64 = null;
            string? fileName = null;
            try
            {
                var bytes = File.ReadAllBytes(_imagemPath!);
                base64 = Convert.ToBase64String(bytes);
                fileName = Path.GetFileName(_imagemPath!);
            }
            catch { }

            var req = new BrinquedoService.CreateBrinquedoRequest
            {
                Nome = nome!,
                Descricao = desc!,
                Preco = preco,
                Estoque = estoque,
                MarcaId = marcaId!.Value,
                CategoriaId = categoriaId!.Value,
                FaixaEtariaId = faixaId!.Value,
                PersonagemId = personagemId!.Value,
                Ativo = true,
                ImagemBase64 = base64,
                ImagemNomeArquivo = fileName
            };

            var ok = await _brinquedoService.CriarAsync(req);
            if (!ok)
            {
                MessageBox.Show("Não foi possível criar o brinquedo. Verifique se a API possui o endpoint de criação.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Brinquedo criado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void BtnEscolherImagem_Click(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "Imagens|*.jpg;*.jpeg;*.png;*.webp;*.bmp;*.gif",
                Title = "Escolher Imagem do Produto"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _imagemPath = dlg.FileName;
                try
                {
                    _imgPreview.Image = Image.FromFile(_imagemPath);
                }
                catch
                {
                    _imgPreview.Image = null;
                }
            }
        }

        private void lblNomeProduto_Click(object sender, EventArgs e) { }
        private void lblPersonagem_Click(object sender, EventArgs e) { }

        private void AtualizarSubCategoriasFiltradas()
        {
            try
            {
                int categoriaId = 0;
                if (cbCategoria.SelectedValue != null)
                {
                    categoriaId = cbCategoria.SelectedValue is int i ? i : Convert.ToInt32(cbCategoria.SelectedValue);
                }
                var lista = _todasSubCats.Where(s => s.CategoriaId == categoriaId).ToList();
                cbSubCategorias.DataSource = lista;
            }
            catch
            {
                cbSubCategorias.DataSource = _todasSubCats;
            }
        }
    }
}
