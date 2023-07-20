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

    public ICollection<Price> Prices { get; set; } = new List<Price>();
}