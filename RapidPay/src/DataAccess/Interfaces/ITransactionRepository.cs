namespace RapidPay.DataAccess.Interfaces;

public interface ITransactionRepository
{
    Task InsertTransaction(string cardnumber, decimal amount, decimal fee);

    Task<bool> TransactionExists(string cardnumber, decimal amount);
}