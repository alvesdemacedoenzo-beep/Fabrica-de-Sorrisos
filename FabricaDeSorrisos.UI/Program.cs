using System;
using System.Windows.Forms;
using FabricaDeSorrisos.UI.Forms;

namespace FabricaDeSorrisos.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmLogin());
        }
    }
}
