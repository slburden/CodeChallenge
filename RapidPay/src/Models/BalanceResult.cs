namespace RapidPay.Models;

public class BalanceResult
{
    public required string CardNumber { get; set; }

    public decimal Balance { get; set; }

    public decimal? CreditLimit { get; set; }
}