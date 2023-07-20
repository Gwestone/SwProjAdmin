using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwProjAdmin.Models;

[Table("Attributes")]
public class Attribute
{
    [Key]
    public int Id { get; set; }

    public string Value { get; set; }

    public string DisplayValue { get; set; }
    
    [ForeignKey("AttributeSet")]
    public string AttributeSetId { get; set; }
    
    public AttributeSet AttributeSet { get; set; }
}