namespace FabricaDeSorrisos.UI.Forms.Marcas
{
    partial class frmCriarMarca
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnFechar = new Guna.UI2.WinForms.Guna2CircleButton();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            txtNomeCategoria = new Guna.UI2.WinForms.Guna2TextBox();
            btnCriar = new Guna.UI2.WinForms.Guna2Button();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
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
            btnFechar.Location = new Point(739, 10);
            btnFechar.Name = "btnFechar";
            btnFechar.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnFechar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnFechar.Size = new Size(38, 36);
            btnFechar.TabIndex = 0;
            btnFechar.Text = "X";
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
            txtNomeCategoria.CustomizableEdges = customizableEdges6;
            txtNomeCategoria.DefaultText = "";
            txtNomeCategoria.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNomeCategoria.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNomeCategoria.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNomeCategoria.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNomeCategoria.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNomeCategoria.Font = new Font("Segoe UI", 9F);
            txtNomeCategoria.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNomeCategoria.Location = new Point(268, 135);
            txtNomeCategoria.Margin = new Padding(3, 4, 3, 4);
            txtNomeCategoria.Name = "txtNomeCategoria";
            txtNomeCategoria.PlaceholderText = "Digite o nome da marca";
            txtNomeCategoria.SelectedText = "";
            txtNomeCategoria.ShadowDecoration.CustomizableEdges = customizableEdges7;
            txtNomeCategoria.Size = new Size(222, 48);
            txtNomeCategoria.TabIndex = 3;
            // 
            // btnCriar
            // 
            btnCriar.BorderRadius = 10;
            btnCriar.CustomizableEdges = customizableEdges4;
            btnCriar.DisabledState.BorderColor = Color.DarkGray;
            btnCriar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCriar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCriar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCriar.FillColor = Color.LimeGreen;
            btnCriar.Font = new Font("Segoe UI", 9F);
            btnCriar.ForeColor = Color.White;
            btnCriar.Location = new Point(500, 570);
            btnCriar.Name = "btnCriar";
            btnCriar.ShadowDecoration.CustomizableEdges = customizableEdges5;
            btnCriar.Size = new Size(204, 57);
            btnCriar.TabIndex = 5;
            btnCriar.Text = "Criar Marca";
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(310, 10);
            lblTitulo.Margin = new Padding(3, 2, 3, 2);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(139, 39);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "Criar Marca";
            // 
            // guna2Button1
            // 
            guna2Button1.BorderRadius = 10;
            guna2Button1.CustomizableEdges = customizableEdges2;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.LimeGreen;
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.Black;
            guna2Button1.Location = new Point(279, 218);
            guna2Button1.Margin = new Padding(3, 2, 3, 2);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges3;
            guna2Button1.Size = new Size(197, 42);
            guna2Button1.TabIndex = 7;
            guna2Button1.Text = "Criar Marca";
            // 
            // frmCriarMarca
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Ivory;
            ClientSize = new Size(788, 338);
            Controls.Add(guna2Button1);
            Controls.Add(lblTitulo);
            Controls.Add(btnCriar);
            Controls.Add(txtNomeCategoria);
            Controls.Add(btnFechar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmCriarMarca";
            Text = "frmCriarMarca";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton btnFechar;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Button btnCriar;
        private Guna.UI2.WinForms.Guna2TextBox txtNomeCategoria;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
    }
}