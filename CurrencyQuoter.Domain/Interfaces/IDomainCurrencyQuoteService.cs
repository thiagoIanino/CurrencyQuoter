using CurrencyQuoter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Domain.Interfaces
{
    public interface IDomainCurrencyQuoteService
    {
        List<CurrencyQuote> CalculateVariancePercentage(List<CurrencyQuote> currencyQuotes);
        Task UpdadteCurrenyQuotes(Dictionary<string, List<CurrencyQuote>> registeredQuotes, List<CurrencyQuote> newQuotes);
        Task<Dictionary<string, List<CurrencyQuote>>> GetRegisteredQuotes(List<CurrencyQuote> currencyQuotes, string currency);
        List<CurrencyQuote> GetNewQuotes(Dictionary<string, List<CurrencyQuote>> registeredQuotes, List<CurrencyQuote> quotes);
    }
}
