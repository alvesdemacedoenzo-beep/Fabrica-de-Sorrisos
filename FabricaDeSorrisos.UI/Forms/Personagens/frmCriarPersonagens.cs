using System;
using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Forms.Personagens
{
    public partial class frmCriarPersonagens : Form
    {
        public frmCriarPersonagens()
        {
            InitializeComponent();
            btnCriarPersonagem.Click += BtnCriarPersonagem_Click;
            btnFechar.Click += (s, e) => Close();
        }

        private void BtnCriarPersonagem_Click(object? sender, EventArgs e)
        {
            var nome = btnNomePersonagem.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome do personagem.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnNomePersonagem.Focus();
                return;
            }
            Tag = nome;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
