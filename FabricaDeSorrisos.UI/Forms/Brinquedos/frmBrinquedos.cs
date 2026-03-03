using FabricaDeSorrisos.UI.Models.Services;
using FabricaDeSorrisos.UI.ViewModels.Brinquedos;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
using FabricaDeSorrisos.UI.Api;
using System.Collections.Generic;

namespace FabricaDeSorrisos.UI.Forms
{
    public partial class frmBrinquedos : Form
    {
        private readonly BrinquedoService _service;
        private static readonly HttpClient _imgClient = new HttpClient();
        private static readonly string _webBaseUrl = ApiSettings.WebBaseUrl;
        private static readonly string _webBaseUrlHttps = ApiSettings.WebBaseUrlHttps;
        private static readonly ConcurrentDictionary<string, Image> _imageCache = new();
        private static readonly Image _placeholder = new Bitmap(1, 1);
        private List<BrinquedoViewModel> _dados = new();

        public frmBrinquedos()
        {
            InitializeComponent();
            _service = new BrinquedoService();
            Load += frmBrinquedos_Load;
            btnCriarBrinquedo.Click += BtnCriar_Click;
            btnEditarBrinquedo.Click += BtnEditar_Click;
            btnStatusBrinquedo.Click += BtnStatus_Click;
            guna2DataGridView1.DataBindingComplete += Grid_DataBindingComplete;
            btnBuscar.Click += BtnBuscar_Click;
            btnFiltrar.Click += BtnFiltrar_Click;
            btnResetar.Click += BtnResetar_Click;
            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnBuscar_Click(s, e); };
            cbFiltro.Items.Clear();
            cbFiltro.Items.AddRange(new object[] { "Todos", "ID", "Nome", "Marca", "Categoria", "Subcategoria", "Personagem", "Faixa Etária", "Status" });
            if (cbFiltro.Items.Count > 0) cbFiltro.SelectedIndex = 0;
        }

        private async void frmBrinquedos_Load(object sender, EventArgs e)
        {
            ConfigurarGrid(guna2DataGridView1);
            await CarregarDados();
        }

        private async Task CarregarDados(string? termo = null)
        {
            var paged = await _service.ListarAsync(pageIndex: 1, pageSize: 100, termo: termo, incluirInativos: true);
            _dados = paged.Items;
            RefreshGrid(_dados);

            var qtd = paged.TotalCount;
            var estoqueTotal = paged.Items.Sum(i => i.Estoque);
        }

