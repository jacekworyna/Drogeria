using Drogeria.Data;

namespace Drogeria.Views
{
    public class ReportsView : UserControl
    {
        private readonly DrogeriaContext _ctx = new();

        public ReportsView() { InitializeComponent(); }

        private ComboBox cmbReport = new() { Dock = DockStyle.Top };
        private DateTimePicker dtFrom = new() { Dock = DockStyle.Top };
        private DataGridView dgv = new() { Dock = DockStyle.Fill, ReadOnly = true };

        private void InitializeComponent()
        {
            cmbReport.Items.AddRange(new[] { "Dzisiejsza sprzedaż", "Top 10 produktów", "Stany poniżej progu" });
            Controls.Add(dgv);
            Controls.Add(cmbReport);
            Controls.Add(dtFrom);
            cmbReport.SelectedIndexChanged += (_, _) => RunReport();
            dtFrom.ValueChanged            += (_, _) => RunReport();
            Load += (_, _) => cmbReport.SelectedIndex = 0;
        }

        private void RunReport()
        {
            switch (cmbReport.SelectedItem)
            {
                case "Dzisiejsza sprzedaż":
                    dgv.DataSource = _ctx.SaleItems
                        .Where(i => i.Sale.SaleDate.Date == DateTime.Today)
                        .Select(i => new {
                            i.Product.Name,
                            i.Quantity,
                            KwotaBrutto = i.Quantity * i.UnitPrice * (1 + i.VatRate)
                        })
                        .ToList();
                    break;

                case "Top 10 produktów":
                    dgv.DataSource = _ctx.SaleItems
                        .GroupBy(i => i.Product.Name)
                        .Select(g => new {
                            Produkt = g.Key,
                            Sztuk   = g.Sum(x => x.Quantity)
                        })
                        .OrderByDescending(x => x.Sztuk)
                        .Take(10)
                        .ToList();
                    break;

                case "Stany poniżej progu":
                    dgv.DataSource = _ctx.StockLevels
                        .Where(s => s.QtyOnHand < s.ReorderLevel)
                        .Select(s => new {
                            s.Product.Name,
                            s.QtyOnHand,
                            s.ReorderLevel
                        })
                        .ToList();
                    break;
            }
        }
    }
}
