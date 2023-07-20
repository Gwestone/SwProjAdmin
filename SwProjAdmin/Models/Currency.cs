using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwProjAdmin.Models;

[Table("Currencies")]
public class Currency
{
    [Key]
    public int Id { get; set; }

    public string Label { get; set; }

    public string Symbol { get; set; }

    [ForeignKey("Price")]
    public int PriceId { get; set; }

    public Price Price { get; set; }
}