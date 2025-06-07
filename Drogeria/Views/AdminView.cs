using System;
using System.Linq;
using System.Windows.Forms;
using Drogeria.Data;

namespace Drogeria.Views
{
    public partial class AdminView : UserControl
    {
        private readonly DrogeriaContext _ctx = new();

        public AdminView() { InitializeComponent(); }

        /* ---------- UI ---------- */
        private ComboBox cmbEntity = new() { Dock = DockStyle.Top };
        private DataGridView dgv = new() { Dock = DockStyle.Fill };
        private BindingSource _bs = new();

        private void InitializeComponent()
        {
            cmbEntity.Items.AddRange(new[] { "Produkty", "Kategorie", "Dostawcy", "Pracownicy" });
            Controls.Add(dgv);
            Controls.Add(cmbEntity);
            dgv.DataSource = _bs;
            dgv.AutoGenerateColumns = true;
            cmbEntity.SelectedIndexChanged += (_, _) => LoadEntity();
            Load += (_, _) => cmbEntity.SelectedIndex = 0;
            dgv.UserDeletingRow += Dgv_UserDeletingRow;
        }

        private void LoadEntity()
        {
            switch (cmbEntity.SelectedItem)
            {
                case "Produkty":
                    _bs.DataSource = _ctx.Products.ToList();
                    break;
                case "Kategorie":
                    _bs.DataSource = _ctx.Categories.ToList();
                    break;
                case "Dostawcy":
                    _bs.DataSource = _ctx.Suppliers.ToList();
                    break;
                case "Pracownicy":
                    _bs.DataSource = _ctx.Employees.ToList();
                    break;
            }
        }

        private void Dgv_UserDeletingRow(object? sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Na pewno usunąć rekord?", "Potwierdź",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            _ctx.Remove(e.Row.DataBoundItem!);
            _ctx.SaveChanges();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            // automatyczne zapisywanie zmian po opuszczeniu zakładki
            _ctx.SaveChanges();
        }
    }
}
