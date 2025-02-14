using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

using RapidPay.DataAccess.Interfaces;
using RapidPay.Models;

namespace RapidPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        public ICardRepository _cardRepository;
        private readonly ILogger<CardController> _logger;

        public CardController(ICardRepository cardRepository, ILogger<CardController> logger)
        {
            _cardRepository = cardRepository;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetCards()
        {
            return Ok(await _cardRepository.GetAll());
        }

        /// <summary>
        /// Creates a new Card with an optional spending limit and a random 15 character number.
        /// </summary>
        /// <param name="limit">An optional float value specifying the spending limit.</param>
        /// <returns>An IActionResult indicating the outcome of the operation.</returns>
        [HttpGet("create")]
        public async Task<IActionResult> Create([FromQuery] float? limit)
        {
            var rand = new Random();

            var card = new CardDetails()
            {
                Number = GetRandomCardNumber(),
                Balance = rand.NextSingle() * (limit ?? 100000),
                Limit = limit,
                Active = false
            };

            if (!await _cardRepository.CardExists(card.Number))
            {
                await _cardRepository.InsertCard(card);
            }

            var result = await _cardRepository.GetCardByNumber(card.Number);

            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] CardDetails details)
        {
            await _cardRepository.InsertCard(details);

            var data = await _cardRepository.GetCardByNumber(details.Number);

            return Ok(data);
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
}