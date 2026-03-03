using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Forms
{
    public partial class frmSubCategorias : Form
    {
        private readonly UI.Services.SubCategoriaService _subService = new UI.Services.SubCategoriaService();
        private readonly UI.Services.CatalogService _catalogService = new UI.Services.CatalogService();
        private List<UI.Services.SubCategoriaService.SubCategoriaItem> _dados = new();

        public frmSubCategorias()
        {
            InitializeComponent();
            Load += FrmSubCategorias_Load;
            ConfigurarGrid();
            VincularEventos();
        }

        private void VincularEventos()
        {
            // Os nomes dos controles vêm do Designer
            btnCriarSubCategoria.Click += BtnCriarSubCategoria_Click;
            btnEditarSubCategoria.Click += BtnEditarSubCategoria_Click;
            btnExcluirSubCategoria.Click += BtnExcluirSubCategoria_Click;
            btnBuscar.Click += BtnBuscar_Click;
            btnFiltrar.Click += BtnFiltrar_Click;
            btnResetar.Click += BtnResetar_Click;
            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnBuscar_Click(s, e); };
            cbFiltro.Items.Clear();
            cbFiltro.Items.AddRange(new object[] { "Todos", "ID", "Nome" });
            if (cbFiltro.Items.Count > 0) cbFiltro.SelectedIndex = 0;
        }

        private async void FrmSubCategorias_Load(object? sender, EventArgs e)
        {
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
            var colNome = new DataGridViewTextBoxColumn { Name = "Nome", DataPropertyName = "Nome", HeaderText = "Nome da Subcategoria", FillWeight = 88 };
            gridCategorias.Columns.Add(colId);
            gridCategorias.Columns.Add(colNome);
        }

        private async Task CarregarDados(string? termo = null)
        {
            var lista = await _subService.GetAllAsync();
            _dados = lista;
            if (!string.IsNullOrWhiteSpace(termo))
            {
                int id;
                bool isId = int.TryParse(termo, out id);
                _dados = _dados.Where(c => (isId && c.Id == id) || (!isId && (c.Nome?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false))).ToList();
            }
            RefreshGrid(_dados);
        }

        private void RefreshGrid(List<UI.Services.SubCategoriaService.SubCategoriaItem> items)
        {
            gridCategorias.DataSource = null;
            gridCategorias.DataSource = items.Select(c => new { c.Id, c.Nome }).ToList();
        }

        private async void BtnCriarSubCategoria_Click(object? sender, EventArgs e)
        {
            using var form = new Sub_Categorias.frmCriarSubCategoria();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Nome já validado dentro do form
                var nome = form.Tag as string ?? string.Empty;
                int categoriaId = await ObterCategoriaPadraoAsync();
                if (categoriaId <= 0)
                {
                    MessageBox.Show("Nenhuma categoria encontrada para associar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var ok = await _subService.CreateAsync(nome, categoriaId);
                if (ok)
                    await CarregarDados();
                else
                    MessageBox.Show("Não foi possível criar a subcategoria.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnEditarSubCategoria_Click(object? sender, EventArgs e)
        {
            if (gridCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma subcategoria para editar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var id = (int)gridCategorias.SelectedRows[0].Cells["Id"].Value;
            var nomeAtual = gridCategorias.SelectedRows[0].Cells["Nome"].Value?.ToString() ?? "";
            using var form = new Sub_Categorias.frmEditarSubCategorias(id, nomeAtual);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var novoNome = form.Tag as string ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(novoNome))
                {
                    var ok = await _subService.UpdateAsync(id, novoNome);
                    if (ok)
                        await CarregarDados();
                    else
                        MessageBox.Show("Não foi possível salvar a edição.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnExcluirSubCategoria_Click(object? sender, EventArgs e)
        {
            if (gridCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma subcategoria para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var id = (int)gridCategorias.SelectedRows[0].Cells["Id"].Value;
            var nome = gridCategorias.SelectedRows[0].Cells["Nome"].Value?.ToString() ?? "";
            var result = MessageBox.Show($"Tem certeza que deseja excluir a Sub-Categoria \"{nome}\"?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var ok = await _subService.DeleteAsync(id);
                if (ok)
                    await CarregarDados();
                else
                    MessageBox.Show("Não foi possível excluir a subcategoria.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<int> ObterCategoriaPadraoAsync()
        {
            // Estratégia: usar a primeira categoria disponível
            var categorias = await _catalogService.GetCategoriasAsync();
            return categorias.FirstOrDefault()?.Id ?? 0;
        }

        private IEnumerable<UI.Services.SubCategoriaService.SubCategoriaItem> AplicarFiltroAtual()
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
                return items.Where(c => (idOk && c.Id == idVal) || (c.Nome?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false));
            }

            return campo switch
            {
                "ID" => (int.TryParse(termo, out var id) ? items.Where(c => c.Id == id) : items),
                "Nome" => items.Where(c => c.Nome?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false),
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
