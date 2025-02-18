using RapidPay.Models;

namespace RapidPay.Business.Interfaces;

public interface ICardService
{
    Task<CardDetails> CreateNewCard(decimal? limit);

    Task<CardDetails> GetCard(string number);

    Task<AuthorizationResult> IsPaymentAuthorized(Payment payment);

    Task<CardDetails> UpdateCard(CardDetails card);

    Task<CardDetails> MakePayment(Payment payment);

    Task<BalanceResult> GetBalance(string number);
}