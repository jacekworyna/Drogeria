using System.ComponentModel.DataAnnotations;

namespace Drogeria.Models;
public class Role
{
    public int RoleId { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    // JSON z listą uprawnień lub osobna tabela
    public string? Permissions { get; set; }

    public ICollection<Employee>? Employees { get; set; }
}