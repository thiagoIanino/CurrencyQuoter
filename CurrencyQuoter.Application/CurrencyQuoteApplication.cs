﻿using CurrencyQuoter.Application.Interfaces;
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
                var quotes = (await _yahooFinanceRepository.GetCurrencyQuotes(currency)).ToCurrencyQuoteList();
                var registeredQuotes = await _domainCurrencyQuoteService.GetRegisteredQuotes(quotes,currency);

                var newQuotes = _domainCurrencyQuoteService.GetNewQuotes(registeredQuotes, quotes);

                var calculatedQuotes = _domainCurrencyQuoteService.CalculateVariancePercentage(newQuotes);

                _ = _domainCurrencyQuoteService.UpdadteCurrenyQuotes(registeredQuotes, calculatedQuotes);

                return quotes;
            }
            catch(Exception ex)
            {
                throw new Exception(Parameters.Exception.GerenalCurrencyQuoteError + ex.Message);
            }
        }
    }
}
