﻿﻿﻿﻿﻿﻿﻿using FabricaDeSorrisos.UI.Models.Services;
using FabricaDeSorrisos.UI.ViewModels.UserViewModels;
using System.Drawing;
using System.Linq;
using FabricaDeSorrisos.UI.Models;

namespace FabricaDeSorrisos.UI.Forms
{
    public partial class frmUsuarios : Form
    {
        private readonly UserService _userService;

        public frmUsuarios(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
            btnCriarUsario.Click += btnCriarUsuario_Click;
            btnEditarUsuario.Click += btnEditarUsuario_Click;
            btnExcluirUsuario.Click += btnExcluirUsuario_Click;
        }

        private async void frmUsuarios_Load(object sender, EventArgs e)
        {
            ConfigurarGrids();
            await CarregarUsuarios();
        }

        // =========================
        // CARREGAMENTO
        // =========================

        private async Task CarregarUsuarios()
        {
            var usuarios = await _userService.ListarTodos();

            guna2DataGridView3.DataSource =
                usuarios.Where(u =>
                    !string.IsNullOrWhiteSpace(u.TipoUsuario) &&
                    u.TipoUsuario.Contains("Admin", StringComparison.OrdinalIgnoreCase))
                .ToList();

            guna2DataGridView1.DataSource =
                usuarios.Where(u =>
                    !string.IsNullOrWhiteSpace(u.TipoUsuario) &&
                    u.TipoUsuario.Contains("Gerente", StringComparison.OrdinalIgnoreCase))
                .ToList();

            guna2DataGridView2.DataSource =
                usuarios.Where(u =>
                    !string.IsNullOrWhiteSpace(u.TipoUsuario) &&
                    u.TipoUsuario.Contains("Cliente", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // =========================
        // CONFIGURAÇÃO DOS GRIDS
        // =========================

        private void ConfigurarGrids()
        {
            var roxoClaro = Color.FromArgb(170, 150, 255);
            ConfigurarGrid(guna2DataGridView2, roxoClaro);
            ConfigurarGrid(guna2DataGridView1, roxoClaro);
            ConfigurarGrid(guna2DataGridView3, roxoClaro);
            RegistrarEventosSelecao();
        }

        private void ConfigurarGrid(DataGridView grid, Color selectionColor)
        {
            grid.AutoGenerateColumns = true;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.RowTemplate.Height = 64;

            grid.BorderStyle = BorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            grid.DefaultCellStyle.SelectionBackColor = selectionColor;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;

            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersHeight = 35;

            grid.DataBindingComplete += Grid_DataBindingComplete;
        }

        private void RegistrarEventosSelecao()
        {
            guna2DataGridView1.Enter += (s, e) => LimparSelecaoExceto((DataGridView)s);
            guna2DataGridView2.Enter += (s, e) => LimparSelecaoExceto((DataGridView)s);
            guna2DataGridView3.Enter += (s, e) => LimparSelecaoExceto((DataGridView)s);
            guna2DataGridView1.CellClick += (s, e) => LimparSelecaoExceto((DataGridView)s);
            guna2DataGridView2.CellClick += (s, e) => LimparSelecaoExceto((DataGridView)s);
            guna2DataGridView3.CellClick += (s, e) => LimparSelecaoExceto((DataGridView)s);
        }

        private void LimparSelecaoExceto(DataGridView ativo)
        {
            var grids = new[] { guna2DataGridView1, guna2DataGridView2, guna2DataGridView3 };
            foreach (var g in grids)
            {
                if (g != ativo) g.ClearSelection();
            }
        }

        private void Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns.Contains("Id"))
            {
                grid.Columns["Id"].HeaderText = "ID";
                grid.Columns["Id"].FillWeight = 15;
                grid.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["Id"].DisplayIndex = 0;
            }

            if (grid.Columns.Contains("Nome"))
            {
                grid.Columns["Nome"].HeaderText = "Nome";
                grid.Columns["Nome"].FillWeight = 45;
                grid.Columns["Nome"].DisplayIndex = 1;
            }

            if (grid.Columns.Contains("Email"))
            {
                grid.Columns["Email"].HeaderText = "Email";
                grid.Columns["Email"].FillWeight = 40;
                grid.Columns["Email"].DisplayIndex = 2;
            }

            // esconder colunas que não devem aparecer
            if (grid.Columns.Contains("TipoUsuario"))
                grid.Columns["TipoUsuario"].Visible = false;

            if (grid.Columns.Contains("Senha"))
                grid.Columns["Senha"].Visible = false;
            grid.ClearSelection();
        }

        // =========================
        // CRUD
        // =========================

        private async void btnCriarUsuario_Click(object sender, EventArgs e)
        {
            // PASSANDO O USERSERVICE PARA O FORM
            var frm = new frmCriarUsuarios(_userService);
            Hide();
            frm.ShowDialog(this);
            Show();

            await CarregarUsuarios();
        }

        private async void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            var selecionado = ObterUsuarioSelecionado();
            if (selecionado == null)
            {
                MessageBox.Show("Selecione um usuário para editar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var frm = new Users.frmEditarUsuario(selecionado);
            Hide();
            frm.ShowDialog(this);
            Show();

            await CarregarUsuarios();
        }

        private async void btnExcluirUsuario_Click(object sender, EventArgs e)
        {
            var selecionado = ObterUsuarioSelecionado();
            if (selecionado == null)
            {
                MessageBox.Show("Selecione um usuário para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(UserSession.UserName) &&
                string.Equals(UserSession.UserName, selecionado.Email, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Você não pode se autoexcluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmar = MessageBox.Show($"Deseja excluir o Usuário \"{selecionado.Nome}\"?",
                "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar != DialogResult.Yes) return;
            var ok = await _userService.Excluir(selecionado.Id);
            if (!ok)
            {
                MessageBox.Show("Não foi possível excluir o usuário.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            await CarregarUsuarios();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private UserViewModel? ObterUsuarioSelecionado()
        {
            var grids = new[] { guna2DataGridView1, guna2DataGridView2, guna2DataGridView3 };
            foreach (var g in grids)
            {
                if (g.SelectedRows.Count > 0)
                {
                    var item = g.SelectedRows[0].DataBoundItem as UserViewModel;
                    if (item != null) return item;
                }
            }
            return null;
        }
    }
}
