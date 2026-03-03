﻿﻿﻿﻿﻿using FabricaDeSorrisos.UI.Models;
using FabricaDeSorrisos.UI.Models.Services;

namespace FabricaDeSorrisos.UI.Forms
{
    public partial class frmLogin : Form
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public frmLogin()
        {
            InitializeComponent();

            _authService = new AuthService();
            _userService = new UserService();

            txtSenha.PasswordChar = '●';
            this.AcceptButton = btnEntrar;

            btnFechar.Click += btnFechar_Click;
            btnCadastro.Click += btnCadastro_Click;
        }

        private async Task RealizarLogin()
        {
            btnEntrar.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show(
                    "Informe email e senha.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                btnEntrar.Enabled = true;
                return;
            }

            try
            {
                var request = new LoginRequest
                {
                    Email = txtEmail.Text.Trim(),
                    Password = txtSenha.Text
                };

                var response = await _authService.LoginAsync(request);

                if (response.Role == "Cliente")
                {
                    MessageBox.Show(
                        "Usuários do tipo Cliente não têm acesso ao sistema administrativo.",
                        "Acesso negado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    btnEntrar.Enabled = true;
                    return;
                }

                // ✅ Inicia sessão
                UserSession.Token = response.Token;
                UserSession.UserName = response.UserName;
                UserSession.Role = response.Role;

                var main = new frmMain(_userService);
                main.Show();

                this.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Email ou senha inválidos.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                btnEntrar.Enabled = true;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show(
                "Deseja realmente encerrar a aplicação?",
                "Encerrar aplicação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnEntrar_Click_1(object sender, EventArgs e)
        {
            _ = RealizarLogin();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            var cadastro = new frmCadastro();
            cadastro.Show();
            this.Hide();
        }
    }
}
