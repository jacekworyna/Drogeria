using System.ComponentModel.DataAnnotations;

namespace Drogeria.Models;
public class Employee
{
    public int EmployeeId { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    [Required, MaxLength(50)]
    public string Login { get; set; } = null!;

    [Required, MaxLength(256)]
    public string PasswordHash { get; set; } = null!;
    
    public bool IsActive { get; set; } = true; 

    public ICollection<Sale>? Sales { get; set; }
}