using RapidPay.Business.Interfaces;
using RapidPay.DataAccess;
using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.Business.Services;

public class PaymentAuthService : IPaymentAuthService
{
    private readonly IAuthAuditRepository _authAuditRepository;
    private readonly ICardRepository _cardRepository;

    private readonly ITransactionService _transactionService;

    public PaymentAuthService(IAuthAuditRepository authAuditRepository, ICardRepository cardRepository, ITransactionService transactionService)
    {
        _authAuditRepository = authAuditRepository;
        _cardRepository = cardRepository;
        _transactionService = transactionService;
    }

    public async Task<AuthorizationResult> AuthorizeCard(string cardnumber, decimal amount)
    {
        var card = await _cardRepository.GetCardByNumber(cardnumber);
        var result = new AuthorizationResult()
        {
            Authorized = true
        };

        if (!card.Active)
        {
            result = new AuthorizationResult()
            {
                Authorized = false,
                DenialReason = "Card not activated"
            };
        }

        if (card.Limit != null)
        {
            if (card.Balance + amount > card.Limit)
            {
                result = new AuthorizationResult()
                {
                    Authorized = false,
                    DenialReason = "Insufficient funds"
                };
            }
        }

        if (result.Authorized && await _transactionService.TransactionExists(cardnumber, amount)) {
              result = new AuthorizationResult{
                Authorized = false,
                DenialReason = "Duplicate Transaction"
              };
        }

        await _authAuditRepository.InsertAudit(new AuthAuditRecord()
        {
            CardNumber = card.Number,
            Amount = amount,
            Approved = result.Authorized,
            DenialReason = result.DenialReason
        });

        return result;
    }
}