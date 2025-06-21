using Microsoft.EntityFrameworkCore;
using Drogeria.Models;

namespace Drogeria.Data;


public class DrogeriaContext : DbContext
{
    public DbSet<Category>          Categories          => Set<Category>();
    public DbSet<Product>           Products            => Set<Product>();
    public DbSet<Supplier>          Suppliers           => Set<Supplier>();
    public DbSet<PurchaseOrder>     PurchaseOrders      => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderLine> PurchaseOrderLines  => Set<PurchaseOrderLine>();
    public DbSet<Customer>          Customers           => Set<Customer>();
    public DbSet<Sale>              Sales               => Set<Sale>();
    public DbSet<SaleItem>          SaleItems           => Set<SaleItem>();
    public DbSet<Employee>          Employees           => Set<Employee>();
    public DbSet<Role>              Roles               => Set<Role>();
    public DbSet<InventoryMovement> InventoryMovements  => Set<InventoryMovement>();
    public DbSet<StockLevel>        StockLevels         => Set<StockLevel>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=Drogeria;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

        protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Category>()
         .HasOne(c => c.ParentCategory)
         .WithMany(c => c.Children)
         .HasForeignKey(c => c.ParentCategoryId)
         .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Product>()
         .Property(p => p.Price).HasPrecision(10, 2);
        b.Entity<Product>()
         .Property(p => p.VatRate).HasPrecision(4, 2);
        b.Entity<Product>()
         .HasIndex(p => p.Name);
        b.Entity<Product>()
         .HasQueryFilter(p => p.IsActive);

        b.Entity<PurchaseOrderLine>()
         .HasOne(l => l.PurchaseOrder)
         .WithMany(o => o.Lines)
         .HasForeignKey(l => l.PurchaseOrderId);

        b.Entity<PurchaseOrderLine>()
         .HasOne(l => l.Product)
         .WithMany(p => p.PurchaseOrderLines)
         .HasForeignKey(l => l.ProductId);

        b.Entity<PurchaseOrder>()
         .HasOne(po => po.Supplier)
         .WithMany(s => s.PurchaseOrders)
         .HasForeignKey(po => po.SupplierId)
         .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Sale>()
         .HasMany(s => s.Items)
         .WithOne(i => i.Sale)
         .HasForeignKey(i => i.SaleId)
         .OnDelete(DeleteBehavior.Restrict);

        b.Entity<SaleItem>()
         .HasOne(i => i.Product)
         .WithMany(p => p.SaleItems)
         .HasForeignKey(i => i.ProductId)
         .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Sale>()
         .Property(s => s.PaymentMethod)
         .HasConversion<string>();

        b.Entity<Employee>()
         .HasOne(e => e.Role)
         .WithMany(r => r.Employees)
         .HasForeignKey(e => e.RoleId)
         .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Employee>()
         .HasQueryFilter(e => e.IsActive);

        b.Entity<InventoryMovement>()
         .Property(m => m.MovementType)
         .HasConversion<string>();

        b.Entity<InventoryMovement>()
         .HasOne(m => m.Product)
         .WithMany(p => p.InventoryMovements)
         .HasForeignKey(m => m.ProductId)
         .OnDelete(DeleteBehavior.Restrict);

        b.Entity<StockLevel>()
         .HasKey(s => s.ProductId);
        b.Entity<StockLevel>()
         .HasOne(s => s.Product)
         .WithOne(p => p.StockLevel)
         .HasForeignKey<StockLevel>(s => s.ProductId)
         .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Category>().HasQueryFilter(c => c.IsActive);
        b.Entity<Supplier>().HasQueryFilter(su => su.IsActive);
        b.Entity<Customer>().HasQueryFilter(cu => cu.IsActive);

        base.OnModelCreating(b);
    }
}
