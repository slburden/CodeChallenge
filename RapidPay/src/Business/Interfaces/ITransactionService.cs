namespace RapidPay.Business.Interfaces;

public interface ITransactionService
{
    Task<bool> CreateTransaction(string card_number, decimal amount, decimal fee);

    Task<bool> TransactionExists(string card_number, decimal amount);
}