using RapidPay.Models;

namespace RapidPay.Business.Interfaces;

public interface ICardService
{
    Task<CardDetails> CreateNewCard(float? limit);

    Task<CardDetails> GetCard(string number);

    Task<AuthorizationResult> IsPaymentAuthorized(string cardnumber, float amount);

    Task<CardDetails> UpdateCard(CardDetails card);

    Task<CardDetails> MakePayment(Payment payment);

    Task<BalanceResult> GetBalance(string number);
}