using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using FabricaDeSorrisos.UI.Services;
using System.Collections.Generic;
using System.Linq;

namespace FabricaDeSorrisos.UI.Forms
{
    public partial class frmCategorias : Form
    {
        private readonly CatalogService _catalogService = new CatalogService();
        private readonly CategoryService _categoryService = new CategoryService();
        private List<CatalogService.CategoriaItem> _dados = new();

        public frmCategorias()
        {
            InitializeComponent();
            Load += frmCategorias_Load;
            btnCriarCategoria.Click += BtnCriarCategoria_Click;
            btnEditarCategoria.Click += BtnEditarCategoria_Click;
            btnExcluirCategoria.Click += BtnExcluirCategoria_Click;
            gridCategorias.CellDoubleClick += (s, e) => BtnEditarCategoria_Click(s, e);
            btnBusca.Click += BtnBuscar_Click;
            btnFiltrar.Click += BtnFiltrar_Click;
            btnResetar.Click += BtnResetar_Click;
            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnBuscar_Click(s, e); };
            cbFiltro.Items.Clear();
            cbFiltro.Items.AddRange(new object[] { "Todos", "ID", "Nome" });
            if (cbFiltro.Items.Count > 0) cbFiltro.SelectedIndex = 0;
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private async void frmCategorias_Load(object? sender, EventArgs e)
        {
            ConfigurarGrid();
            await CarregarDados();
            AjustarVisibilidadeColunas("Todos");
        }

        private void ConfigurarGrid()
        {
            gridCategorias.ReadOnly = true;
            gridCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCategorias.MultiSelect = false;
            gridCategorias.AutoGenerateColumns = false;
            gridCategorias.RowTemplate.Height = 64;
            gridCategorias.ColumnHeadersHeight = 32;
            gridCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCategorias.Columns.Clear();
            var colId = new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", HeaderText = "ID", FillWeight = 12 };
            colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var colNome = new DataGridViewTextBoxColumn { Name = "Nome", DataPropertyName = "Nome", HeaderText = "Nome", FillWeight = 88 };
            gridCategorias.Columns.Add(colId);
            gridCategorias.Columns.Add(colNome);
        }

        private async Task CarregarDados(string? termo = null)
        {
            var categorias = await _catalogService.GetCategoriasAsync();
            _dados = categorias;
            if (!string.IsNullOrWhiteSpace(termo))
            {
                int id;
                bool isId = int.TryParse(termo, out id);
                _dados = _dados.Where(c => (isId && c.Id == id) || (!isId && (c.Nome?.Contains(termo, System.StringComparison.OrdinalIgnoreCase) ?? false))).ToList();
            }
            RefreshGrid(_dados);
        }

        private void BtnCriarCategoria_Click(object? sender, EventArgs e)
        {
            using var frm = new Categorias.frmCriarCategoria();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _ = CarregarDados();
            }
        }

        private (int id, string nome)? ObterSelecionada()
        {
            if (gridCategorias.CurrentRow == null) return null;
            var idCell = gridCategorias.CurrentRow.Cells["Id"] ?? gridCategorias.CurrentRow.Cells[0];
            var nomeCell = gridCategorias.CurrentRow.Cells["Nome"] ?? gridCategorias.CurrentRow.Cells[1];
            var id = Convert.ToInt32(idCell.Value);
            var nome = Convert.ToString(nomeCell.Value) ?? "";
            return (id, nome);
        }

        private void BtnEditarCategoria_Click(object? sender, EventArgs e)
        {
            var sel = ObterSelecionada();
            if (sel == null)
            {
                MessageBox.Show("Selecione uma categoria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var frm = new Categorias.frmEditarCategoria(sel.Value.id, sel.Value.nome);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _ = CarregarDados();
            }
        }

        private async void BtnExcluirCategoria_Click(object? sender, EventArgs e)
        {
            var sel = ObterSelecionada();
            if (sel == null)
            {
                MessageBox.Show("Selecione uma categoria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show($"Deseja excluir a Categoria \"{sel.Value.nome}\"?",
                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            var ok = await _categoryService.DeleteAsync(sel.Value.id);
            if (!ok)
            {
                MessageBox.Show("Não foi possível excluir a categoria. Verifique a API.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Categoria excluída com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await CarregarDados();
        }

        private void RefreshGrid(List<CatalogService.CategoriaItem> items)
        {
            gridCategorias.DataSource = null;
            gridCategorias.DataSource = items.Select(c => new { c.Id, c.Nome }).ToList();
        }

        private IEnumerable<CatalogService.CategoriaItem> AplicarFiltroAtual()
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
                return items.Where(c => (idOk && c.Id == idVal) || (c.Nome?.Contains(termo, System.StringComparison.OrdinalIgnoreCase) ?? false));
            }

            return campo switch
            {
                "ID" => (int.TryParse(termo, out var id) ? items.Where(c => c.Id == id) : items),
                "Nome" => items.Where(c => c.Nome?.Contains(termo, System.StringComparison.OrdinalIgnoreCase) ?? false),
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
                foreach (DataGridViewColumn c in gridCategorias.Columns) c.Visible = true;
                return;
            }

            foreach (DataGridViewColumn c in gridCategorias.Columns) c.Visible = false;
            var target = campo == "ID" ? "Id" : "Nome";
            foreach (DataGridViewColumn c in gridCategorias.Columns)
            {
                if (string.Equals(c.DataPropertyName, target, System.StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.Name, target, System.StringComparison.OrdinalIgnoreCase))
                {
                    c.Visible = true;
                    break;
                }
            }
        }
    }
}
