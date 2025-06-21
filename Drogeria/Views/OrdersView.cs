using Drogeria.Data;
using Drogeria.Forms;
using Drogeria.Models;

namespace Drogeria.Views
{
    public class OrdersView : UserControl
    {
        private readonly DrogeriaContext _ctx = new();

        public OrdersView()
        {
            InitializeComponent();
        }

        private void OrdersView_Load(object? sender, EventArgs e) => RefreshGrid();

        private DataGridView dgv = new() { Dock = DockStyle.Fill, ReadOnly = true };
        private Button btnNew = new() { Text = "Nowe zamówienie", Dock = DockStyle.Top };
        private Button btnRecv = new() { Text = "Oznacz jako dostarczone", Dock = DockStyle.Top };

        private void InitializeComponent()
        {
            Controls.Add(dgv);
            Controls.Add(btnRecv);
            Controls.Add(btnNew);
            btnNew.Click += (_, _) => CreateOrder();
            btnRecv.Click += (_, _) => MarkDelivered();
            Load += OrdersView_Load;
        }

        private void RefreshGrid()
        {
            dgv.DataSource = _ctx.PurchaseOrders
                .Select(o => new
                {
                    o.PurchaseOrderId,
                    Dostawca = o.Supplier.Name,
                    o.OrderDate,
                    o.ExpectedDelivery,
                    Status = o.Status.ToString()
                })
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        private void CreateOrder()
        {
            using var dlg = new OrderWizardForm(_ctx);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            int supplierId = dlg.SelectedSupplierId;
            var lines = dlg.Lines;

            if (!lines.Any())
            {
                MessageBox.Show("Nie wybrano żadnych pozycji.", "Informacja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var po = new PurchaseOrder
            {
                SupplierId = supplierId,
                OrderDate = DateTime.UtcNow,
                ExpectedDelivery = DateTime.UtcNow.AddDays(5),
                Status = PurchaseOrderStatus.Draft
            };
            _ctx.PurchaseOrders.Add(po);
            _ctx.SaveChanges();

            foreach (var (prodId, qty, cost) in lines)
            {
                _ctx.PurchaseOrderLines.Add(new PurchaseOrderLine
                {
                    PurchaseOrderId = po.PurchaseOrderId,
                    ProductId = prodId,
                    Quantity = qty,
                    UnitCost = cost
                });
            }

            _ctx.SaveChanges();
            RefreshGrid();
        }


        private void MarkDelivered()
        {
            if (dgv.CurrentRow?.DataBoundItem is not { } row) return;
            int poId = (int)row.GetType().GetProperty("PurchaseOrderId")!.GetValue(row)!;
            var po = _ctx.PurchaseOrders.Find(poId);
            if (po is null) return;

            po.Status = PurchaseOrderStatus.Delivered;
            _ctx.SaveChanges();
            RefreshGrid();
        }
    }
}