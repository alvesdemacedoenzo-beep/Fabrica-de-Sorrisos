using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using FabricaDeSorrisos.UI.Services;
using FabricaDeSorrisos.UI.Forms.Marcas;

namespace FabricaDeSorrisos.UI.Forms
{
    public partial class frmMarcas : Form
    {
        private readonly CatalogService _catalogService = new CatalogService();
        private readonly MarcaService _marcaService = new MarcaService();
        private List<CatalogService.MarcaItem> _dados = new();

        public frmMarcas()
        {
            InitializeComponent();
            Load += frmMarcas_Load;
            btnCriarMarca.Click += BtnCriarMarca_Click;
            btnEditarMarca.Click += BtnEditarMarca_Click;
            btnExcluirMarca.Click += BtnExcluirMarca_Click;
            btnBuscar.Click += BtnBuscar_Click;
            btnFiltrar.Click += BtnFiltrar_Click;
            btnResetar.Click += BtnResetar_Click;
            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnBuscar_Click(s, e); };
            cbFiltro.Items.Clear();
            cbFiltro.Items.AddRange(new object[] { "Todos", "ID", "Nome" });
            if (cbFiltro.Items.Count > 0) cbFiltro.SelectedIndex = 0;
        }

        private async void frmMarcas_Load(object? sender, EventArgs e)
        {
            ConfigurarGrid();
            await CarregarDados();
            AjustarVisibilidadeColunas("Todos");
        }

        private void ConfigurarGrid()
        {
            guna2DataGridView1.ReadOnly = true;
            guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            guna2DataGridView1.MultiSelect = false;
            guna2DataGridView1.AutoGenerateColumns = false;
            guna2DataGridView1.RowTemplate.Height = 64;
            guna2DataGridView1.ColumnHeadersHeight = 32;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            guna2DataGridView1.Columns.Clear();
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                FillWeight = 12,
                ReadOnly = true
            };
            colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var colNome = new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                DataPropertyName = "Nome",
                HeaderText = "Nome da Marca",
                FillWeight = 88,
                ReadOnly = true,
            };
            guna2DataGridView1.Columns.Add(colId);
            guna2DataGridView1.Columns.Add(colNome);
        }

        private async Task CarregarDados(string? termo = null)
        {
            var marcas = await _catalogService.GetMarcasAsync();
            _dados = marcas;
            if (!string.IsNullOrWhiteSpace(termo))
            {
                int id;
                bool isId = int.TryParse(termo, out id);
                _dados = _dados.Where(m => (isId && m.Id == id) || (!isId && (m.Nome?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false))).ToList();
            }
            RefreshGrid(_dados);
        }

        private void RefreshGrid(List<CatalogService.MarcaItem> items)
        {
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = items.Select(m => new { m.Id, m.Nome }).ToList();
        }

        private async void BtnCriarMarca_Click(object? sender, EventArgs e)
        {
            using var dlg = new frmCriarMarca();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                await CarregarDados();
            }
        }

        private int? ObterIdSelecionado()
        {
            if (guna2DataGridView1.CurrentRow == null) return null;
            var row = guna2DataGridView1.CurrentRow.DataBoundItem;
            if (row is CatalogService.MarcaItem item) return item.Id;
            var idCell = guna2DataGridView1.CurrentRow.Cells["Id"]?.Value;
            if (idCell is int id) return id;
            if (int.TryParse(idCell?.ToString(), out var parsed)) return parsed;
            return null;
        }

        private string? ObterNomeSelecionado()
        {
            if (guna2DataGridView1.CurrentRow == null) return null;
            var row = guna2DataGridView1.CurrentRow.DataBoundItem;
            if (row is CatalogService.MarcaItem item) return item.Nome;
            return guna2DataGridView1.CurrentRow.Cells["Nome"]?.Value?.ToString();
        }

        private async void BtnEditarMarca_Click(object? sender, EventArgs e)
        {
            var id = ObterIdSelecionado();
            if (id == null)
            {
                MessageBox.Show("Selecione uma marca.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nome = ObterNomeSelecionado() ?? string.Empty;
            using var dlg = new frmEditarMarca(id.Value, nome);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                await CarregarDados();
            }
        }

        private async void BtnExcluirMarca_Click(object? sender, EventArgs e)
        {
            var id = ObterIdSelecionado();
            if (id == null)
            {
                MessageBox.Show("Selecione uma marca.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nome = ObterNomeSelecionado() ?? $"ID {id}";
            var confirm = MessageBox.Show(
                $"Deseja excluir a Marca \"{nome}\"?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var ok = await _marcaService.DeleteAsync(id.Value);
            if (!ok)
            {
                MessageBox.Show("Não foi possível excluir a marca. Verifique a API.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await CarregarDados();
        }

        private IEnumerable<CatalogService.MarcaItem> AplicarFiltroAtual()
        {
            var termo = txtBusca.Text?.Trim() ?? "";
            var campo = (cbFiltro.SelectedItem as string) ?? "Todos";
            bool temTermo = !string.IsNullOrWhiteSpace(termo);
            var items = _dados.AsEnumerable();

            if (!temTermo && campo == "Todos") return items;

            if (campo == "Todos")
            {
                if (!temTermo) return items;
                int idVal;
                bool idOk = int.TryParse(termo, out idVal);
                return items.Where(m => (idOk && m.Id == idVal) || (m.Nome?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false));
            }

            return campo switch
            {
                "ID" => (int.TryParse(termo, out var id) ? items.Where(m => m.Id == id) : items),
                "Nome" => items.Where(m => m.Nome?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false),
                _ => items
            };
        }

        private void BtnFiltrar_Click(object? sender, EventArgs e)
        {
            var filtrados = AplicarFiltroAtual().ToList();
            RefreshGrid(filtrados);
            AjustarVisibilidadeColunas((cbFiltro.SelectedItem as string) ?? "Todos");
        }

        private async void BtnBuscar_Click(object? sender, EventArgs e)
        {
            var termo = txtBusca.Text?.Trim();
            await CarregarDados(termo);
            var filtrados = AplicarFiltroAtual().ToList();
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

        private void AjustarVisibilidadeColunas(string campo)
        {
            if (campo == "Todos")
            {
                foreach (DataGridViewColumn c in guna2DataGridView1.Columns) c.Visible = true;
                return;
            }

            foreach (DataGridViewColumn c in guna2DataGridView1.Columns) c.Visible = false;
            var target = campo == "ID" ? "Id" : "Nome";
            foreach (DataGridViewColumn c in guna2DataGridView1.Columns)
            {
                if (string.Equals(c.DataPropertyName, target, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.Name, target, StringComparison.OrdinalIgnoreCase))
                {
                    c.Visible = true;
                    break;
                }
            }
        }
    }
}
