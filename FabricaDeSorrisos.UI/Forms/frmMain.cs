using FabricaDeSorrisos.UI.Models;
using FabricaDeSorrisos.UI.Models.Services;
using System;
using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Forms
{
    public partial class frmMain : Form
    {
        private readonly UserService _userService;
        private Form _formAtivo;

        public frmMain(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!UserSession.IsAuthenticated)
            {
                MessageBox.Show(
                    "Sessão inválida.",
                    "Acesso negado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                VoltarParaLogin();
                return;
            };
            AplicarPermissoes();

            // Abre uma tela padrão ao entrar (opcional)
            AbrirFormularioNoPanel(new frmBrinquedos());
        }

        private string ObterNomeUsuario()
        {
            if (string.IsNullOrWhiteSpace(UserSession.UserName))
                return "Usuário";

            if (UserSession.UserName.Contains("@"))
                return UserSession.UserName.Split('@')[0];

            return UserSession.UserName;
        }

        private void AplicarPermissoes()
        {
            bool isAdmin = UserSession.Role == "Admin";

            btnUsuarios.Visible = isAdmin;
        }

        // =====================
        // NAVEGAÇÃO (PANEL)
        // =====================

        private void AbrirFormularioNoPanel(Form form)
        {
            if (_formAtivo != null)
            {
                _formAtivo.Close();
                _formAtivo.Dispose();
            }

            _formAtivo = form;

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelDashboard.Controls.Clear();
            panelDashboard.Controls.Add(form);

            form.BringToFront();
            form.Show();
        }

        // =====================
        // BOTÕES → FORMULÁRIOS
        // =====================

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormularioNoPanel(new frmUsuarios(_userService));
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            AbrirFormularioNoPanel(new frmCategorias());
        }

        private void btnSubCategorias_Click(object sender, EventArgs e)
        {
            AbrirFormularioNoPanel(new frmSubCategorias());
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            AbrirFormularioNoPanel(new frmMarcas());
        }

        private void btnPersonagens_Click(object sender, EventArgs e)
        {
            AbrirFormularioNoPanel(new frmPersonagens());
        }

        private void btnBrinquedos_Click(object sender, EventArgs e)
        {
            AbrirFormularioNoPanel(new frmBrinquedos());
        }

        // =====================
        // CONTROLES GERAIS
        // =====================

        private void btnFechar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Deseja sair do sistema?",
                "Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                UserSession.Logout();
                VoltarParaLogin();
            }
        }

        private void VoltarParaLogin()
        {
            new frmLogin().Show();
            Close();
        }
    }
}
