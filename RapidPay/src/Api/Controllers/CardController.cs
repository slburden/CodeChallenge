using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

using RapidPay.Business.Interfaces;
using RapidPay.Models;

namespace RapidPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        public ICardService _cardService;
        private readonly ILogger<CardController> _logger;

        public CardController(ICardService cardService, ILogger<CardController> logger)
        {
            _cardService = cardService;
            _logger = logger;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create([FromQuery] float? limit)
        {
            return Ok(await _cardService.CreateNewCard(limit));
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance([FromQuery] string cardnumber)
        {
            return Ok(await _cardService.GetBalance(cardnumber));
        }

        [HttpGet("isauthorized")]
        public async Task<IActionResult> Authorized([FromQuery] string cardnumber, float amount)
        {
            return Ok(await _cardService.IsPaymentAuthorized(cardnumber, amount));
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