        private void ConfigurarGrid(Guna2DataGridView grid)
        {
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoGenerateColumns = false;
            grid.RowTemplate.Height = 64;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.CellFormatting += Grid_CellFormatting;

            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.Id), HeaderText = "ID", FillWeight = 6, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            var imgCol = new DataGridViewImageColumn
            {
                HeaderText = "Imagem",
                Name = "colImagem",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                FillWeight = 10
            };
            imgCol.DefaultCellStyle.NullValue = _placeholder;
            grid.Columns.Add(imgCol);
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.Nome), HeaderText = "Nome", FillWeight = 16, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.Estoque), HeaderText = "Estoque", FillWeight = 8, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            var colPreco = new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.Preco), HeaderText = "Preço", FillWeight = 8, ValueType = typeof(decimal) };
            colPreco.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPreco.DefaultCellStyle.Format = "C2";
            grid.Columns.Add(colPreco);
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.Descricao), HeaderText = "Descrição", FillWeight = 20, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.FaixaEtaria), HeaderText = "Faixa Etária", FillWeight = 8, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.Marca), HeaderText = "Marca", FillWeight = 8, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.Categoria), HeaderText = "Categoria", FillWeight = 8, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.SubCategoria), HeaderText = "Subcategoria", FillWeight = 8, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.Personagem), HeaderText = "Personagem", FillWeight = 8, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BrinquedoViewModel.Status), HeaderText = "Status", FillWeight = 6, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
        }

        private void RefreshGrid(List<BrinquedoViewModel> items)
        {
            var ordenados = items.OrderBy(i => i.Id).ToList();
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = ordenados;
        }

        private IEnumerable<BrinquedoViewModel> AplicarFiltroAtual()
        {
            var termo = txtBusca.Text?.Trim() ?? "";
            var campo = (cbFiltro.SelectedItem as string) ?? "Todos";
            bool temTermo = !string.IsNullOrWhiteSpace(termo);

            IEnumerable<BrinquedoViewModel> items = _dados;

            if (!temTermo && (campo == "Todos" || string.IsNullOrWhiteSpace(campo)))
                return items;

            StringComparison cmp = StringComparison.OrdinalIgnoreCase;

            if (campo == "Todos")
            {
                if (!temTermo) return items;
                int idVal;
                bool idOk = int.TryParse(termo, out idVal);
                return items.Where(b =>
                    (idOk && b.Id == idVal) ||
                    (b.Nome?.Contains(termo, cmp) ?? false) ||
                    (b.Marca?.Contains(termo, cmp) ?? false) ||
                    (b.Categoria?.Contains(termo, cmp) ?? false) ||
                    (b.SubCategoria?.Contains(termo, cmp) ?? false) ||
                    (b.Personagem?.Contains(termo, cmp) ?? false) ||
                    (b.FaixaEtaria?.Contains(termo, cmp) ?? false) ||
                    (b.Status?.Contains(termo, cmp) ?? false));
            }

            return campo switch
            {
                "ID" => (int.TryParse(termo, out var id) ? items.Where(b => b.Id == id) : items),
                "Nome" => items.Where(b => b.Nome?.Contains(termo, cmp) ?? false),
                "Marca" => items.Where(b => b.Marca?.Contains(termo, cmp) ?? false),
                "Categoria" => items.Where(b => b.Categoria?.Contains(termo, cmp) ?? false),
                "Subcategoria" => items.Where(b => b.SubCategoria?.Contains(termo, cmp) ?? false),
                "Personagem" => items.Where(b => b.Personagem?.Contains(termo, cmp) ?? false),
                "Faixa Etária" => items.Where(b => b.FaixaEtaria?.Contains(termo, cmp) ?? false),
                "Status" => items.Where(b => b.Status?.Contains(termo, cmp) ?? false),
                _ => items
            };
        }

        private async void BtnBuscar_Click(object? sender, EventArgs e)
        {
            var termo = txtBusca.Text?.Trim();
            await CarregarDados(termo);
            var filtrados = AplicarFiltroAtual().ToList();
            RefreshGrid(filtrados);
        }

        private void BtnFiltrar_Click(object? sender, EventArgs e)
        {
            var filtrados = AplicarFiltroAtual().ToList();
            if (filtrados.Count == 0)
            {
                var termo = txtBusca.Text?.Trim();
                var _ = CarregarDados(termo);
                filtrados = AplicarFiltroAtual().ToList();
            }
            RefreshGrid(filtrados);
            AjustarVisibilidadeColunas((cbFiltro.SelectedItem as string) ?? "Todos");
        }

        private async void BtnResetar_Click(object? sender, EventArgs e)
        {
            txtBusca.Text = "";
            if (cbFiltro.Items.Count > 0) cbFiltro.SelectedIndex = 0;
            await CarregarDados();
            AjustarVisibilidadeColunas("Todos");
        }

        private string GetPropertyNameByCampo(string campo)
        {
            return campo switch
            {
                "ID" => nameof(BrinquedoViewModel.Id),
                "Nome" => nameof(BrinquedoViewModel.Nome),
                "Marca" => nameof(BrinquedoViewModel.Marca),
                "Categoria" => nameof(BrinquedoViewModel.Categoria),
                "Subcategoria" => nameof(BrinquedoViewModel.SubCategoria),
                "Personagem" => nameof(BrinquedoViewModel.Personagem),
                "Faixa Etária" => nameof(BrinquedoViewModel.FaixaEtaria),
                "Status" => nameof(BrinquedoViewModel.Status),
                _ => "Todos"
            };
        }

        private void AjustarVisibilidadeColunas(string campo)
        {
            var prop = GetPropertyNameByCampo(campo);
            if (prop == "Todos")
            {
                foreach (DataGridViewColumn c in guna2DataGridView1.Columns) c.Visible = true;
                return;
            }
            foreach (DataGridViewColumn c in guna2DataGridView1.Columns) c.Visible = false;
            foreach (DataGridViewColumn c in guna2DataGridView1.Columns)
            {
                if (string.Equals(c.DataPropertyName, prop, StringComparison.OrdinalIgnoreCase))
                {
                    c.Visible = true;
                    break;
                }
            }
        }

        private async void Grid_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            await CarregarImagensAsync(guna2DataGridView1);
        }

        private void Grid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            var grid = sender as Guna2DataGridView;
            if (grid == null) return;
            var col = grid.Columns[e.ColumnIndex];
            if (string.Equals(col.DataPropertyName, nameof(BrinquedoViewModel.Preco), StringComparison.OrdinalIgnoreCase))
            {
                if (e.Value is decimal d)
                {
                    e.Value = $"R$ {d:N2}";
                    e.FormattingApplied = true;
                }
                else if (e.Value != null)
                {
                    decimal val;
                    if (decimal.TryParse(e.Value.ToString(), out val))
                    {
                        e.Value = $"R$ {val:N2}";
                        e.FormattingApplied = true;
                    }
                }
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private async Task CarregarImagensAsync(Guna2DataGridView grid)
        {
            var imgColName = "colImagem";
            // Garante a existência da coluna de imagem mesmo se não tiver sido criada
            if (!grid.Columns.Contains(imgColName))
            {
                var newImg = new DataGridViewImageColumn
                {
                    HeaderText = "Imagem",
                    Name = imgColName,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    Width = 90
                };
                // insere como primeira coluna
                grid.Columns.Insert(0, newImg);
            }

            // Obtém o índice da coluna de imagem de forma segura
            int imgIndex = grid.Columns[imgColName].Index;

            foreach (DataGridViewRow row in grid.Rows)
            {
                var vm = row.DataBoundItem as BrinquedoViewModel;
                if (vm == null) continue;
                if (string.IsNullOrWhiteSpace(vm.ImagemUrl))
                {
                    row.Cells[imgIndex].Value = null;
                    continue;
                }
                var path = vm.ImagemUrl.Trim();
                if (path.StartsWith("~/")) path = path.Substring(1); // normaliza ~/ para /
                if (!path.StartsWith("/")) path = "/" + path;       // garante barra inicial
                try
                {
                    // Base da API sem o /api/ (para /static)
                    var apiBase = new Uri(ApiSettings.BaseUrl);
                    var apiHost = $"{apiBase.Scheme}://{apiBase.Host}{(apiBase.IsDefaultPort ? "" : ":" + apiBase.Port)}";

                    var candidates = vm.ImagemUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                        ? new[] { vm.ImagemUrl }
                        : new[]
                        {
                            _webBaseUrl.TrimEnd('/') + path,
                            _webBaseUrlHttps.TrimEnd('/') + path,
                            apiHost.TrimEnd('/') + "/static" + path
                        };

                    Image? img = null;
                    foreach (var url in candidates)
                    {
                        if (_imageCache.TryGetValue(url, out var cached))
                        {
                            img = cached;
                            break;
                        }
                        try
                        {
                            var bytes = await _imgClient.GetByteArrayAsync(url);
                            using var ms = new MemoryStream(bytes);
                            using var temp = Image.FromStream(ms);
                            img = new Bitmap(temp);
                            _imageCache[url] = img;
                            break;
                        }
                        catch
                        {
                            // tenta próximo candidato
                        }
                    }

                    if (img == null) img = _placeholder;
                    row.Cells[imgIndex].Value = img;
                }
                catch
                {
                    row.Cells[imgIndex].Value = _placeholder;
                }
            }
        }

        private void BtnCriar_Click(object? sender, EventArgs e)
        {
            var frm = new frmCriarBrinquedos();
            Hide();
            frm.ShowDialog(this);
            Show();
            _ = CarregarDados();
        }

        private async void BtnStatus_Click(object? sender, EventArgs e)
        {
            var selecionado = ObterSelecionado();
            if (selecionado == null)
            {
                MessageBox.Show("Selecione um brinquedo no grid.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var desejaAtivar = !selecionado.Ativo;
            var acaoTexto = desejaAtivar ? "ativar" : "desativar";

            var confirmar = MessageBox.Show(
                $"Deseja {acaoTexto} o brinquedo '{selecionado.Nome}' (ID {selecionado.Id})?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmar != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool ok;
                if (desejaAtivar)
                    ok = await _service.AtivarAsync(selecionado.Id);
                else
                    ok = await _service.DesativarAsync(selecionado.Id);

                if (!ok)
                {
                    MessageBox.Show("Operação não concluída pela API. Verifique se o endpoint de status está disponível.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao alterar status do brinquedo.\n\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            await CarregarDados();
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            var selecionado = ObterSelecionado();
            if (selecionado == null)
            {
                MessageBox.Show("Selecione um brinquedo no grid.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frm = new Brinquedos.frmEditarBrinquedos(selecionado);
            frm.ShowDialog(this);
            _ = CarregarDados();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblBrinquedosCadastrados_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private BrinquedoViewModel? ObterSelecionado()
        {
            if (guna2DataGridView1.CurrentRow == null) return null;
            return guna2DataGridView1.CurrentRow.DataBoundItem as BrinquedoViewModel;
        }
    }
}
