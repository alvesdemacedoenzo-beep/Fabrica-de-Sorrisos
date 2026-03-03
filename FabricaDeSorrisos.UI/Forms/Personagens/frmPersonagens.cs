using System;
using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Forms
{
    public partial class frmPersonagens : Form
    {
        private readonly UI.Services.PersonagemService _service = new UI.Services.PersonagemService();
        private List<UI.Services.PersonagemService.PersonagemItem> _dados = new();

        public frmPersonagens()
        {
            InitializeComponent();
        }

        private void frmPersonagens_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            VincularEventos();
            _ = CarregarDados();
            AjustarVisibilidadeColunas("Todos");
        }

        private void ConfigurarGrid()
        {
            dgvPersonagem.ReadOnly = true;
            dgvPersonagem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonagem.MultiSelect = false;
            dgvPersonagem.AutoGenerateColumns = false;
            dgvPersonagem.RowTemplate.Height = 64;
            dgvPersonagem.ColumnHeadersHeight = 32;
            dgvPersonagem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPersonagem.Columns.Clear();
            var colId = new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", HeaderText = "ID", FillWeight = 12 };
            colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            var colNome = new DataGridViewTextBoxColumn { Name = "Nome", DataPropertyName = "Nome", HeaderText = "Nome do Personagem", FillWeight = 88 };
            dgvPersonagem.Columns.Add(colId);
            dgvPersonagem.Columns.Add(colNome);
        }

        private void VincularEventos()
        {
            btnCriarPersonagem.Click += BtnCriarPersonagem_Click;
            btnEditarPersonagem.Click += BtnEditarPersonagem_Click;
            btnExcluirPersonagem.Click += BtnExcluirPersonagem_Click;
            btnBuscar.Click += BtnBuscar_Click;
            btnFiltrar.Click += BtnFiltrar_Click;
            btnResetar.Click += BtnResetar_Click;
            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnBuscar_Click(s, e); };
            cbFiltro.Items.Clear();
            cbFiltro.Items.AddRange(new object[] { "Todos", "ID", "Nome" });
            if (cbFiltro.Items.Count > 0) cbFiltro.SelectedIndex = 0;
        }

        private async System.Threading.Tasks.Task CarregarDados(string? termo = null)
        {
            var lista = await _service.GetAllAsync();
            _dados = lista;
            if (!string.IsNullOrWhiteSpace(termo))
            {
                int id;
                bool isId = int.TryParse(termo, out id);
                _dados = _dados.Where(p => (isId && p.Id == id) || (!isId && (p.Nome?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false))).ToList();
            }
            RefreshGrid(_dados);
        }

        private void RefreshGrid(List<UI.Services.PersonagemService.PersonagemItem> items)
        {
            dgvPersonagem.DataSource = null;
            dgvPersonagem.DataSource = items.Select(p => new { p.Id, p.Nome }).ToList();
        }

        private async void BtnCriarPersonagem_Click(object? sender, EventArgs e)
        {
            using var form = new Personagens.frmCriarPersonagens();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var nome = form.Tag as string ?? string.Empty;
                var ok = await _service.CreateAsync(nome);
                if (ok) await CarregarDados();
                else MessageBox.Show("Não foi possível criar o personagem.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnEditarPersonagem_Click(object? sender, EventArgs e)
        {
            if (dgvPersonagem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um personagem para editar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var id = (int)dgvPersonagem.SelectedRows[0].Cells["Id"].Value;
            var nomeAtual = dgvPersonagem.SelectedRows[0].Cells["Nome"].Value?.ToString() ?? "";
            using var form = new Personagens.frmEditarPersonagens(id, nomeAtual);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var novoNome = form.Tag as string ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(novoNome))
                {
                    var ok = await _service.UpdateAsync(id, novoNome);
                    if (ok) await CarregarDados();
                    else MessageBox.Show("Não foi possível salvar a edição.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void BtnExcluirPersonagem_Click(object? sender, EventArgs e)
        {
            if (dgvPersonagem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um personagem para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var id = (int)dgvPersonagem.SelectedRows[0].Cells["Id"].Value;
            var nome = dgvPersonagem.SelectedRows[0].Cells["Nome"].Value?.ToString() ?? "";
            var result = MessageBox.Show($"Deseja excluir o Personagem \"{nome}\"?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var ok = await _service.DeleteAsync(id);
                if (ok) await CarregarDados();
                else MessageBox.Show("Não foi possível excluir o personagem.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private IEnumerable<UI.Services.PersonagemService.PersonagemItem> AplicarFiltroAtual()
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
                return items.Where(p => (idOk && p.Id == idVal) || (p.Nome?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false));
            }

            return campo switch
            {
                "ID" => (int.TryParse(termo, out var id) ? items.Where(p => p.Id == id) : items),
                "Nome" => items.Where(p => p.Nome?.Contains(termo, StringComparison.OrdinalIgnoreCase) ?? false),
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
                foreach (DataGridViewColumn c in dgvPersonagem.Columns) c.Visible = true;
                return;
            }

            foreach (DataGridViewColumn c in dgvPersonagem.Columns) c.Visible = false;
            var target = campo == "ID" ? "Id" : "Nome";
            foreach (DataGridViewColumn c in dgvPersonagem.Columns)
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
