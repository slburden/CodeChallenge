using RapidPay.Models;

namespace RapidPay.DataAccess.Interfaces;

public interface ICardRepository
{
    Task<IEnumerable<CardDetails>> GetAll();

    Task<CardDetails> GetCardByNumber(string number);

    Task InsertCard(CardDetails details);

    Task<bool> CardExists(string number);
}