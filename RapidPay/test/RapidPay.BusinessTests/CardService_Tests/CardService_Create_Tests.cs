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
        private Mock<ICardRepository> _cardRepositoryMock;
        private Mock<IPaymentAuthService> _paymentAuthServiceMock;

        private Mock<IUFEService> _ufeServiceMock;

        [SetUp]
        public void Setup()
        {
            var dict = new Dictionary<string, CardDetails>();

            _cardRepositoryMock = new Mock<ICardRepository>();
            _cardRepositoryMock.Setup(c => c.CardExists(It.IsAny<string>())).Returns(Task.FromResult<bool>(false));
            _cardRepositoryMock.Setup(c => c.UpsertCard(It.IsAny<CardDetails>())).Returns((CardDetails c) =>
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
            _cardRepositoryMock.Setup(c => c.GetCardByNumber(It.IsAny<string>())).Returns((string c) =>
            {
                return Task.FromResult(dict[c]);
            });

            _paymentAuthServiceMock = new Mock<IPaymentAuthService>();
            _ufeServiceMock = new Mock<IUFEService>();
        }

        [Test]
        public async Task CardHas_Null_Limit_Test()
        {
            var service = new CardService(_cardRepositoryMock.Object, _paymentAuthServiceMock.Object, _ufeServiceMock.Object);

            var card = await service.CreateNewCard(null);

            Assert.IsNull(card.Limit);
        }

        [Test]
        [TestCase(15000)]
        [TestCase(1000)]
        public async Task CardHas_Value_Limit_Test(float expected)
        {
            var service = new CardService(_cardRepositoryMock.Object, _paymentAuthServiceMock.Object, _ufeServiceMock.Object);

            var card = await service.CreateNewCard(expected);

            Assert.That(card.Limit, Is.EqualTo(expected));
        }
    }
}