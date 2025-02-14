using System.ComponentModel.DataAnnotations;

namespace RapidPay.Models;

public class CardDetails
{
    public int Id { get; set; }

    [Required]
    [StringLength(15)]
    public required string Number { get; set; }

    public bool Active { get; set; }

    public float Balance { get; set; }

    public float? Limit { get; set; }
}