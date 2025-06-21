using Drogeria.Models;

namespace Drogeria.Data;

public class RoleSeeder
{
    public static void SeedRoles(DrogeriaContext ctx)
    {
        if (ctx.Roles.Any()) return;

        var roles = new[]
        {
            new Role { Name = "Kierownik",   Description = "Pełen dostęp" },
            new Role { Name = "Kasjer",      Description = "Sprzedaż i klienci" },
            new Role { Name = "Magazynier",  Description = "Magazyn i zamówienia" }
        };

        ctx.Roles.AddRange(roles);
        ctx.SaveChanges();
    }
}