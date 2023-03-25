using CurrencyQuoter.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Application
{
    public class CurrencyQuoteApplication
    {
        private IYahooFinanceRepository _yahooFinanceRepository;
        public CurrencyQuoteApplication(IYahooFinanceRepository yahooFinanceRepository)
        {
            _yahooFinanceRepository = yahooFinanceRepository;
        }

        public async Task GetCurrencyQuotesValues(string currency) 
        {
            var quotes = await _yahooFinanceRepository.GetCurrencyQuotes(currency);
        }
    }
}
