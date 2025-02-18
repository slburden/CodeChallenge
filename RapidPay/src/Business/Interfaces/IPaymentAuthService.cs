using RapidPay.Models;

namespace RapidPay.Business.Interfaces;

public interface IPaymentAuthService
{
    Task<AuthorizationResult> AuthorizeCard(Payment payment);
}