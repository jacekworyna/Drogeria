using System;
using System.Linq;
using System.Windows.Forms;
using Drogeria.Data;

namespace Drogeria.Views
{
    public partial class InventoryView : UserControl
    {
        private readonly DrogeriaContext _ctx = new();

        public InventoryView() { InitializeComponent(); }

        private void InventoryView_Load(object sender, EventArgs e) => RefreshGrid();

        /* ---------- UI ---------- */
        private DataGridView dgv = new() { Dock = DockStyle.Fill, ReadOnly = true };
        private Button btnReceive = new() { Text = "Przyjęcie (+)", Dock = DockStyle.Top };
        private Button btnAdjust  = new() { Text = "Korekta (±)",  Dock = DockStyle.Top };

        private void InitializeComponent()
        {
            Controls.Add(dgv);
            Controls.Add(btnAdjust);
            Controls.Add(btnReceive);
            btnReceive.Click += (_, _) => ShowReceiveDialog();
            btnAdjust.Click  += (_, _) => ShowAdjustDialog();
            Load  += InventoryView_Load;
        }

        /* ---------- Logika ---------- */
        private void RefreshGrid()
        {
            dgv.DataSource = _ctx.StockLevels
                .Select(s => new {
                    s.ProductId,
                    s.Product.Name,
                    s.QtyOnHand,
                    s.ReorderLevel
                })
                .OrderBy(p => p.Name)
                .ToList();
        }

        private void ShowReceiveDialog()
        {
            // TODO: formularz przyjęcia dostawy (ProductId, Qty)
            MessageBox.Show("Przyjęcie – do zaimplementowania.");
        }

        private void ShowAdjustDialog()
        {
            // TODO: formularz korekty (±Qty, powód)
            MessageBox.Show("Korekta – do zaimplementowania.");
        }
    }
}