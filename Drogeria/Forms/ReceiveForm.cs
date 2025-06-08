using Drogeria.Data;
using Drogeria.Models;

namespace Drogeria.Forms;

public partial class ReceiveForm : Form
{
    private readonly DrogeriaContext _ctx;
    public  int SelectedProductId => ((Product)cmbProducts.SelectedItem!).ProductId;
    public  int Quantity          => (int)numQty.Value;

    private readonly ComboBox      cmbProducts = new() { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly NumericUpDown numQty      = new() { Dock = DockStyle.Top, Minimum = 1, Maximum = 10000, Value = 1 };
    private readonly Button        btnOk       = new() { Text = "OK", Dock = DockStyle.Right, DialogResult = DialogResult.OK };
    private readonly Button        btnCancel   = new() { Text = "Anuluj", Dock = DockStyle.Right, DialogResult = DialogResult.Cancel };

    public ReceiveForm(DrogeriaContext ctx)
    {
        _ctx = ctx;
        Text = "Przyjęcie dostawy";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition   = FormStartPosition.CenterParent;
        AcceptButton    = btnOk;
        CancelButton    = btnCancel;
        ClientSize      = new Size(350, 120);

        Controls.Add(numQty);
        Controls.Add(cmbProducts);
        Controls.Add(new Panel
        {
            Dock = DockStyle.Bottom, Height = 40,
            Controls = { btnOk, btnCancel }
        });

        // załaduj produkty (tylko aktywne)
        var list = _ctx.Products
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToList();
        cmbProducts.DataSource    = list;
        cmbProducts.DisplayMember = "Name";
    }
}