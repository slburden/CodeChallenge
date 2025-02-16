using RapidPay.Business.Interfaces;
using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.Business.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;
    private readonly IPaymentAuthService _paymentAuthService;

    private readonly IUFEService _uFEService;

    public CardService(ICardRepository cardRepository, IPaymentAuthService paymentAuthService, IUFEService uFEService)
    {
        _cardRepository = cardRepository;
        _paymentAuthService = paymentAuthService;
        _uFEService = uFEService;
    }

    public async Task<CardDetails> CreateNewCard(float? limit)
    {
        var rand = new Random();

        var cardNum = GetRandomCardNumber();

        while (await _cardRepository.CardExists(cardNum))
        {
            // Double checks to make sure our new "random" card number is unique
        }

        var card = new CardDetails()
        {
            Number = cardNum,
            Balance = rand.NextSingle() * (limit ?? 100000),
            Limit = limit,
            Active = false
        };

        await _cardRepository.UpsertCard(card);

        var result = await _cardRepository.GetCardByNumber(card.Number);

        return result;
    }

    public async Task<BalanceResult> GetBalance(string number)
    {
        var card = await GetCard(number);

        return new BalanceResult()
        {
            Balance = card.Balance,
            CardNumber = card.Number,
            CreditLimit = card.Limit
        };
    }


    public async Task<CardDetails> GetCard(string number)
    {
        var card = await _cardRepository.GetCardByNumber(number);

        return card;
    }

    public async Task<AuthorizationResult> IsPaymentAuthorized(string cardnumber, float amount)
    {
        return await _paymentAuthService.AuthorizeCard(cardnumber, amount);
    }

    public async Task<CardDetails> MakePayment(Payment payment)
    {
        var card = await _cardRepository.GetCardByNumber(payment.CardNumber);

        var rate = await _uFEService.GetRate();

        card.Balance += payment.Amount + rate.Rate;

        return await UpdateCard(card);
    }


    public async Task<CardDetails> UpdateCard(CardDetails card)
    {
        await _cardRepository.UpsertCard(card);

        return await _cardRepository.GetCardByNumber(card.Number);
    }

    private string GetRandomCardNumber()
    {
        var chars = "0123456789";
        var data = "";
        var rand = new Random();
        for (int i = 0; i < 15; i++)
        {
            data += chars[rand.Next(0, chars.Length)];
        }

        return data;
    }
}