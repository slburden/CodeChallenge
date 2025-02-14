using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RapidPay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        [HttpGet]
        public async Task<object> GetObjectAsync()
        {

            return new object();
        }
    }
}