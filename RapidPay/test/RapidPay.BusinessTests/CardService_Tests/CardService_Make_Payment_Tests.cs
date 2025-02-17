using Moq;

using NUnit.Framework;

using RapidPay.Business.Interfaces;
using RapidPay.Business.Services;
using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;


namespace RapidPay.BusinessTests;

public class CardService_Make_Payment_Tests
{
    private Mock<ICardRepository> _cardRepositoryMock;
    private Mock<IPaymentAuthService> _paymentAuthServiceMock;

    private Mock<IUFEService> _ufeServiceMock;

    private readonly decimal _expectedRate = 1.414m;

    [SetUp]
    public void Setup()
    {
        var dict = new Dictionary<string, CardDetails>(){
                {
                    "599180382130527",
                    new CardDetails(){
                        Number = "599180382130527",
                        Active = true,
                        Balance=225.0m,
                        Limit=1000
                    }
                },
                {
                    "123456789012345",
                    new CardDetails() {
                        Number = "123456789012345",
                        Active = true,
                        Balance = 345.0m,
                        Limit = 25000
                    }
                },
                {
                    "606481876887782",
                    new CardDetails() {
                        Number = "606481876887782",
                        Active = true,
                        Balance = 1345.0m,
                        Limit = null
                    }
                }
            };


        _cardRepositoryMock = new Mock<ICardRepository>();
        _cardRepositoryMock.Setup(c => c.GetCardByNumber(It.IsAny<string>())).Returns((string c) =>
       {
           return Task.FromResult(dict[c]);
       });

        _paymentAuthServiceMock = new Mock<IPaymentAuthService>();

        _ufeServiceMock = new Mock<IUFEService>();
        _ufeServiceMock.Setup(c => c.GetRate()).Returns(() =>
        {
            var rate = new UfeRate()
            {
                TimeStamp = DateTime.Now,
                Rate = _expectedRate
            };
            return Task.FromResult(rate);
        });
    }

    [Test]
    [TestCase("606481876887782", 1345.0, 225.0)]
    [TestCase("123456789012345", 345.0, 234.45)]
    [TestCase("599180382130527", 225.0, 1376.5)]
    public async Task Make_Payment_Test(string cardnum, decimal startBalance, decimal amount)
    {
        var cardService = new CardService(_cardRepositoryMock.Object, _paymentAuthServiceMock.Object, _ufeServiceMock.Object);

        var result = await cardService.MakePayment(new Payment()
        {
            CardNumber = cardnum,
            Amount = amount
        });

        var expectedBalance = startBalance + amount + _expectedRate;

        Assert.That(result.Number, Is.EqualTo(cardnum));
        Assert.That(result.Balance, Is.EqualTo(expectedBalance));
    }
}