using System.Threading.Tasks;

using Moq;

using NUnit.Framework;

using RapidPay.Business.Interfaces;
using RapidPay.Business.Services;
using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace Tests
{
    public class CardService_Balance_Tests
    {
        private ICardRepository _cardRepository;
        private IPaymentAuthService _paymentAuthService;

        [SetUp]
        public void Setup()
        {
            var dict = new Dictionary<string, CardDetails>(){
                {
                    "599180382130527",
                    new CardDetails(){
                        Number = "599180382130527",
                        Active = true,
                        Balance=225.0f,
                        Limit=1000
                    }
                },
                {
                    "123456789012345",
                    new CardDetails() {
                        Number = "123456789012345",
                        Active = true,
                        Balance = 345.0f,
                        Limit = 25000
                    }
                },
                {
                    "606481876887782",
                    new CardDetails() {
                        Number = "606481876887782",
                        Active = true,
                        Balance = 1345.0f,
                        Limit = null
                    }
                }
            };


            var cardRepoMock = new Mock<ICardRepository>();
            cardRepoMock.Setup(c => c.GetCardByNumber(It.IsAny<string>())).Returns((string c) =>
            {
                return Task.FromResult(dict[c]);
            });


            var paymentAuthMock = new Mock<IPaymentAuthService>();

            _cardRepository = cardRepoMock.Object;
            _paymentAuthService = paymentAuthMock.Object;
        }

        [Test]
        [TestCase("606481876887782", 1345.0f, null)]
        [TestCase("123456789012345", 345.0f, 25000f)]
        [TestCase("599180382130527", 225.0f, 1000f)]
        public async Task Card_Get_Balance_Test(string cardnum, float balance, float? limit)
        {
            var service = new CardService(_cardRepository, _paymentAuthService);


            var result = await service.GetBalance(cardnum);

            Assert.That(result.CardNumber, Is.EqualTo(cardnum));
            Assert.That(result.Balance, Is.EqualTo(balance));
            Assert.That(result.CreditLimit, Is.EqualTo(limit));
        }
    }
}