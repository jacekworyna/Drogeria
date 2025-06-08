using Drogeria.Data;
using Drogeria.Models;

namespace Drogeria.Forms;

public partial class OrderWizardForm : Form
{
    /* -------- public out -------- */
    public int SelectedSupplierId => ((Supplier)cmbSupplier.SelectedItem!).SupplierId;
    public List<(int productId, int qty, decimal unitCost)> Lines { get; } = new();

    /* -------- zależności -------- */
    private readonly DrogeriaContext _ctx;

    /* -------- UI -------- */
    private readonly ComboBox      cmbSupplier = new() { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly DataGridView  dgv         = new() { Dock = DockStyle.Fill, AllowUserToAddRows = true };
    private readonly Button        btnAuto     = new() { Text = "Dodaj braki", Dock = DockStyle.Top };
    private readonly Button        btnOk       = new() { Text = "Zapisz", Dock = DockStyle.Right, DialogResult = DialogResult.OK };
    private readonly Button        btnCancel   = new() { Text = "Anuluj", Dock = DockStyle.Right, DialogResult = DialogResult.Cancel };

    public OrderWizardForm(DrogeriaContext ctx)
    {
        _ctx = ctx;

        Text            = "Kreator zamówienia";
        FormBorderStyle = FormBorderStyle.SizableToolWindow;
        StartPosition   = FormStartPosition.CenterParent;
        Width  = 700;
        Height = 500;

        Controls.Add(dgv);
        Controls.Add(btnAuto);
        Controls.Add(cmbSupplier);
        Controls.Add(new Panel
        {
            Dock = DockStyle.Bottom, Height = 40,
            Controls = { btnOk, btnCancel }
        });

        /* ---- dostawcy ---- */
        cmbSupplier.DataSource    = _ctx.Suppliers.Where(s => s.IsActive)
                                                  .OrderBy(s => s.Name)
                                                  .ToList();
        cmbSupplier.DisplayMember = "Name";

        /* ---- siatka ---- */
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID",       Width = 60 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Produkt",  Width = 250 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ilość",    Width = 60 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cost",     Width = 80 });

        btnAuto.Click += (_, _) => AddShortages();
    }

    /* ------ braki poniżej progu ------ */
    private void AddShortages()
    {
        var shortList = _ctx.StockLevels
                            .Where(s => s.QtyOnHand < s.ReorderLevel)
                            .Select(s => new
                            {
                                s.Product.ProductId,
                                s.Product.Name,
                                QtyToOrder = s.ReorderLevel - s.QtyOnHand,
                                s.Product.Price
                            })
                            .ToList();

        foreach (var p in shortList)
            dgv.Rows.Add(p.ProductId, p.Name, p.QtyToOrder, p.Price);
    }

    /* ------ walidacja i zwrot ------ */
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (DialogResult != DialogResult.OK) return;

        Lines.Clear();
        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.IsNewRow) continue;
            if (!int.TryParse(row.Cells[0].Value?.ToString(), out int prodId) ||
                !int.TryParse(row.Cells[2].Value?.ToString(), out int qty) ||
                !decimal.TryParse(row.Cells[3].Value?.ToString(), out decimal cost) ||
                qty <= 0 || cost < 0)
            {
                MessageBox.Show("Popraw dane wierszy (ID, Ilość, Cost).",
                                "Walidacja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            Lines.Add((prodId, qty, cost));
        }
    }
}
