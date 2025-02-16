namespace RapidPay.Models;

public class Payment
{
    public required string CardNumber { get; set; }

    public decimal Amount { get; set; }
}