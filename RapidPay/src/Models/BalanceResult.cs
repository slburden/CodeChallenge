namespace RapidPay.Models;

public class BalanceResult
{
    public required string CardNumber { get; set; }

    public float Balance { get; set; }

    public float? CreditLimit { get; set; }
}