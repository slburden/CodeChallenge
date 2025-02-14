using Microsoft.AspNetCore.Http;
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

        public CardController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            return Ok(await _cardRepository.GetAll());
        }
    }
}