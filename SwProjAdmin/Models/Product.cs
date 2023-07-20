using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwProjAdmin.Models;

[Table("Products")]
public class Product
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }
    
    public int inStock { get; set; }
    
    public string Description { get; set; }
    
    public string Brand { get; set; }
    
    [ForeignKey("Category")]
    public string CategoryId { get; set; }
    
    public Category Category { get; set; }

    public ICollection<GalleryItem> GalleryItems { get; set; } = new List<GalleryItem>();

    public ICollection<Price> Prices { get; set; } = new List<Price>();

    public ICollection<AttributeSet> AttributeSets { get; set; } = new List<AttributeSet>();
}