using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Forms.Sub_Categorias
{
    public partial class frmEditarSubCategorias : Form
    {
        public frmEditarSubCategorias()
        {
            InitializeComponent();
            btnSalvarEdição.Click += BtnSalvar_Click;
            btnFechar.Click += (s, e) => Close();
        }

        public frmEditarSubCategorias(int id, string nomeAtual) : this()
        {
            txtNome.Text = nomeAtual;
            Tag = id;
        }

        private void BtnSalvar_Click(object? sender, EventArgs e)
        {
            var nome = txtNome.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o novo nome.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }
            Tag = nome;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
