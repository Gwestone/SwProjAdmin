using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SwProjAdmin.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public string Id { get; set; }
    
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}