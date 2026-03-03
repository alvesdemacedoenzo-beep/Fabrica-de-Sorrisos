using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Forms.Sub_Categorias
{
    public partial class frmCriarSubCategoria : Form
    {
        public frmCriarSubCategoria()
        {
            InitializeComponent();
            btnCriar.Click += BtnCriar_Click;
            btnFechar.Click += (s, e) => Close();
        }

        private void BtnCriar_Click(object? sender, EventArgs e)
        {
            var nome = txtNome.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome da subcategoria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }
            Tag = nome;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
