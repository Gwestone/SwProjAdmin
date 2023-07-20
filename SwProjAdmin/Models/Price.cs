using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwProjAdmin.Models;

[Table("Prices")]
public class Price
{
    [Key]
    public int Id { get; set; }

    public double Amount { get; set; }

    [ForeignKey("Currency")]
    public int CurrencyId { get; set; }

    public Currency Currency { get; set; }
    
    [ForeignKey("Product")]
    public string ProductId { get; set; }

    public Product Product { get; set; }
}