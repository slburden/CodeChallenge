
using RapidPay.Business.Interfaces;
using RapidPay.DataAccess.Interfaces;

namespace RapidPay.Business.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<bool> CreateTransaction(string card_number, decimal amount, decimal fee)
    {
        try
        {
            await _transactionRepository.InsertTransaction(card_number, amount, fee);
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    public async Task<bool> TransactionExists(string card_number, decimal amount)
    {
        return await _transactionRepository.TransactionExists(card_number, amount);
    }
}