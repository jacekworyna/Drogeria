using Drogeria.Data;
using Drogeria.Models;

public static class CategorySeeder
{
    public static void SeedCategories(DrogeriaContext ctx)
    {
        if (ctx.Categories.Any()) return;

        ctx.Categories.AddRange(
            new Category { Name = "Hair Care",   IsActive = true },
            new Category { Name = "Make-Up",     IsActive = true },
            new Category { Name = "Skin Care",   IsActive = true },
            new Category { Name = "Fragrances",  IsActive = true }
        );

        ctx.SaveChanges();
    }
}