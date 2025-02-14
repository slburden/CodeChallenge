using RapidPay.Models;

namespace RapidPay.DataAccess.Interfaces;

public interface ICardRepository
{
    Task<IEnumerable<CardDetails>> GetAll();
}