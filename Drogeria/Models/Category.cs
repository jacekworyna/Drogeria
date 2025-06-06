using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drogeria.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    public int? ParentCategoryId { get; set; }

    [ForeignKey(nameof(ParentCategoryId))]
    public Category? ParentCategory { get; set; }

    public bool IsActive { get; set; } = true; 
    public ICollection<Category>? Children { get; set; }
    public ICollection<Product>? Products { get; set; }
}