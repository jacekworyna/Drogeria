﻿using Drogeria.Data;
using Drogeria.Forms;
using Drogeria.Models;

namespace Drogeria.Views
{
    public class InventoryView : UserControl
    {
        private readonly DrogeriaContext _ctx = new();

        public InventoryView() { InitializeComponent(); }

        private void InventoryView_Load(object sender, EventArgs e) => RefreshGrid();

        
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
            using var dlg = new ReceiveForm(_ctx);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            int productId = dlg.SelectedProductId;
            int qty       = dlg.Quantity;

            var move = new InventoryMovement
            {
                ProductId      = productId,
                MovementType   = MovementType.PurchaseIn,
                QuantityChange =  qty,
                Timestamp      = DateTime.UtcNow,
                SourceLineId   = null,
                SourceTable    = "ManualReceive"
            };
            _ctx.InventoryMovements.Add(move);

            var stock = _ctx.StockLevels.Find(productId);
            if (stock is null)
            {
                stock = new StockLevel
                {
                    ProductId    = productId,
                    QtyOnHand    = qty,
                    ReorderLevel = 5
                };
                _ctx.StockLevels.Add(stock);
            }
            else
            {
                stock.QtyOnHand += qty;
            }

            _ctx.SaveChanges();
            RefreshGrid();
        }


        private void ShowAdjustDialog()
        {
            using var dlg = new AdjustForm(_ctx);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            int  productId = dlg.SelectedProductId;
            int  qty       = dlg.Quantity;
            bool isMinus   = dlg.IsMinus;
            string reason  = dlg.Reason;

            var move = new InventoryMovement
            {
                ProductId      = productId,
                MovementType   = isMinus ? MovementType.CorrectionOut
                    : MovementType.CorrectionIn,
                QuantityChange = qty,
                Timestamp      = DateTime.UtcNow,
                SourceTable    = $"ManualAdj:{reason}",
                SourceLineId   = null
            };
            _ctx.InventoryMovements.Add(move);

            var stock = _ctx.StockLevels.Find(productId);
            if (stock == null)
            {
                MessageBox.Show("Produkt nie ma jeszcze rekordu StockLevel – korekta przerwana.",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ctx.InventoryMovements.Remove(move);
                return;
            }

            stock.QtyOnHand += isMinus ? -qty : qty;
            _ctx.SaveChanges();
            RefreshGrid();
        }

    }
}