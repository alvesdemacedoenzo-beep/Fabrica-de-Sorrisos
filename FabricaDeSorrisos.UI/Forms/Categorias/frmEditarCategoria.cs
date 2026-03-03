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
    public partial class frmEditarCategoria : Form
    {
        private readonly CategoryService _service = new CategoryService();
        private readonly int _categoriaId;

        public frmEditarCategoria()
        {
            InitializeComponent();
            btnSalvarEdicao.Click += BtnSalvarEdicao_Click;
            btnFechar.Click += (s, e) => Close();
        }

        public frmEditarCategoria(int id, string nomeAtual) : this()
        {
            _categoriaId = id;
            txtNomeCategoria.Text = nomeAtual;
        }

        private async void BtnSalvarEdicao_Click(object? sender, EventArgs e)
        {
            var novoNome = txtNomeCategoria.Text?.Trim();
            if (string.IsNullOrWhiteSpace(novoNome))
            {
                MessageBox.Show("Informe o novo nome da categoria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ok = await _service.UpdateAsync(_categoriaId, novoNome);
            if (!ok)
            {
                MessageBox.Show("Não foi possível salvar a edição. Verifique a API.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Categoria atualizada com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
