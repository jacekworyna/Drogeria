using Drogeria.Models;
using Drogeria.Views;

namespace Drogeria.Forms;

public partial class MainForm : Form
{
    private readonly Employee _user;

    private SalesView     _salesView     = null!;
    private InventoryView _inventoryView = null!;
    private OrdersView    _ordersView    = null!;
    private ReportsView   _reportsView   = null!;
    private AdminView     _adminView     = null!;

    public MainForm(Employee user)
    {
        _user = user;
        InitializeComponent();
        Load += MainForm_Load;
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        Text = $"Panel główny – {_user.FirstName} ({_user.Role.Name})";

        _salesView     = new SalesView()     { Dock = DockStyle.Fill };
        _inventoryView = new InventoryView() { Dock = DockStyle.Fill };
        _ordersView    = new OrdersView()    { Dock = DockStyle.Fill };
        _reportsView   = new ReportsView()   { Dock = DockStyle.Fill };
        _adminView     = new AdminView()     { Dock = DockStyle.Fill };

        tabSales.Controls.Add(_salesView);
        tabInventory.Controls.Add(_inventoryView);
        tabOrders.Controls.Add(_ordersView);
        tabReports.Controls.Add(_reportsView);
        tabAdmin.Controls.Add(_adminView);

        switch (_user.Role.Name)
        {
            case "Kasjer":
                tabInventory.Hide();
                tabOrders.Hide();
                tabReports.Hide();
                tabAdmin.Hide();
                break;

            case "Magazynier":
                tabSales.Hide();
                tabReports.Hide();
                tabAdmin.Hide();
                break;

            case "Kierownik":
                break;

            default:
                MessageBox.Show("Nieznana rola: " + _user.Role.Name);
                Close();
                return;
        }

        if (!tabSales.Visible)
            tabControl.SelectedTab = tabInventory.Visible ? tabInventory : tabOrders;
        else
            tabControl.SelectedTab = tabSales;
    }
}
