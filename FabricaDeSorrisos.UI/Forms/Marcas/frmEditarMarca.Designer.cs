namespace FabricaDeSorrisos.UI.Forms.Marcas
{
    partial class frmEditarMarca
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
            btnSalvarEdição = new Guna.UI2.WinForms.Guna2Button();
            txtEditarMarca = new Guna.UI2.WinForms.Guna2TextBox();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
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
            btnFechar.Location = new Point(744, 9);
            btnFechar.Margin = new Padding(3, 2, 3, 2);
            btnFechar.Name = "btnFechar";
            btnFechar.ShadowDecoration.CustomizableEdges = customizableEdges5;
            btnFechar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnFechar.Size = new Size(38, 36);
            btnFechar.TabIndex = 0;
            btnFechar.Text = "X";
            // 
            // btnSalvarEdição
            // 
            btnSalvarEdição.BorderRadius = 10;
            btnSalvarEdição.CustomizableEdges = customizableEdges3;
            btnSalvarEdição.DisabledState.BorderColor = Color.DarkGray;
            btnSalvarEdição.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSalvarEdição.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSalvarEdição.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSalvarEdição.FillColor = Color.DeepSkyBlue;
            btnSalvarEdição.Font = new Font("Segoe UI", 9F);
            btnSalvarEdição.ForeColor = Color.Black;
            btnSalvarEdição.Location = new Point(260, 213);
            btnSalvarEdição.Margin = new Padding(3, 2, 3, 2);
            btnSalvarEdição.Name = "btnSalvarEdição";
            btnSalvarEdição.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSalvarEdição.Size = new Size(197, 42);
            btnSalvarEdição.TabIndex = 1;
            btnSalvarEdição.Text = "Salvar Edição";
            // 
            // txtEditarMarca
            // 
            txtEditarMarca.BorderRadius = 10;
            txtEditarMarca.CustomizableEdges = customizableEdges1;
            txtEditarMarca.DefaultText = "";
            txtEditarMarca.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtEditarMarca.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtEditarMarca.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtEditarMarca.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtEditarMarca.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtEditarMarca.Font = new Font("Segoe UI", 9F);
            txtEditarMarca.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtEditarMarca.Location = new Point(236, 141);
            txtEditarMarca.Margin = new Padding(3, 4, 3, 4);
            txtEditarMarca.Name = "txtEditarMarca";
            txtEditarMarca.PlaceholderText = "Digite um Novo Nome";
            txtEditarMarca.SelectedText = "";
            txtEditarMarca.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtEditarMarca.Size = new Size(250, 45);
            txtEditarMarca.TabIndex = 3;
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(256, 9);
            lblTitulo.Margin = new Padding(3, 2, 3, 2);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(202, 39);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "Edite uma Marca";
            // 
            // frmEditarMarca
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Ivory;
            ClientSize = new Size(788, 338);
            Controls.Add(lblTitulo);
            Controls.Add(txtEditarMarca);
            Controls.Add(btnSalvarEdição);
            Controls.Add(btnFechar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmEditarMarca";
            Text = "frmEditarMarca";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2TextBox txtEditarMarca;
        private Guna.UI2.WinForms.Guna2Button btnSalvarEdição;
        private Guna.UI2.WinForms.Guna2CircleButton btnFechar;
    }
}