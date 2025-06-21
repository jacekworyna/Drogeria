using Drogeria.Data;
using Drogeria.Models;

public static class EmployeeSeeder
{
    public static void SeedEmployees(DrogeriaContext ctx)
    {
        if (ctx.Employees.Any()) return;

        var kierownik = ctx.Roles.First(r => r.Name == "Kierownik");
        var kasjer    = ctx.Roles.First(r => r.Name == "Kasjer");

        ctx.Employees.AddRange(
            new Employee
            {
                FirstName = "Anna", LastName = "Nowak",
                Login = "kier", PasswordHash = Hash("pass1"),
                RoleId = kierownik.RoleId, IsActive = true
            },
            new Employee
            {
                FirstName = "Piotr", LastName = "Kowalski",
                Login = "kas", PasswordHash = Hash("pass2"),
                RoleId = kasjer.RoleId, IsActive = true
            }
        );

        ctx.SaveChanges();
    }

    private static string Hash(string input)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(sha.ComputeHash(bytes));
    }
}