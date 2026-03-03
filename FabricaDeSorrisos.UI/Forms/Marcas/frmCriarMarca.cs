using System;
using System.Windows.Forms;
using FabricaDeSorrisos.UI.Services;

namespace FabricaDeSorrisos.UI.Forms.Marcas
{
    public partial class frmCriarMarca : Form
    {
        private readonly MarcaService _service = new MarcaService();
        public frmCriarMarca()
        {
            InitializeComponent();
            btnCriar.Click += BtnCriar_Click;
            btnFechar.Click += (s, e) => Close();
            // Se existir segundo botão de criar, apontar para o mesmo handler
            try { guna2Button1.Click += BtnCriar_Click; } catch { }
        }

        private async void BtnCriar_Click(object? sender, EventArgs e)
        {
            var nome = txtNomeCategoria.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome da marca.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ok = await _service.CreateAsync(nome);
            if (!ok)
            {
                MessageBox.Show("Não foi possível criar a marca. Verifique a API.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Marca criada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
