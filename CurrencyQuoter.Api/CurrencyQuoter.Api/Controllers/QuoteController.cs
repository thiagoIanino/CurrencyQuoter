using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CurrencyQuoter.Api.Controllers
{
    [ApiController]
    [Route("quotes")]
    public class QuoteController : ControllerBase
    {
        public QuoteController()
        {
            
        }

        [HttpGet]
        [Route("{currency}")]
        public async Task<IActionResult> GetCurrecyVariance(string currency)
        {
            if (currency is null)
                return BadRequest();

            return Ok(currency);
        }
    }
}
