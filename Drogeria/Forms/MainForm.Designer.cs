namespace Drogeria.Forms;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
        tabControl = new System.Windows.Forms.TabControl();
        tabSales = new System.Windows.Forms.TabPage();
        tabInventory = new System.Windows.Forms.TabPage();
        tabOrders = new System.Windows.Forms.TabPage();
        tabReports = new System.Windows.Forms.TabPage();
        tabAdmin = new System.Windows.Forms.TabPage();
        tabControl.SuspendLayout();
        SuspendLayout();
        // 
        // tabControl
        // 
        tabControl.Controls.Add(tabSales);
        tabControl.Controls.Add(tabInventory);
        tabControl.Controls.Add(tabOrders);
        tabControl.Controls.Add(tabReports);
        tabControl.Controls.Add(tabAdmin);
        tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
        tabControl.Location = new System.Drawing.Point(0, 0);
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new System.Drawing.Size(800, 450);
        tabControl.TabIndex = 0;
        // 
        // tabSales
        // 
        tabSales.Location = new System.Drawing.Point(4, 24);
        tabSales.Name = "tabSales";
        tabSales.Padding = new System.Windows.Forms.Padding(3);
        tabSales.Size = new System.Drawing.Size(792, 422);
        tabSales.TabIndex = 0;
        tabSales.Text = "Sprzedaż";
        tabSales.UseVisualStyleBackColor = true;
        // 
        // tabInventory
        // 
        tabInventory.Location = new System.Drawing.Point(4, 24);
        tabInventory.Name = "tabInventory";
        tabInventory.Padding = new System.Windows.Forms.Padding(3);
        tabInventory.Size = new System.Drawing.Size(792, 422);
        tabInventory.TabIndex = 1;
        tabInventory.Text = "Magazyn";
        tabInventory.UseVisualStyleBackColor = true;
        // 
        // tabOrders
        // 
        tabOrders.Location = new System.Drawing.Point(4, 24);
        tabOrders.Name = "tabOrders";
        tabOrders.Padding = new System.Windows.Forms.Padding(3);
        tabOrders.Size = new System.Drawing.Size(792, 422);
        tabOrders.TabIndex = 2;
        tabOrders.Text = "Zamówienia";
        tabOrders.UseVisualStyleBackColor = true;
        // 
        // tabReports
        // 
        tabReports.Location = new System.Drawing.Point(4, 24);
        tabReports.Name = "tabReports";
        tabReports.Padding = new System.Windows.Forms.Padding(3);
        tabReports.Size = new System.Drawing.Size(792, 422);
        tabReports.TabIndex = 3;
        tabReports.Text = "Raporty";
        tabReports.UseVisualStyleBackColor = true;
        // 
        // tabAdmin
        // 
        tabAdmin.Location = new System.Drawing.Point(4, 24);
        tabAdmin.Name = "tabAdmin";
        tabAdmin.Padding = new System.Windows.Forms.Padding(3);
        tabAdmin.Size = new System.Drawing.Size(792, 422);
        tabAdmin.TabIndex = 4;
        tabAdmin.Text = "Administracja";
        tabAdmin.UseVisualStyleBackColor = true;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(tabControl);
        Text = "Drogeria";
        tabControl.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.TabPage tabOrders;
    private System.Windows.Forms.TabPage tabReports;
    private System.Windows.Forms.TabPage tabAdmin;

    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tabSales;
    private System.Windows.Forms.TabPage tabInventory;

    #endregion
}