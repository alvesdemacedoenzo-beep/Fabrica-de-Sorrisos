namespace FabricaDeSorrisos.UI.Forms.Personagens
{
    partial class frmCriarPersonagens
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            btnFechar = new Guna.UI2.WinForms.Guna2CircleButton();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnNomePersonagem = new Guna.UI2.WinForms.Guna2TextBox();
            btnCriarPersonagem = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 10;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // btnFechar
            // 
            btnFechar.DisabledState.BorderColor = Color.DarkGray;
            btnFechar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnFechar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnFechar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnFechar.FillColor = Color.Red;
            btnFechar.Font = new Font("Segoe UI", 9F);
            btnFechar.ForeColor = Color.White;
            btnFechar.Location = new Point(740, 11);
            btnFechar.Margin = new Padding(3, 2, 3, 2);
            btnFechar.Name = "btnFechar";
            btnFechar.ShadowDecoration.CustomizableEdges = customizableEdges5;
            btnFechar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnFechar.Size = new Size(38, 36);
            btnFechar.TabIndex = 0;
            btnFechar.Text = "X";
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(298, 39);
            lblTitulo.Margin = new Padding(3, 2, 3, 2);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(213, 39);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Criar Personagem";
            // 
            // btnNomePersonagem
            // 
            btnNomePersonagem.BorderRadius = 10;
            btnNomePersonagem.CustomizableEdges = customizableEdges3;
            btnNomePersonagem.DefaultText = "";
            btnNomePersonagem.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            btnNomePersonagem.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            btnNomePersonagem.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            btnNomePersonagem.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            btnNomePersonagem.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            btnNomePersonagem.Font = new Font("Segoe UI", 9F);
            btnNomePersonagem.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            btnNomePersonagem.Location = new Point(282, 136);
            btnNomePersonagem.Name = "btnNomePersonagem";
            btnNomePersonagem.PlaceholderText = "Digite o Nome do Personagem";
            btnNomePersonagem.SelectedText = "";
            btnNomePersonagem.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnNomePersonagem.Size = new Size(250, 45);
            btnNomePersonagem.TabIndex = 2;
            // 
            // btnCriarPersonagem
            // 
            btnCriarPersonagem.BorderRadius = 10;
            btnCriarPersonagem.CustomizableEdges = customizableEdges1;
            btnCriarPersonagem.DisabledState.BorderColor = Color.DarkGray;
            btnCriarPersonagem.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCriarPersonagem.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCriarPersonagem.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCriarPersonagem.FillColor = Color.LimeGreen;
            btnCriarPersonagem.Font = new Font("Segoe UI", 9F);
            btnCriarPersonagem.ForeColor = Color.Black;
            btnCriarPersonagem.Location = new Point(298, 209);
            btnCriarPersonagem.Margin = new Padding(3, 2, 3, 2);
            btnCriarPersonagem.Name = "btnCriarPersonagem";
            btnCriarPersonagem.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCriarPersonagem.Size = new Size(197, 42);
            btnCriarPersonagem.TabIndex = 3;
            btnCriarPersonagem.Text = "Criar Personagem";
            // 
            // frmCriarPersonagens
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Ivory;
            ClientSize = new Size(790, 340);
            Controls.Add(btnCriarPersonagem);
            Controls.Add(btnNomePersonagem);
            Controls.Add(lblTitulo);
            Controls.Add(btnFechar);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "frmCriarPersonagens";
            Text = "frmCriarPersonagens";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2CircleButton btnFechar;
        private Guna.UI2.WinForms.Guna2Button btnCriarPersonagem;
        private Guna.UI2.WinForms.Guna2TextBox btnNomePersonagem;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
    }
}