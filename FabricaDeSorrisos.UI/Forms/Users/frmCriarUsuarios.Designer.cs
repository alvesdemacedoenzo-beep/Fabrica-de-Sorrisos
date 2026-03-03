namespace FabricaDeSorrisos.UI.Forms
{
    partial class frmCriarUsuarios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            txtSenha = new Guna.UI2.WinForms.Guna2TextBox();
            btnCriarUsuario = new Guna.UI2.WinForms.Guna2Button();
            cbTipoUsuario = new Guna.UI2.WinForms.Guna2ComboBox();
            txtNome = new Guna.UI2.WinForms.Guna2TextBox();
            btnFechar = new Guna.UI2.WinForms.Guna2CircleButton();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtSenhaNovamente = new Guna.UI2.WinForms.Guna2TextBox();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 20;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // txtEmail
            // 
            txtEmail.BorderRadius = 10;
            txtEmail.CustomizableEdges = customizableEdges12;
            txtEmail.DefaultText = "";
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(338, 127);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "E-mail";
            txtEmail.SelectedText = "";
            txtEmail.ShadowDecoration.CustomizableEdges = customizableEdges13;
            txtEmail.Size = new Size(192, 38);
            txtEmail.TabIndex = 6;
            // 
            // txtSenha
            // 
            txtSenha.BorderRadius = 10;
            txtSenha.CustomizableEdges = customizableEdges10;
            txtSenha.DefaultText = "";
            txtSenha.Font = new Font("Segoe UI", 9F);
            txtSenha.Location = new Point(339, 173);
            txtSenha.Margin = new Padding(3, 4, 3, 4);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '●';
            txtSenha.PlaceholderText = "Senha";
            txtSenha.SelectedText = "";
            txtSenha.ShadowDecoration.CustomizableEdges = customizableEdges11;
            txtSenha.Size = new Size(192, 38);
            txtSenha.TabIndex = 5;
            // 
            // btnCriarUsuario
            // 
            btnCriarUsuario.BorderRadius = 10;
            btnCriarUsuario.CustomizableEdges = customizableEdges8;
            btnCriarUsuario.FillColor = Color.Lime;
            btnCriarUsuario.Font = new Font("Segoe UI", 9F);
            btnCriarUsuario.ForeColor = Color.Black;
            btnCriarUsuario.Location = new Point(334, 347);
            btnCriarUsuario.Name = "btnCriarUsuario";
            btnCriarUsuario.ShadowDecoration.CustomizableEdges = customizableEdges9;
            btnCriarUsuario.Size = new Size(197, 42);
            btnCriarUsuario.TabIndex = 4;
            btnCriarUsuario.Text = "Criar Usuário";
            btnCriarUsuario.Click += btnEntrar_Click;
            // 
            // cbTipoUsuario
            // 
            cbTipoUsuario.BackColor = Color.Transparent;
            cbTipoUsuario.BorderRadius = 10;
            cbTipoUsuario.CustomizableEdges = customizableEdges6;
            cbTipoUsuario.DrawMode = DrawMode.OwnerDrawFixed;
            cbTipoUsuario.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoUsuario.FocusedColor = Color.Empty;
            cbTipoUsuario.Font = new Font("Segoe UI", 10F);
            cbTipoUsuario.ForeColor = Color.FromArgb(68, 88, 112);
            cbTipoUsuario.ItemHeight = 30;
            cbTipoUsuario.Location = new Point(337, 264);
            cbTipoUsuario.Name = "cbTipoUsuario";
            cbTipoUsuario.ShadowDecoration.CustomizableEdges = customizableEdges7;
            cbTipoUsuario.Size = new Size(193, 36);
            cbTipoUsuario.TabIndex = 3;
            // 
            // txtNome
            // 
            txtNome.BorderRadius = 10;
            txtNome.CustomizableEdges = customizableEdges4;
            txtNome.DefaultText = "";
            txtNome.Font = new Font("Segoe UI", 9F);
            txtNome.Location = new Point(338, 81);
            txtNome.Margin = new Padding(3, 4, 3, 4);
            txtNome.Name = "txtNome";
            txtNome.PlaceholderText = "Nome";
            txtNome.SelectedText = "";
            txtNome.ShadowDecoration.CustomizableEdges = customizableEdges5;
            txtNome.Size = new Size(192, 38);
            txtNome.TabIndex = 2;
            // 
            // btnFechar
            // 
            btnFechar.FillColor = Color.Red;
            btnFechar.Font = new Font("Segoe UI", 9F);
            btnFechar.ForeColor = Color.White;
            btnFechar.Location = new Point(940, 12);
            btnFechar.Name = "btnFechar";
            btnFechar.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnFechar.Size = new Size(38, 36);
            btnFechar.TabIndex = 1;
            btnFechar.Text = "X";
            btnFechar.Click += btnFechar_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(357, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(157, 39);
            lblTitulo.TabIndex = 7;
            lblTitulo.Text = "Criar Usuário";
            // 
            // txtSenhaNovamente
            // 
            txtSenhaNovamente.BorderRadius = 10;
            txtSenhaNovamente.CustomizableEdges = customizableEdges1;
            txtSenhaNovamente.DefaultText = "";
            txtSenhaNovamente.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtSenhaNovamente.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtSenhaNovamente.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtSenhaNovamente.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtSenhaNovamente.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtSenhaNovamente.Font = new Font("Segoe UI", 9F);
            txtSenhaNovamente.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtSenhaNovamente.Location = new Point(338, 219);
            txtSenhaNovamente.Margin = new Padding(3, 4, 3, 4);
            txtSenhaNovamente.Name = "txtSenhaNovamente";
            txtSenhaNovamente.PlaceholderText = "Digite a Senha novamente";
            txtSenhaNovamente.SelectedText = "";
            txtSenhaNovamente.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtSenhaNovamente.Size = new Size(192, 38);
            txtSenhaNovamente.TabIndex = 8;
            // 
            // frmCriarUsuarios
            // 
            BackColor = Color.Ivory;
            ClientSize = new Size(990, 485);
            Controls.Add(txtSenhaNovamente);
            Controls.Add(lblTitulo);
            Controls.Add(btnFechar);
            Controls.Add(txtNome);
            Controls.Add(cbTipoUsuario);
            Controls.Add(btnCriarUsuario);
            Controls.Add(txtSenha);
            Controls.Add(txtEmail);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmCriarUsuarios";
            Load += frmCriarUsuarios_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtSenha;
        private Guna.UI2.WinForms.Guna2Button btnCriarUsuario;
        private Guna.UI2.WinForms.Guna2TextBox txtNome;
        private Guna.UI2.WinForms.Guna2ComboBox cbTipoUsuario;
        private Guna.UI2.WinForms.Guna2CircleButton btnFechar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2TextBox txtSenhaNovamente;
    }
}
