using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwProjAdmin.Models;

[Table("AttributeSets")]
public class AttributeSet
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();
    
    [ForeignKey("Product")]
    public string ProductId { get; set; }

    public Product Product;
}