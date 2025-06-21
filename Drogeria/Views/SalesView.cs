using Drogeria.Data;
using Drogeria.Forms;
using Microsoft.EntityFrameworkCore;

namespace Drogeria.Views;

public class SalesView : UserControl
{
    private readonly DrogeriaContext _ctx = new();

    private readonly DataGridView _dgv = new()
    {
        Dock = DockStyle.Fill,
        AutoGenerateColumns = true,
        ReadOnly = true
    };

    private readonly Button _btnNewSale = new()
    {
        Text = "Nowa sprzedaż",
        Dock = DockStyle.Top,
        Height = 32
    };

    private readonly Button _btnRefresh = new()
    {
        Text = "Odśwież",
        Dock = DockStyle.Top,
        Height = 32
    };

    public SalesView()
    {
        InitializeComponent();
        Load += (_, _) => LoadTodaySales();
    }

    private void InitializeComponent()
    {
        Controls.Add(_dgv);
        Controls.Add(_btnRefresh);
        Controls.Add(_btnNewSale);

        _btnRefresh.Click += (_, _) => LoadTodaySales();
        _btnNewSale.Click += (_, _) => CreateSampleSale();

        Size = new Size(650, 450);
    }

    private void LoadTodaySales()
    {
        var today = DateTime.Today;
        var list = _ctx.SaleItems
                       .Where(i => EF.Functions.DateDiffDay(i.Sale.SaleDate, today) == 0)
                       .Select(i => new
                       {
                           i.SaleId,
                           Produkt = i.Product.Name,
                           i.Quantity,
                           Netto = i.UnitPrice,
                           Brutto = i.UnitPrice * (1 + i.VatRate)
                       })
                       .OrderBy(i => i.SaleId)
                       .ToList();

        _dgv.DataSource = list;
    }

    private void CreateSampleSale()
    {
        using var form = new NewSaleForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            MessageBox.Show("Sprzedaż zapisana.");
            LoadTodaySales();
        }
    }



    protected override void Dispose(bool disposing)
    {
        if (disposing)
            _ctx.Dispose();
        base.Dispose(disposing);
    }
}
