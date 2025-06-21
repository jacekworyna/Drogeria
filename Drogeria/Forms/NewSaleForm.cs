using Drogeria.Data;
using Drogeria.Models;

namespace Drogeria.Forms;

public partial class NewSaleForm : Form
{
    private readonly DrogeriaContext _ctx = new();

    private ComboBox comboBoxProducts = null!;
    private NumericUpDown numericQty = null!;
    private Button btnConfirm = null!;
    private Button btnCancel = null!;

    public NewSaleForm()
    {
        InitializeComponent();
        LoadProducts();
    }

    #region UI

    

    private void LoadProducts()
    {
        var products = _ctx.Products
            .Where(p => p.IsActive)
            .Select(p => new
            {
                p.ProductId,
                Display = $"{p.Name} ({p.Price:0.00} zł)"
            })
            .ToList();

        comboBoxProducts.DataSource = products;
        comboBoxProducts.DisplayMember = "Display";
        comboBoxProducts.ValueMember = "ProductId";
    }

    #endregion

    #region Logika zapisu

    private void BtnConfirm_Click(object? sender, EventArgs e)
    {
        if (comboBoxProducts.SelectedValue is not int productId)
        {
            MessageBox.Show("Nie wybrano produktu.");
            return;
        }

        var product = _ctx.Products.Find(productId);
        var stock = _ctx.StockLevels.FirstOrDefault(s => s.ProductId == productId);
        var qty = (int)numericQty.Value;

        if (product == null || stock == null)
        {
            MessageBox.Show("Produkt lub stan magazynowy nie istnieje.");
            return;
        }

        if (stock.QtyOnHand < qty)
        {
            MessageBox.Show("Brak wystarczającej ilości towaru na magazynie.");
            return;
        }

        var employee = _ctx.Employees.FirstOrDefault(e => e.IsActive);
        if (employee == null)
        {
            MessageBox.Show("Brak aktywnego pracownika.");
            return;
        }

        var net = Math.Round(product.Price * qty, 2);
        var gross = Math.Round(net * (1 + product.VatRate), 2);

        var sale = new Sale
        {
            SaleDate = DateTime.Now,
            PaymentMethod = PaymentMethod.Cash,
            EmployeeId = employee.EmployeeId,
            TotalNet = net,
            TotalGross = gross,
            Items = new List<SaleItem>
            {
                new SaleItem
                {
                    ProductId = product.ProductId,
                    Quantity = qty,
                    UnitPrice = product.Price,
                    VatRate = product.VatRate
                }
            }
        };

        stock.QtyOnHand -= qty;

        _ctx.Sales.Add(sale);
        _ctx.InventoryMovements.Add(new InventoryMovement
        {
            ProductId = product.ProductId,
            MovementType = MovementType.SaleOut,
            QuantityChange = -qty,
            Timestamp = DateTime.Now,
            SourceTable = "Sale"
        });

        _ctx.SaveChanges();

        MessageBox.Show("Sprzedaż została zapisana.");
        DialogResult = DialogResult.OK;
        Close();
    }

    #endregion
    
}
