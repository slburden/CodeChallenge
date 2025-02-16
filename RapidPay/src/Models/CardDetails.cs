using System.ComponentModel.DataAnnotations;

namespace RapidPay.Models;

public class CardDetails
{
    public int Id { get; set; }

    [Required]
    [StringLength(15)]
    public required string Number { get; set; }

    public bool Active { get; set; }

    public decimal Balance { get; set; }

    public decimal? Limit { get; set; }
}