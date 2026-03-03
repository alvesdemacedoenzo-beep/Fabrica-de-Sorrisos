using System.Windows.Forms;

namespace FabricaDeSorrisos.UI.Helpers
{
    public static class MessageHelper
    {
        public static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Erro",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
