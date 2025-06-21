using System.ComponentModel;
using Drogeria.Data;
using Drogeria.Models;
using Microsoft.EntityFrameworkCore;

namespace Drogeria.Forms;

public partial class LoginForm : Form
{
    private readonly DrogeriaContext _ctx = new();

    private TextBox txtLogin = null!;
    private TextBox txtPassword = null!;
    private Button btnLogin = null!;
    private Label lblStatus = null!;

    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] 
    public Employee? LoggedUser { get; private set; }

    public LoginForm()
    {
        InitializeComponent();
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        string login = txtLogin.Text.Trim();
        string password = txtPassword.Text.Trim();

        var employee = _ctx.Employees
            .Include(e => e.Role)
            .FirstOrDefault(e => e.Login == login && e.IsActive);

        if (employee == null)
        {
            lblStatus.Text = "Nieprawidłowy login lub konto nieaktywne.";
            return;
        }

        if ((login == "kier" && password == "pass1") || (login == "kas" && password == "pass2"))
        {
            LoggedUser = employee;
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            lblStatus.Text = "Nieprawidłowe hasło.";
        }
    }
    
}
