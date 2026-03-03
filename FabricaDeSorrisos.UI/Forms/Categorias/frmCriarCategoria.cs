using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FabricaDeSorrisos.UI.Services;

namespace FabricaDeSorrisos.UI.Forms.Categorias
{
    public partial class frmCriarCategoria : Form
    {
        private readonly CategoryService _service = new CategoryService();

        public frmCriarCategoria()
        {
            InitializeComponent();
            btnCriarCategoria.Click += BtnCriarCategoria_Click;
            guna2CircleButton1.Click += (s, e) => Close();
        }

        private async void BtnCriarCategoria_Click(object? sender, EventArgs e)
        {
            var nome = txtNomeCategoria.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome da categoria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ok = await _service.CreateAsync(nome);
            if (!ok)
            {
                MessageBox.Show("Não foi possível criar a categoria. Verifique a API.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Categoria criada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
