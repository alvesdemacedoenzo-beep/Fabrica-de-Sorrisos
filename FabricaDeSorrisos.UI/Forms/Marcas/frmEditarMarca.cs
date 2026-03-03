using System;
using System.Windows.Forms;
using FabricaDeSorrisos.UI.Services;

namespace FabricaDeSorrisos.UI.Forms.Marcas
{
    public partial class frmEditarMarca : Form
    {
        private readonly MarcaService _service = new MarcaService();
        private readonly int _id;

        public frmEditarMarca()
        {
            InitializeComponent();
            btnSalvarEdição.Click += BtnSalvarEdicao_Click;
            btnFechar.Click += (s, e) => Close();
        }

        public frmEditarMarca(int id, string nomeAtual) : this()
        {
            _id = id;
            txtEditarMarca.Text = nomeAtual ?? string.Empty;
        }

        private async void BtnSalvarEdicao_Click(object? sender, EventArgs e)
        {
            var novoNome = txtEditarMarca.Text?.Trim();
            if (string.IsNullOrWhiteSpace(novoNome))
            {
                MessageBox.Show("Informe o novo nome da marca.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ok = await _service.UpdateAsync(_id, novoNome);
            if (!ok)
            {
                MessageBox.Show("Não foi possível salvar a edição. Verifique a API.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Marca atualizada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
