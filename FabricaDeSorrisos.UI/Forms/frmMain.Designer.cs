using System;
using System.Drawing;
using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Forms
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            panelDashboard = new Guna.UI2.WinForms.Guna2Panel();
            btnVoltar = new Guna.UI2.WinForms.Guna2GradientButton();
            btnUsuarios = new Guna.UI2.WinForms.Guna2GradientButton();
            btnPersonagens = new Guna.UI2.WinForms.Guna2GradientButton();
            btnMarcas = new Guna.UI2.WinForms.Guna2GradientButton();
            btnSubCategorias = new Guna.UI2.WinForms.Guna2GradientButton();
            btnCategorias = new Guna.UI2.WinForms.Guna2GradientButton();
            btnBrinquedos = new Guna.UI2.WinForms.Guna2GradientButton();
            pbLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            panelBotões = new Guna.UI2.WinForms.Guna2GradientPanel();
            txtCatálogo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblGestão = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            panelBotões.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.BorderRadius = 20;
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // panelDashboard
            // 
            panelDashboard.BorderRadius = 10;
            panelDashboard.CustomizableEdges = customizableEdges1;
            panelDashboard.Dock = DockStyle.Fill;
            panelDashboard.Location = new Point(262, 0);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelDashboard.Size = new Size(1184, 682);
            panelDashboard.TabIndex = 0;
            // 
            // btnVoltar
            // 
            btnVoltar.BackColor = Color.Transparent;
            btnVoltar.BorderRadius = 10;
            btnVoltar.CustomizableEdges = customizableEdges17;
            btnVoltar.FillColor = Color.SkyBlue;
            btnVoltar.FillColor2 = Color.Blue;
            btnVoltar.Font = new Font("Segoe UI", 9F);
            btnVoltar.ForeColor = Color.White;
            btnVoltar.Location = new Point(0, 602);
            btnVoltar.Name = "btnVoltar";
            btnVoltar.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnVoltar.Size = new Size(250, 56);
            btnVoltar.TabIndex = 13;
            btnVoltar.Text = "Voltar para à área de Login";
            btnVoltar.Click += btnFechar_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.CustomizableEdges = customizableEdges15;
            btnUsuarios.FillColor = Color.SkyBlue;
            btnUsuarios.FillColor2 = Color.Blue;
            btnUsuarios.Font = new Font("Segoe UI", 9F);
            btnUsuarios.ForeColor = Color.White;
            btnUsuarios.Image = Properties.Resources.user1;
            btnUsuarios.ImageAlign = HorizontalAlignment.Left;
            btnUsuarios.Location = new Point(0, 468);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnUsuarios.Size = new Size(253, 56);
            btnUsuarios.TabIndex = 10;
            btnUsuarios.Text = "Usuários";
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // btnPersonagens
            // 
            btnPersonagens.CustomizableEdges = customizableEdges13;
            btnPersonagens.FillColor = Color.SkyBlue;
            btnPersonagens.FillColor2 = Color.Blue;
            btnPersonagens.Font = new Font("Segoe UI", 9F);
            btnPersonagens.ForeColor = Color.White;
            btnPersonagens.Image = Properties.Resources.robo_do_usuario1;
            btnPersonagens.ImageAlign = HorizontalAlignment.Left;
            btnPersonagens.Location = new Point(0, 379);
            btnPersonagens.Name = "btnPersonagens";
            btnPersonagens.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnPersonagens.Size = new Size(253, 56);
            btnPersonagens.TabIndex = 6;
            btnPersonagens.Text = "Personagens";
            btnPersonagens.Click += btnPersonagens_Click;
            // 
            // btnMarcas
            // 
            btnMarcas.CustomizableEdges = customizableEdges11;
            btnMarcas.FillColor = Color.SkyBlue;
            btnMarcas.FillColor2 = Color.Blue;
            btnMarcas.Font = new Font("Segoe UI", 9F);
            btnMarcas.ForeColor = Color.White;
            btnMarcas.Image = Properties.Resources.marca_de_verificacao1;
            btnMarcas.ImageAlign = HorizontalAlignment.Left;
            btnMarcas.Location = new Point(0, 326);
            btnMarcas.Name = "btnMarcas";
            btnMarcas.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnMarcas.Size = new Size(253, 56);
            btnMarcas.TabIndex = 5;
            btnMarcas.Text = "Marcas";
            btnMarcas.Click += btnMarcas_Click;
            // 
            // btnSubCategorias
            // 
            btnSubCategorias.CustomizableEdges = customizableEdges9;
            btnSubCategorias.FillColor = Color.SkyBlue;
            btnSubCategorias.FillColor2 = Color.Blue;
            btnSubCategorias.Font = new Font("Segoe UI", 9F);
            btnSubCategorias.ForeColor = Color.White;
            btnSubCategorias.Image = Properties.Resources.categoria__1_1;
            btnSubCategorias.ImageAlign = HorizontalAlignment.Left;
            btnSubCategorias.Location = new Point(0, 269);
            btnSubCategorias.Name = "btnSubCategorias";
            btnSubCategorias.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnSubCategorias.Size = new Size(253, 56);
            btnSubCategorias.TabIndex = 4;
            btnSubCategorias.Text = "Subcategorias";
            btnSubCategorias.Click += btnSubCategorias_Click;
            // 
            // btnCategorias
            // 
            btnCategorias.CustomizableEdges = customizableEdges7;
            btnCategorias.FillColor = Color.SkyBlue;
            btnCategorias.FillColor2 = Color.Blue;
            btnCategorias.Font = new Font("Segoe UI", 9F);
            btnCategorias.ForeColor = Color.White;
            btnCategorias.Image = Properties.Resources.categoria3;
            btnCategorias.ImageAlign = HorizontalAlignment.Left;
            btnCategorias.Location = new Point(0, 212);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnCategorias.Size = new Size(253, 56);
            btnCategorias.TabIndex = 3;
            btnCategorias.Text = "Categorias";
            btnCategorias.Click += btnCategorias_Click;
            // 
            // btnBrinquedos
            // 
            btnBrinquedos.CustomizableEdges = customizableEdges5;
            btnBrinquedos.FillColor = Color.SkyBlue;
            btnBrinquedos.FillColor2 = Color.Blue;
            btnBrinquedos.Font = new Font("Segoe UI", 9F);
            btnBrinquedos.ForeColor = Color.White;
            btnBrinquedos.Image = Properties.Resources.urso_teddy1;
            btnBrinquedos.ImageAlign = HorizontalAlignment.Left;
            btnBrinquedos.Location = new Point(0, 155);
            btnBrinquedos.Name = "btnBrinquedos";
            btnBrinquedos.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnBrinquedos.Size = new Size(253, 56);
            btnBrinquedos.TabIndex = 2;
            btnBrinquedos.Text = "Brinquedos";
            btnBrinquedos.Click += btnBrinquedos_Click;
            // 
            // pbLogo
            // 
            pbLogo.CustomizableEdges = customizableEdges3;
            pbLogo.Image = Properties.Resources.logo_oficial;
            pbLogo.ImageRotate = 0F;
            pbLogo.Location = new Point(0, 0);
            pbLogo.Name = "pbLogo";
            pbLogo.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pbLogo.Size = new Size(250, 113);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogo.TabIndex = 0;
            pbLogo.TabStop = false;
            // 
            // panelBotões
            // 
            panelBotões.BackColor = Color.Transparent;
            panelBotões.BorderRadius = 10;
            panelBotões.Controls.Add(pbLogo);
            panelBotões.Controls.Add(txtCatálogo);
            panelBotões.Controls.Add(btnBrinquedos);
            panelBotões.Controls.Add(btnCategorias);
            panelBotões.Controls.Add(btnSubCategorias);
            panelBotões.Controls.Add(btnMarcas);
            panelBotões.Controls.Add(btnPersonagens);
            panelBotões.Controls.Add(lblGestão);
            panelBotões.Controls.Add(btnUsuarios);
            panelBotões.Controls.Add(btnVoltar);
            panelBotões.CustomizableEdges = customizableEdges19;
            panelBotões.Dock = DockStyle.Left;
            panelBotões.FillColor = Color.SkyBlue;
            panelBotões.FillColor2 = Color.Blue;
            panelBotões.Location = new Point(0, 0);
            panelBotões.Name = "panelBotões";
            panelBotões.ShadowDecoration.CustomizableEdges = customizableEdges20;
            panelBotões.Size = new Size(262, 682);
            panelBotões.TabIndex = 1;
            // 
            // txtCatálogo
            // 
            txtCatálogo.BackColor = Color.Transparent;
            txtCatálogo.Font = new Font("Mongolian Baiti", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCatálogo.ForeColor = Color.White;
            txtCatálogo.Location = new Point(83, 126);
            txtCatálogo.Name = "txtCatálogo";
            txtCatálogo.Size = new Size(76, 23);
            txtCatálogo.TabIndex = 1;
            txtCatálogo.Text = "Catálogo";
            // 
            // lblGestão
            // 
            lblGestão.BackColor = Color.Transparent;
            lblGestão.Font = new Font("Mongolian Baiti", 15F);
            lblGestão.ForeColor = Color.White;
            lblGestão.Location = new Point(83, 441);
            lblGestão.Name = "lblGestão";
            lblGestão.Size = new Size(59, 23);
            lblGestão.TabIndex = 9;
            lblGestão.Text = "Gestão";
            // 
            // frmMain
            // 
            ClientSize = new Size(1446, 682);
            Controls.Add(panelDashboard);
            Controls.Add(panelBotões);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmMain";
            Load += frmMain_Load;
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            panelBotões.ResumeLayout(false);
            panelBotões.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel panelDashboard;
        private Guna.UI2.WinForms.Guna2GradientPanel panelBotões;
        private Guna.UI2.WinForms.Guna2PictureBox pbLogo;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtCatálogo;
        private Guna.UI2.WinForms.Guna2GradientButton btnBrinquedos;
        private Guna.UI2.WinForms.Guna2GradientButton btnCategorias;
        private Guna.UI2.WinForms.Guna2GradientButton btnSubCategorias;
        private Guna.UI2.WinForms.Guna2GradientButton btnMarcas;
        private Guna.UI2.WinForms.Guna2GradientButton btnPersonagens;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGestão;
        private Guna.UI2.WinForms.Guna2GradientButton btnUsuarios;
        private Guna.UI2.WinForms.Guna2GradientButton btnVoltar;
    }
}
