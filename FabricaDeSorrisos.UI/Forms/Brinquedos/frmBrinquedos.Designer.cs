namespace FabricaDeSorrisos.UI.Forms
{
    partial class frmBrinquedos
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTituloLista = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnCriarBrinquedo = new Guna.UI2.WinForms.Guna2GradientButton();
            btnStatusBrinquedo = new Guna.UI2.WinForms.Guna2GradientButton();
            btnEditarBrinquedo = new Guna.UI2.WinForms.Guna2GradientButton();
            lblBrinquedosCadastrados = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblBrinquedoCadastrado = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnResetar = new Guna.UI2.WinForms.Guna2Button();
            btnFiltrar = new Guna.UI2.WinForms.Guna2GradientButton();
            btnBuscar = new Guna.UI2.WinForms.Guna2GradientButton();
            txtBusca = new Guna.UI2.WinForms.Guna2TextBox();
            cbFiltro = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblTituloLista
            // 
            lblTituloLista.BackColor = Color.Transparent;
            lblTituloLista.Font = new Font("Segoe UI", 16F);
            lblTituloLista.Location = new Point(14, 8);
            lblTituloLista.Margin = new Padding(3, 4, 3, 4);
            lblTituloLista.Name = "lblTituloLista";
            lblTituloLista.Size = new Size(224, 32);
            lblTituloLista.TabIndex = 0;
            lblTituloLista.Text = "Brinquedos cadastrados";
            // 
            // guna2DataGridView1
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            guna2DataGridView1.ColumnHeadersHeight = 32;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            guna2DataGridView1.GridColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.Location = new Point(2, 263);
            guna2DataGridView1.Margin = new Padding(3, 2, 3, 2);
            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowHeadersWidth = 51;
            guna2DataGridView1.RowTemplate.Height = 29;
            guna2DataGridView1.Size = new Size(1181, 420);
            guna2DataGridView1.TabIndex = 13;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            guna2DataGridView1.ThemeStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            guna2DataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 32;
            guna2DataGridView1.ThemeStyle.ReadOnly = false;
            guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = Color.White;
            guna2DataGridView1.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            guna2DataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 29;
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            guna2DataGridView1.CellContentClick += guna2DataGridView1_CellContentClick;
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 10;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 20F);
            guna2HtmlLabel1.Location = new Point(452, 8);
            guna2HtmlLabel1.Margin = new Padding(3, 2, 3, 2);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(431, 56);
            guna2HtmlLabel1.TabIndex = 17;
            guna2HtmlLabel1.Text = "Catálogo de Brinquedos";
            guna2HtmlLabel1.Click += guna2HtmlLabel1_Click;
            // 
            // btnCriarBrinquedo
            // 
            btnCriarBrinquedo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCriarBrinquedo.BorderRadius = 10;
            btnCriarBrinquedo.CustomizableEdges = customizableEdges15;
            btnCriarBrinquedo.DisabledState.BorderColor = Color.DarkGray;
            btnCriarBrinquedo.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCriarBrinquedo.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCriarBrinquedo.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnCriarBrinquedo.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCriarBrinquedo.FillColor = Color.HotPink;
            btnCriarBrinquedo.FillColor2 = Color.Yellow;
            btnCriarBrinquedo.Font = new Font("Segoe UI", 9F);
            btnCriarBrinquedo.ForeColor = Color.Black;
            btnCriarBrinquedo.Image = Properties.Resources.plus;
            btnCriarBrinquedo.ImageAlign = HorizontalAlignment.Left;
            btnCriarBrinquedo.Location = new Point(257, 98);
            btnCriarBrinquedo.Margin = new Padding(3, 2, 3, 2);
            btnCriarBrinquedo.Name = "btnCriarBrinquedo";
            btnCriarBrinquedo.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnCriarBrinquedo.Size = new Size(197, 42);
            btnCriarBrinquedo.TabIndex = 19;
            btnCriarBrinquedo.Text = "Criar Brinquedo";
            // 
            // btnStatusBrinquedo
            // 
            btnStatusBrinquedo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStatusBrinquedo.BorderRadius = 10;
            btnStatusBrinquedo.CustomizableEdges = customizableEdges13;
            btnStatusBrinquedo.DisabledState.BorderColor = Color.DarkGray;
            btnStatusBrinquedo.DisabledState.CustomBorderColor = Color.DarkGray;
            btnStatusBrinquedo.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnStatusBrinquedo.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnStatusBrinquedo.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnStatusBrinquedo.FillColor = Color.Wheat;
            btnStatusBrinquedo.Font = new Font("Segoe UI", 9F);
            btnStatusBrinquedo.ForeColor = Color.Black;
            btnStatusBrinquedo.Location = new Point(742, 98);
            btnStatusBrinquedo.Margin = new Padding(3, 2, 3, 2);
            btnStatusBrinquedo.Name = "btnStatusBrinquedo";
            btnStatusBrinquedo.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnStatusBrinquedo.Size = new Size(197, 42);
            btnStatusBrinquedo.TabIndex = 21;
            btnStatusBrinquedo.Text = "Ativar/Desativar Brinquedo";
            // 
            // btnEditarBrinquedo
            // 
            btnEditarBrinquedo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditarBrinquedo.BorderRadius = 10;
            btnEditarBrinquedo.CustomizableEdges = customizableEdges11;
            btnEditarBrinquedo.DisabledState.BorderColor = Color.DarkGray;
            btnEditarBrinquedo.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEditarBrinquedo.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEditarBrinquedo.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnEditarBrinquedo.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEditarBrinquedo.FillColor = Color.Aqua;
            btnEditarBrinquedo.FillColor2 = Color.Yellow;
            btnEditarBrinquedo.Font = new Font("Segoe UI", 9F);
            btnEditarBrinquedo.ForeColor = Color.Black;
            btnEditarBrinquedo.Image = Properties.Resources.lapis4;
            btnEditarBrinquedo.ImageAlign = HorizontalAlignment.Left;
            btnEditarBrinquedo.Location = new Point(505, 98);
            btnEditarBrinquedo.Margin = new Padding(3, 2, 3, 2);
            btnEditarBrinquedo.Name = "btnEditarBrinquedo";
            btnEditarBrinquedo.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnEditarBrinquedo.Size = new Size(197, 42);
            btnEditarBrinquedo.TabIndex = 22;
            btnEditarBrinquedo.Text = "Editar Brinquedo";
            // 
            // lblBrinquedosCadastrados
            // 
            lblBrinquedosCadastrados.BackColor = Color.Transparent;
            lblBrinquedosCadastrados.Font = new Font("Segoe UI", 15F);
            lblBrinquedosCadastrados.Location = new Point(0, 0);
            lblBrinquedosCadastrados.Margin = new Padding(3, 2, 3, 2);
            lblBrinquedosCadastrados.Name = "lblBrinquedosCadastrados";
            lblBrinquedosCadastrados.Size = new Size(215, 30);
            lblBrinquedosCadastrados.TabIndex = 14;
            lblBrinquedosCadastrados.Text = "Brinquedos Cadastrados";
            lblBrinquedosCadastrados.Click += lblBrinquedosCadastrados_Click;
            // 
            // lblBrinquedoCadastrado
            // 
            lblBrinquedoCadastrado.BackColor = Color.Transparent;
            lblBrinquedoCadastrado.Font = new Font("Segoe UI", 15F);
            lblBrinquedoCadastrado.Location = new Point(489, 46);
            lblBrinquedoCadastrado.Name = "lblBrinquedoCadastrado";
            lblBrinquedoCadastrado.Size = new Size(291, 39);
            lblBrinquedoCadastrado.TabIndex = 23;
            lblBrinquedoCadastrado.Text = "Brinquedos Cadastrados";
            // 
            // btnResetar
            // 
            btnResetar.BorderRadius = 10;
            btnResetar.CustomizableEdges = customizableEdges9;
            btnResetar.DisabledState.BorderColor = Color.DarkGray;
            btnResetar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnResetar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnResetar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnResetar.FillColor = Color.Gray;
            btnResetar.Font = new Font("Segoe UI", 9F);
            btnResetar.ForeColor = Color.White;
            btnResetar.Location = new Point(992, 213);
            btnResetar.Name = "btnResetar";
            btnResetar.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnResetar.Size = new Size(138, 45);
            btnResetar.TabIndex = 24;
            btnResetar.Text = "Resetar Busca/Filtro";
            // 
            // btnFiltrar
            // 
            btnFiltrar.BorderRadius = 10;
            btnFiltrar.CustomizableEdges = customizableEdges7;
            btnFiltrar.DisabledState.BorderColor = Color.DarkGray;
            btnFiltrar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnFiltrar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnFiltrar.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnFiltrar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnFiltrar.FillColor = Color.Crimson;
            btnFiltrar.FillColor2 = Color.Navy;
            btnFiltrar.Font = new Font("Segoe UI", 9F);
            btnFiltrar.ForeColor = Color.White;
            btnFiltrar.Location = new Point(759, 213);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnFiltrar.Size = new Size(150, 45);
            btnFiltrar.TabIndex = 25;
            btnFiltrar.Text = "Filtrar";
            // 
            // btnBuscar
            // 
            btnBuscar.BorderRadius = 10;
            btnBuscar.CustomizableEdges = customizableEdges5;
            btnBuscar.DisabledState.BorderColor = Color.DarkGray;
            btnBuscar.DisabledState.CustomBorderColor = Color.DarkGray;
            btnBuscar.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnBuscar.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnBuscar.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnBuscar.FillColor = Color.PaleTurquoise;
            btnBuscar.FillColor2 = Color.SteelBlue;
            btnBuscar.Font = new Font("Segoe UI", 9F);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(522, 213);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnBuscar.Size = new Size(150, 45);
            btnBuscar.TabIndex = 26;
            btnBuscar.Text = "Buscar";
            // 
            // txtBusca
            // 
            txtBusca.BorderRadius = 10;
            txtBusca.CustomizableEdges = customizableEdges3;
            txtBusca.DefaultText = "";
            txtBusca.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtBusca.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtBusca.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtBusca.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtBusca.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBusca.Font = new Font("Segoe UI", 9F);
            txtBusca.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtBusca.Location = new Point(12, 214);
            txtBusca.Name = "txtBusca";
            txtBusca.PlaceholderText = "Digite para Buscar";
            txtBusca.SelectedText = "";
            txtBusca.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtBusca.Size = new Size(504, 44);
            txtBusca.TabIndex = 27;
            // 
            // cbFiltro
            // 
            cbFiltro.BackColor = Color.Transparent;
            cbFiltro.BorderRadius = 10;
            cbFiltro.CustomizableEdges = customizableEdges1;
            cbFiltro.DrawMode = DrawMode.OwnerDrawFixed;
            cbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFiltro.FocusedColor = Color.FromArgb(94, 148, 255);
            cbFiltro.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbFiltro.Font = new Font("Segoe UI", 10F);
            cbFiltro.ForeColor = Color.FromArgb(68, 88, 112);
            cbFiltro.ItemHeight = 30;
            cbFiltro.Location = new Point(759, 171);
            cbFiltro.Name = "cbFiltro";
            cbFiltro.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cbFiltro.Size = new Size(180, 36);
            cbFiltro.TabIndex = 28;
            // 
            // frmBrinquedos
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Ivory;
            ClientSize = new Size(1184, 682);
            Controls.Add(cbFiltro);
            Controls.Add(txtBusca);
            Controls.Add(btnBuscar);
            Controls.Add(btnFiltrar);
            Controls.Add(btnResetar);
            Controls.Add(lblBrinquedoCadastrado);
            Controls.Add(btnEditarBrinquedo);
            Controls.Add(btnStatusBrinquedo);
            Controls.Add(btnCriarBrinquedo);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(guna2DataGridView1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmBrinquedos";
            Text = "frmBrinquedos";
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTituloLista;
        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2GradientButton btnCriarBrinquedo;
        private Guna.UI2.WinForms.Guna2GradientButton btnEditarBrinquedo;
        private Guna.UI2.WinForms.Guna2GradientButton btnStatusBrinquedo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBrinquedosCadastrados;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBrinquedoCadastrado;
        private Guna.UI2.WinForms.Guna2ComboBox cbFiltro;
        private Guna.UI2.WinForms.Guna2TextBox txtBusca;
        private Guna.UI2.WinForms.Guna2GradientButton btnBuscar;
        private Guna.UI2.WinForms.Guna2GradientButton btnFiltrar;
        private Guna.UI2.WinForms.Guna2Button btnResetar;
    }
}
