namespace FabricaDeSorrisos.UI.Forms.Sub_Categorias
{
    partial class frmEditarSubCategorias
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnFechar = new Guna.UI2.WinForms.Guna2CircleButton();
            btnSalvarEdição = new Guna.UI2.WinForms.Guna2Button();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtNome = new Guna.UI2.WinForms.Guna2TextBox();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
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
            btnFechar.Location = new Point(740, 9);
            btnFechar.Margin = new Padding(3, 2, 3, 2);
            btnFechar.Name = "btnFechar";
            btnFechar.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnFechar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btnFechar.Size = new Size(38, 36);
            btnFechar.TabIndex = 0;
            btnFechar.Text = "X";
            // 
            // btnSalvarEdição
            // 
            btnSalvarEdição.BorderRadius = 10;
            btnSalvarEdição.CustomizableEdges = customizableEdges2;
            btnSalvarEdição.DisabledState.BorderColor = Color.DarkGray;
            btnSalvarEdição.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSalvarEdição.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSalvarEdição.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSalvarEdição.FillColor = Color.DeepSkyBlue;
            btnSalvarEdição.Font = new Font("Segoe UI", 9F);
            btnSalvarEdição.ForeColor = Color.Black;
            btnSalvarEdição.Location = new Point(261, 178);
            btnSalvarEdição.Margin = new Padding(3, 2, 3, 2);
            btnSalvarEdição.Name = "btnSalvarEdição";
            btnSalvarEdição.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnSalvarEdição.Size = new Size(197, 42);
            btnSalvarEdição.TabIndex = 1;
            btnSalvarEdição.Text = "Salvar Edição";
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 20F);
            lblTitulo.Location = new Point(233, 9);
            lblTitulo.Margin = new Padding(3, 2, 3, 2);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(251, 39);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Editar Sub-Categoria";
            // 
            // txtNome
            // 
            txtNome.BorderRadius = 10;
            txtNome.CustomizableEdges = customizableEdges4;
            txtNome.DefaultText = "";
            txtNome.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNome.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNome.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNome.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNome.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNome.Font = new Font("Segoe UI", 9F);
            txtNome.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNome.Location = new Point(234, 114);
            txtNome.Name = "txtNome";
            txtNome.PlaceholderText = "Digite o Novo Nome";
            txtNome.SelectedText = "";
            txtNome.ShadowDecoration.CustomizableEdges = customizableEdges5;
            txtNome.Size = new Size(250, 45);
            txtNome.TabIndex = 3;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 10;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // frmEditarSubCategorias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Ivory;
            ClientSize = new Size(790, 340);
            Controls.Add(txtNome);
            Controls.Add(lblTitulo);
            Controls.Add(btnSalvarEdição);
            Controls.Add(btnFechar);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "frmEditarSubCategorias";
            Text = "frmEditarSubCategorias";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton btnFechar;
        private Guna.UI2.WinForms.Guna2Button btnSalvarEdição;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2TextBox txtNome;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
    }
}