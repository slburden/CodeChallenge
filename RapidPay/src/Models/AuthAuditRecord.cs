using System.ComponentModel.DataAnnotations;

namespace RapidPay.Models;

public class AuthAuditRecord
{
    [Required]
    public required string CardNumber { get; set; }

    public float Amount { get; set; }

    public bool Approved { get; set; }

    public string? DenialReason { get; set; }
}