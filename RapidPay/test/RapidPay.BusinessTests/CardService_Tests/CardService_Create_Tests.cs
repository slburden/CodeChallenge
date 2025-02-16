using Moq;

using NUnit.Framework;

using RapidPay.Business.Interfaces;
using RapidPay.Business.Services;
using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.BusinessTests
{
    public class CardService_Create_Tests
    {
        private ICardRepository _cardRepository;
        private IPaymentAuthService _paymentAuthService;
        [SetUp]
        public void Setup()
        {
            var dict = new Dictionary<string, CardDetails>();

            var cardRepoMock = new Mock<ICardRepository>();
            cardRepoMock.Setup(c => c.CardExists(It.IsAny<string>())).Returns(Task.FromResult<bool>(false));
            cardRepoMock.Setup(c => c.UpsertCard(It.IsAny<CardDetails>())).Returns((CardDetails c) =>
            {
                if (!dict.ContainsKey(c.Number))
                {

                    dict.Add(c.Number, c);
                }
                else
                {
                    dict[c.Number] = c;
                }

                return Task.FromResult(dict[c.Number]);
            });
            cardRepoMock.Setup(c => c.GetCardByNumber(It.IsAny<string>())).Returns((string c) =>
            {
                return Task.FromResult(dict[c]);
            });

            var paymentAuthMock = new Mock<IPaymentAuthService>();

            _cardRepository = cardRepoMock.Object;
            _paymentAuthService = paymentAuthMock.Object;
        }

        [Test]
        public async Task CardHas_Null_Limit_Test()
        {
            var service = new CardService(_cardRepository, _paymentAuthService);

            var card = await service.CreateNewCard(null);

            Assert.IsNull(card.Limit);
        }

        [Test]
        [TestCase(15000)]
        [TestCase(1000)]
        public async Task CardHas_Value_Limit_Test(float expected)
        {
            var service = new CardService(_cardRepository, _paymentAuthService);

            var card = await service.CreateNewCard(expected);

            Assert.That(card.Limit, Is.EqualTo(expected));
        }
    }
}