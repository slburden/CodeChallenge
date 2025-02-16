using System.Threading.Tasks;

using Moq;

using NUnit.Framework;

using RapidPay.Business.Interfaces;
using RapidPay.Business.Services;
using RapidPay.DataAccess;
using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace Tests
{
    public class PaymentAuthService_AuthorizeCard_Test
    {
        private ICardRepository _cardRepository;
        private IAuthAuditRepository _authAuditService;

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
                        Active = false,
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


            var cardRepoMock = new Mock<ICardRepository>();
            cardRepoMock.Setup(c => c.GetCardByNumber(It.IsAny<string>())).Returns((string c) =>
            {
                return Task.FromResult(dict[c]);
            });

            var authMock = new Mock<IAuthAuditRepository>();
            authMock.Setup(c => c.InsertAudit(It.IsAny<AuthAuditRecord>()));

            _cardRepository = cardRepoMock.Object;
            _authAuditService = authMock.Object;
        }

        [Test]
        [TestCase("599180382130527", 900, false)]
        [TestCase("123456789012345", 500, false)]
        [TestCase("606481876887782", 7000, true)]
        public async Task Authorize_Payment_Test(string cardnum, decimal amount, bool exppected)
        {
            var service = new PaymentAuthService(_authAuditService, _cardRepository);

            var results = await service.AuthorizeCard(cardnum, amount);

            Assert.That(results.Authorized, Is.EqualTo(exppected));
        }
    }
}