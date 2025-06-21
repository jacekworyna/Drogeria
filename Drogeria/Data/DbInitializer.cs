using Drogeria.Data;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static void Seed(DrogeriaContext ctx)
    {
        ctx.Database.Migrate();

        RoleSeeder.SeedRoles(ctx);
        EmployeeSeeder.SeedEmployees(ctx);
        CategorySeeder.SeedCategories(ctx);
        SupplierSeeder.SeedSuppliers(ctx);

        ProductSeeder.SeedProducts(ctx);


        Console.WriteLine("Baza danych została uzupełniona.");
    }
}