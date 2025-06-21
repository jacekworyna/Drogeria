using System.ComponentModel;

namespace Drogeria.Forms;

partial class LoginForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>

    private void InitializeComponent()
    {
        Text = "Logowanie";
        Size = new Size(300, 220);
        StartPosition = FormStartPosition.CenterScreen;

        var lblLogin = new Label { Text = "Login:", Location = new Point(20, 20), AutoSize = true };
        txtLogin = new TextBox { Location = new Point(100, 20), Width = 150 };

        var lblPassword = new Label { Text = "Hasło:", Location = new Point(20, 60), AutoSize = true };
        txtPassword = new TextBox { Location = new Point(100, 60), Width = 150, UseSystemPasswordChar = true };

        btnLogin = new Button { Text = "Zaloguj", Location = new Point(100, 100), Width = 100 };
        btnLogin.Click += BtnLogin_Click;

        lblStatus = new Label { Location = new Point(20, 140), Width = 250, ForeColor = Color.Red };

        Controls.AddRange(new Control[] { lblLogin, txtLogin, lblPassword, txtPassword, btnLogin, lblStatus });
    }

    #endregion
}