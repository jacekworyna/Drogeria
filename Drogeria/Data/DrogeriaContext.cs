using Microsoft.EntityFrameworkCore;
using Drogeria.Models;

namespace Drogeria.Data;

/// <summary>
/// Kontekst EF Core - mapuje encje domenowe na tabele bazy „Drogeria”.
/// </summary>
public class DrogeriaContext : DbContext
{
    // -------------------- DbSety --------------------
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

    // -------------------- Konfiguracja połączenia --------------------
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer(
                // ► wrzuć ten connection-string do secrets.json / appsettings.json
                "Server=(localdb)\\MSSQLLocalDB;Database=Drogeria;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

    // -------------------- Mapowanie encji (Fluent API) --------------------
    protected override void OnModelCreating(ModelBuilder b)
    {
        // ------- Category ↔ Category (drzewo)
        b.Entity<Category>()
         .HasOne(c => c.ParentCategory)
         .WithMany(c => c.Children)
         .HasForeignKey(c => c.ParentCategoryId)
         .OnDelete(DeleteBehavior.Restrict);

        // ------- Product
        b.Entity<Product>()
         .Property(p => p.Price).HasPrecision(10, 2);
        b.Entity<Product>()
         .Property(p => p.VatRate).HasPrecision(4, 2);
        b.Entity<Product>()
         .HasIndex(p => p.Name);
        // filtr globalny (ukrywa nieaktywne)
        b.Entity<Product>()
         .HasQueryFilter(p => p.IsActive);

        // ------- PurchaseOrder ↔ PurchaseOrderLine
        b.Entity<PurchaseOrderLine>()
         .HasOne(l => l.PurchaseOrder)
         .WithMany(o => o.Lines)
         .HasForeignKey(l => l.PurchaseOrderId);

        // ------- Sale ↔ SaleItem
        b.Entity<Sale>()
         .HasMany(s => s.Items)
         .WithOne(i => i.Sale)
         .HasForeignKey(i => i.SaleId);
        b.Entity<SaleItem>()
         .HasOne(i => i.Product)
         .WithMany(p => p.SaleItems)
         .HasForeignKey(i => i.ProductId)
         .OnDelete(DeleteBehavior.Restrict);
        b.Entity<Sale>()
         .Property(s => s.PaymentMethod)
         .HasConversion<string>();              // enum → tekst

        // ------- Employee ↔ Role
        b.Entity<Employee>()
         .HasOne(e => e.Role)
         .WithMany(r => r.Employees)
         .HasForeignKey(e => e.RoleId);
        b.Entity<Employee>()
         .HasQueryFilter(e => e.IsActive);

        // ------- InventoryMovement
        b.Entity<InventoryMovement>()
         .Property(m => m.MovementType)
         .HasConversion<string>();              // enum → tekst
        b.Entity<InventoryMovement>()
         .HasOne(m => m.Product)
         .WithMany(p => p.InventoryMovements)
         .HasForeignKey(m => m.ProductId);

        // ------- StockLevel (1-do-1 z Product)
        b.Entity<StockLevel>()
         .HasKey(s => s.ProductId);
        b.Entity<StockLevel>()
         .HasOne(s => s.Product)
         .WithOne(p => p.StockLevel)
         .HasForeignKey<StockLevel>(s => s.ProductId);

        // ------- Pozostałe filtry on/off
        b.Entity<Category>().HasQueryFilter(c => c.IsActive);
        b.Entity<Supplier>().HasQueryFilter(su => su.IsActive);
        b.Entity<Customer>().HasQueryFilter(cu => cu.IsActive);

        base.OnModelCreating(b);
    }
}
