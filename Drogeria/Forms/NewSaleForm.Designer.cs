using System.ComponentModel;

namespace Drogeria.Forms;

partial class NewSaleForm
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
        Text = "Nowa sprzedaż";
        Size = new Size(400, 200);
        StartPosition = FormStartPosition.CenterParent;

        comboBoxProducts = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(20, 20),
            Width = 340
        };

        numericQty = new NumericUpDown
        {
            Location = new Point(20, 60),
            Width = 100,
            Minimum = 1,
            Maximum = 100,
            Value = 1
        };

        btnConfirm = new Button
        {
            Text = "Zatwierdź",
            Location = new Point(20, 110),
            Width = 100
        };
        btnConfirm.Click += BtnConfirm_Click;

        btnCancel = new Button
        {
            Text = "Anuluj",
            Location = new Point(140, 110),
            Width = 100
        };
        btnCancel.Click += (_, _) => Close();

        Controls.Add(comboBoxProducts);
        Controls.Add(numericQty);
        Controls.Add(btnConfirm);
        Controls.Add(btnCancel);
    }

    #endregion
}