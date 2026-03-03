namespace FabricaDeSorrisos.UI.Forms.Categorias
{
    partial class frmCriarCategoria
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            txtNomeCategoria = new Guna.UI2.WinForms.Guna2TextBox();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnCriarCategoria = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // guna2CircleButton1
            // 
            guna2CircleButton1.DisabledState.BorderColor = Color.DarkGray;
            guna2CircleButton1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2CircleButton1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2CircleButton1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2CircleButton1.FillColor = Color.Red;
            guna2CircleButton1.Font = new Font("Segoe UI", 9F);
            guna2CircleButton1.ForeColor = Color.White;
            guna2CircleButton1.Location = new Point(738, 10);
            guna2CircleButton1.Name = "guna2CircleButton1";
            guna2CircleButton1.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CircleButton1.Size = new Size(38, 36);
            guna2CircleButton1.TabIndex = 0;
            guna2CircleButton1.Text = "X";
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 10;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // txtNomeCategoria
            // 
            txtNomeCategoria.BorderRadius = 10;
            txtNomeCategoria.CustomizableEdges = customizableEdges4;
            txtNomeCategoria.DefaultText = "";
            txtNomeCategoria.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNomeCategoria.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNomeCategoria.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNomeCategoria.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNomeCategoria.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNomeCategoria.Font = new Font("Segoe UI", 9F);
            txtNomeCategoria.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNomeCategoria.Location = new Point(273, 112);
            txtNomeCategoria.Margin = new Padding(3, 4, 3, 4);
            txtNomeCategoria.Name = "txtNomeCategoria";
            txtNomeCategoria.PlaceholderText = "Digite o nome da Categoria";
            txtNomeCategoria.SelectedText = "";
            txtNomeCategoria.ShadowDecoration.CustomizableEdges = customizableEdges5;
            txtNomeCategoria.Size = new Size(202, 47);
            txtNomeCategoria.TabIndex = 1;
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(258, 10);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(236, 39);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Nome da Categoria";
            // 
            // btnCriarCategoria
            // 
            btnCriarCategoria.BorderRadius = 10;
            btnCriarCategoria.CustomizableEdges = customizableEdges2;
            btnCriarCategoria.DisabledState.BorderColor = Color.DarkGray;
            btnCriarCategoria.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCriarCategoria.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCriarCategoria.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCriarCategoria.FillColor = Color.LimeGreen;
            btnCriarCategoria.Font = new Font("Segoe UI", 9F);
            btnCriarCategoria.ForeColor = Color.Black;
            btnCriarCategoria.Location = new Point(273, 180);
            btnCriarCategoria.Name = "btnCriarCategoria";
            btnCriarCategoria.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnCriarCategoria.Size = new Size(202, 55);
            btnCriarCategoria.TabIndex = 6;
            btnCriarCategoria.Text = "Criar Categoria";
            // 
            // frmCriarCategoria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Ivory;
            ClientSize = new Size(790, 340);
            Controls.Add(btnCriarCategoria);
            Controls.Add(lblTitulo);
            Controls.Add(txtNomeCategoria);
            Controls.Add(guna2CircleButton1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmCriarCategoria";
            Text = "frmCriarCategoria";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2TextBox txtNomeCategoria;
        private Guna.UI2.WinForms.Guna2Button btnCriarCategoria;
    }
}