using CurrencyQuoter.Application.Interfaces;
using CurrencyQuoter.Application.Mappers;
using CurrencyQuoter.Domain.Entities;
using CurrencyQuoter.Domain.Interfaces;
using CurrencyQuoter.Domain.Parameters;
using CurrencyQuoter.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Application
{
    public class CurrencyQuoteApplication : ICurrencyQuoteApplication
    {
        private IYahooFinanceRepository _yahooFinanceRepository;
        private IDomainCurrencyQuoteService _domainCurrencyQuoteService;
        public CurrencyQuoteApplication(IYahooFinanceRepository yahooFinanceRepository, IDomainCurrencyQuoteService domainCurrencyQuoteService)
        {
            _yahooFinanceRepository = yahooFinanceRepository;
            _domainCurrencyQuoteService = domainCurrencyQuoteService;
        }

        public async Task<List<CurrencyQuote>> GetCurrencyQuotesValues(string currency)
        {
            try
            {
                var upperCaseCurrency = currency.ToUpper();
                var quotes = (await _yahooFinanceRepository.GetCurrencyQuotes(upperCaseCurrency))?.ToCurrencyQuoteList(upperCaseCurrency);

                var calculatedQuotes = _domainCurrencyQuoteService.CalculateVariancePercentage(quotes);
                _ = RegisterNewQuotes(calculatedQuotes, upperCaseCurrency);

                return calculatedQuotes;
            }
            catch(Exception ex)
            {
                throw new Exception(Parameters.Exception.GerenalCurrencyQuoteError + ex.Message);
            }
        }

        private async Task RegisterNewQuotes(List<CurrencyQuote> calculatedQuotes, string currency)
        {
            var registeredQuotes = await _domainCurrencyQuoteService.GetRegisteredQuotes(calculatedQuotes, currency);
            var newQuotes = _domainCurrencyQuoteService.GetNewQuotes(registeredQuotes, calculatedQuotes);

            await _domainCurrencyQuoteService.UpdadteCurrenyQuotes(registeredQuotes, newQuotes);
        }
    }
}