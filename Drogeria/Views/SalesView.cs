// SalesView.cs

using Drogeria.Data;
using Microsoft.EntityFrameworkCore;

namespace Drogeria.Views;

/// <summary>
/// Zakładka „Sprzedaż dzisiaj” – pokazuje wszystkie pozycje paragonów z bieżącej daty.
/// </summary>
public partial class SalesView : UserControl
{
    private readonly DrogeriaContext _ctx = new();

    // ---------- UI ----------
    private readonly DataGridView _dgv = new()
    {
        Dock = DockStyle.Fill,
        AutoGenerateColumns = true,
        ReadOnly = true
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
        Load += (_, _) => LoadTodaySales();       // ładujemy dopiero w runtime
    }

    // ---------- Inicjalizacja kontrolek ----------
    private void InitializeComponent()
    {
        Controls.Add(_dgv);
        Controls.Add(_btnRefresh);
        _btnRefresh.Click += (_, _) => LoadTodaySales();

        // rozmiar design-time – opcjonalny, aby coś było widać w podglądzie
        Size = new Size(650, 450);
    }

    // ---------- Logika ----------
    private void LoadTodaySales()
    {
        // EF Core 8 tłumaczy DateOnly/DateTime.Date, ale DateDiffDay jest 100 % SQL-side
        var today = DateTime.Today;
        var list = _ctx.SaleItems
                       .Where(i => EF.Functions.DateDiffDay(i.Sale.SaleDate, today) == 0)
                       .Select(i => new
                       {
                           i.SaleId,
                           Produkt  = i.Product.Name,
                           i.Quantity,
                           Netto    = i.UnitPrice,
                           Brutto   = i.UnitPrice * (1 + i.VatRate)
                       })
                       .OrderBy(i => i.SaleId)
                       .ToList();

        _dgv.DataSource = list;
    }

    // ---------- Cleanup ----------
    protected override void Dispose(bool disposing)
    {
        if (disposing)
            _ctx.Dispose(); // zwolnij połączenie do bazy
        base.Dispose(disposing);
    }
}
