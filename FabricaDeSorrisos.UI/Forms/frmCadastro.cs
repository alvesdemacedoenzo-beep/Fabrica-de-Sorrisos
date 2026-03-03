using FabricaDeSorrisos.UI.Models.Services;
using FabricaDeSorrisos.UI.ViewModels.UserViewModels;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Forms
{
    public partial class frmCadastro : Form
    {
        private readonly UserService _userService;

        public frmCadastro()
        {
            InitializeComponent();
            _userService = new UserService();

            txtSenha.PasswordChar = '●';
            txtSenhaNovamente.PasswordChar = '●';

            btnCriar.Click += btnCriar_Click;
            btnVoltarLogin.Click += btnVoltarLogin_Click;
        }

        private async void btnCriar_Click(object sender, System.EventArgs e)
        {
            var nome = txtNome.Text?.Trim() ?? string.Empty;
            var email = txtEmail.Text?.Trim() ?? string.Empty;
            var senha = txtSenha.Text ?? string.Empty;
            var senha2 = txtSenhaNovamente.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha) ||
                string.IsNullOrWhiteSpace(senha2))
            {
                MessageBox.Show(
                    "Preencha todos os campos obrigatórios.",
                    "Campos obrigatórios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!EmailValido(email))
            {
                MessageBox.Show(
                    "E-mail inválido. Informe um e-mail que contenha '@' e '.com'.",
                    "Validação de e-mail",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtEmail.Focus();
                return;
            }

            if (!senha.Equals(senha2))
            {
                MessageBox.Show(
                    "As senhas não coincidem.",
                    "Validação de senha",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!SenhaValida(senha))
            {
                MessageBox.Show(
                    "A senha deve ter pelo menos 5 caracteres.",
                    "Validação de senha",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            try
            {
                var usuario = new UserViewModel
                {
                    Nome = nome,
                    Email = email,
                    Senha = senha,
                    TipoUsuario = "Gerente"
                };

                var ok = await _userService.Criar(usuario);

                if (ok)
                {
                    MessageBox.Show(
                        "Usuário criado com sucesso. Você já pode fazer login.",
                        "Cadastro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    var login = new frmLogin();
                    login.Show();
                    Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(
                    "Não foi possível criar o usuário.\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnVoltarLogin_Click(object sender, System.EventArgs e)
        {
            var login = new frmLogin();
            login.Show();
            Close();
        }

        private bool SenhaValida(string senha)
        {
            return senha?.Length >= 5;
        }

        private bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return email.Contains("@") && email.Contains(".com");
        }
    }
}
