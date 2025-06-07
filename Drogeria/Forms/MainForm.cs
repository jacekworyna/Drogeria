using Drogeria.Views;

namespace Drogeria.Forms;

public partial class MainForm : Form
{
    private SalesView      _salesView      = null!;
    private InventoryView  _inventoryView  = null!;
    private OrdersView     _ordersView     = null!;
    private ReportsView    _reportsView    = null!;
    private AdminView      _adminView      = null!;

    public MainForm()
    {
        InitializeComponent();
        Load += MainForm_Load;
    }
    
    private void MainForm_Load(object sender, EventArgs e)
    {
        // 1. Utwórz instancje
        _salesView     = new SalesView()     { Dock = DockStyle.Fill };
        _inventoryView = new InventoryView() { Dock = DockStyle.Fill };
        _ordersView    = new OrdersView()    { Dock = DockStyle.Fill };
        _reportsView   = new ReportsView()   { Dock = DockStyle.Fill };
        _adminView     = new AdminView()     { Dock = DockStyle.Fill };

        // 2. Dodaj do odpowiednich stron
        tabSales.Controls.Add(_salesView);
        tabInventory.Controls.Add(_inventoryView);
        tabOrders.Controls.Add(_ordersView);
        tabReports.Controls.Add(_reportsView);
        tabAdmin.Controls.Add(_adminView);

        // 3. Wybierz zakładkę startową
        tabControl.SelectedTab = tabSales;
    }

}