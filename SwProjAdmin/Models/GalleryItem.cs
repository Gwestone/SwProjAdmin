using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwProjAdmin.Models;

[Table("Gallery")]
public class GalleryItem
{
    [Key]
    public int Id { get; set; }

    public string URL { get; set; }

    [ForeignKey("Product")]
    public string ProductId { get; set; }

    public Product Product { get; set; }
}