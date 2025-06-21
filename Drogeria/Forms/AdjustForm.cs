using Drogeria.Data;
using Drogeria.Models;

namespace Drogeria.Forms;

public partial class AdjustForm : Form
{
    public int  SelectedProductId => ((Product)cmbProducts.SelectedItem!).ProductId;
    public int  Quantity          => (int)numQty.Value;
    public bool IsMinus           => rbMinus.Checked;
    public string Reason          => txtReason.Text.Trim();

    private readonly DrogeriaContext _ctx;

    private readonly ComboBox      cmbProducts = new() { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly NumericUpDown numQty      = new() { Dock = DockStyle.Top, Minimum = 1, Maximum = 10000, Value = 1 };
    private readonly RadioButton   rbPlus      = new() { Text = "+ (zwiększ)", Dock = DockStyle.Left, Checked = true };
    private readonly RadioButton   rbMinus     = new() { Text = "– (zmniejsz)", Dock = DockStyle.Left };
    private readonly TextBox       txtReason   = new() { Dock = DockStyle.Top, PlaceholderText = "Powód korekty…" };
    private readonly Button        btnOk       = new() { Text = "OK", Dock = DockStyle.Right, DialogResult = DialogResult.OK };
    private readonly Button        btnCancel   = new() { Text = "Anuluj", Dock = DockStyle.Right, DialogResult = DialogResult.Cancel };

    public AdjustForm(DrogeriaContext ctx)
    {
        _ctx = ctx;

        Text            = "Korekta magazynowa";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition   = FormStartPosition.CenterParent;
        AcceptButton    = btnOk;
        CancelButton    = btnCancel;
        ClientSize      = new Size(380, 180);

        var pnlRadio = new Panel { Dock = DockStyle.Top, Height = 30 };
        pnlRadio.Controls.AddRange(new Control[] { rbMinus, rbPlus });

        Controls.Add(txtReason);
        Controls.Add(pnlRadio);
        Controls.Add(numQty);
        Controls.Add(cmbProducts);
        Controls.Add(new Panel
        {
            Dock = DockStyle.Bottom, Height = 40,
            Controls = { btnOk, btnCancel }
        });

        cmbProducts.DataSource    = _ctx.Products.Where(p => p.IsActive)
                                                 .OrderBy(p => p.Name)
                                                 .ToList();
        cmbProducts.DisplayMember = "Name";
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (DialogResult == DialogResult.OK && Reason.Length == 0)
        {
            MessageBox.Show("Wpisz krótki powód korekty.", "Walidacja",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
        }
    }
}
