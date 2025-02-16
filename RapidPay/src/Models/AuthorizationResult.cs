namespace RapidPay.Models;

public class AuthorizationResult
{
    public bool Authorized { get; set; }

    public string? DenialReason { get; set; }
}