using CurrencyQuoter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CurrencyQuoter.Api.Controllers
{
    [ApiController]
    [Route("quotes")]
    public class QuoteController : ControllerBase
    {
        private readonly ICurrencyQuoteApplication _currencyQuoteApplication;
        public QuoteController(ICurrencyQuoteApplication currencyQuoteApplication)
        {
            _currencyQuoteApplication = currencyQuoteApplication;
        }

        [HttpGet]
        [Route("{currency}")]
        public async Task<IActionResult> GetCurrecyVariance(string currency)
        {
            if (currency is null)
                return BadRequest();

            var currencyQuote = await _currencyQuoteApplication.GetCurrencyQuotesValues(currency);

            return Ok(currencyQuote);
        }
    }
}
