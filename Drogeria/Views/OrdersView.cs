using Drogeria.Data;
using Drogeria.Models;

namespace Drogeria.Views
{
    public partial class OrdersView : UserControl
    {
        private readonly DrogeriaContext _ctx = new();

        public OrdersView() { InitializeComponent(); }

        private void OrdersView_Load(object? sender, EventArgs e) => RefreshGrid();

        /* ---------- UI ---------- */
        private DataGridView dgv = new() { Dock = DockStyle.Fill, ReadOnly = true };
        private Button btnNew   = new() { Text = "Nowe zamówienie", Dock = DockStyle.Top };
        private Button btnRecv  = new() { Text = "Oznacz jako dostarczone", Dock = DockStyle.Top };

        private void InitializeComponent()
        {
            Controls.Add(dgv);
            Controls.Add(btnRecv);
            Controls.Add(btnNew);
            btnNew.Click  += (_, _) => CreateOrder();
            btnRecv.Click += (_, _) => MarkDelivered();
            Load += OrdersView_Load;
        }

        private void RefreshGrid()
        {
            dgv.DataSource = _ctx.PurchaseOrders
                .Select(o => new {
                    o.PurchaseOrderId,
                    Dostawca = o.Supplier.Name,
                    o.OrderDate,
                    o.ExpectedDelivery,
                    Status = o.Status.ToString()   // <- .ToString()
                })
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        private void CreateOrder()
        {
            // TODO: kreator zamówienia (braki + ręczne dodawanie pozycji)
            MessageBox.Show("Kreator zamówienia – do zaimplementowania.");
        }

        private void MarkDelivered()
        {
            if (dgv.CurrentRow?.DataBoundItem is not { } row) return;
            int poId = (int)row.GetType().GetProperty("PurchaseOrderId")!.GetValue(row)!;
            var po   = _ctx.PurchaseOrders.Find(poId);
            if (po is null) return;

            po.Status = PurchaseOrderStatus.Delivered;
            _ctx.SaveChanges();
            RefreshGrid();
        }
    }
}
