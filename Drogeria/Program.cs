using Drogeria.Data;
using Drogeria.Forms;

namespace Drogeria;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        using var ctx = new DrogeriaContext();
        using var login = new LoginForm();
        DbInitializer.Seed(ctx);
        if (login.ShowDialog() == DialogResult.OK)
        {
            Application.Run(new MainForm(login.LoggedUser!));
        }
        else
        {
            MessageBox.Show("Dostęp zabroniony.");
        }
    }
}