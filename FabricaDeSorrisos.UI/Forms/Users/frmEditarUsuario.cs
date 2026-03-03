using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FabricaDeSorrisos.UI.ViewModels.UserViewModels;

namespace FabricaDeSorrisos.UI.Forms.Users
{
    public partial class frmEditarUsuario : Form
    {
        private UserViewModel _model;
        public frmEditarUsuario()
        {
            InitializeComponent();
        }
        public frmEditarUsuario(UserViewModel model)
        {
            InitializeComponent();
            _model = model;
            txtNome.Text = model.Nome ?? "";
            txtEmail.Text = model.Email ?? "";
            cbCargo.Items.Clear();
            cbCargo.Items.Add("Admin");
            cbCargo.Items.Add("Gerente");
            cbCargo.Items.Add("Cliente");
            var cargo = string.IsNullOrWhiteSpace(model.TipoUsuario) ? "Cliente" : (
                model.TipoUsuario.Contains("Admin", StringComparison.OrdinalIgnoreCase) ? "Admin" :
                model.TipoUsuario.Contains("Gerente", StringComparison.OrdinalIgnoreCase) ? "Gerente" : "Cliente");
            cbCargo.SelectedItem = cargo;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
