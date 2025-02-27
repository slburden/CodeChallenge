using Microsoft.AspNetCore.Mvc;

using RapidPay.Business.Interfaces;
using RapidPay.Models;

namespace RapidPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ILogger<CardController> _logger;

        public CardController(ICardService cardService, ILogger<CardController> logger)
        {
            _cardService = cardService;
            _logger = logger;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create([FromQuery] decimal? limit)
        {
            return Ok(await _cardService.CreateNewCard(limit));
        }

        [HttpPost("balance")]
        public async Task<IActionResult> GetBalance([FromBody] BalanceRequest balance)
        {
            return Ok(await _cardService.GetBalance(balance.CardNumber));
        }

        [HttpPost("isauthorized")]
        public async Task<IActionResult> Authorized([FromBody] Payment payment)
        {
            return Ok(await _cardService.IsPaymentAuthorized(payment));
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] Payment payment)
        {
            return Ok(await _cardService.MakePayment(payment));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] CardDetails details)
        {
            return Ok(await _cardService.UpdateCard(details));
        }
    }
}