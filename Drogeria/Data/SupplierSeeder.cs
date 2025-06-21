using Bogus;
using Drogeria.Data;
using Drogeria.Models;

public static class SupplierSeeder
{
    public static void SeedSuppliers(DrogeriaContext ctx)
    {
        if (ctx.Suppliers.Any()) return;

        var faker = new Faker<Supplier>()
            .RuleFor(s => s.Name,  f => f.Company.CompanyName())
            .RuleFor(s => s.Email, f => f.Internet.Email())
            .RuleFor(s => s.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(s => s.IsActive, true);

        var suppliers = faker.Generate(5);
        ctx.Suppliers.AddRange(suppliers);

        ctx.SaveChanges();
    }
}