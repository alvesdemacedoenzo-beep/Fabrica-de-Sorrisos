using System;
using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Forms.Personagens
{
    public partial class frmEditarPersonagens : Form
    {
        public frmEditarPersonagens()
        {
            InitializeComponent();
            btnConfirmarEdição.Click += BtnConfirmarEdicao_Click;
            btnFechar.Click += (s, e) => Close();
        }

        public frmEditarPersonagens(int id, string nomeAtual) : this()
        {
            txtNomePersonagem.Text = nomeAtual;
            Tag = id;
        }

        private void BtnConfirmarEdicao_Click(object? sender, EventArgs e)
        {
            var nome = txtNomePersonagem.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o novo nome do personagem.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNomePersonagem.Focus();
                return;
            }
            Tag = nome;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
