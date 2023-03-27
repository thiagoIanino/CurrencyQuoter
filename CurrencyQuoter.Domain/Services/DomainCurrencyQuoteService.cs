using CurrencyQuoter.Domain.Entities;
using CurrencyQuoter.Domain.Interfaces;
using CurrencyQuoter.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Domain.Services
{
    public class DomainCurrencyQuoteService : IDomainCurrencyQuoteService
    {
        private readonly IRedisRepository _redisRepository;
        public DomainCurrencyQuoteService(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        public List<CurrencyQuote> CalculateVariancePercentage(List<CurrencyQuote> currencyQuotes)
        {
            var firstQuote = currencyQuotes.FirstOrDefault();

            for (var i = 0; i < currencyQuotes.Count; i++)
            {
                if (i == 0)
                {
                    currencyQuotes[i].PercentageVarianceToLastDate = 0;
                    currencyQuotes[i].PercentageVarianceToFirstDate = 0;
                }
                else
                {
                    currencyQuotes[i].PercentageVarianceToLastDate = CalculatePercentageVariance(currencyQuotes[i].Value, currencyQuotes[i - 1].Value);
                    currencyQuotes[i].PercentageVarianceToFirstDate = CalculatePercentageVariance(currencyQuotes[i].Value, firstQuote.Value);
                }
            }
            return currencyQuotes;
        }

        private double CalculatePercentageVariance(double quoteValue, double comparedQuoteValue)
        {
            var diference = quoteValue - comparedQuoteValue;
            var percentage = diference * 100 / comparedQuoteValue;

            return Math.Round(percentage, 2);
        }

        public async Task<Dictionary<int, List<CurrencyQuote>>> GetRegisteredQuotes(List<CurrencyQuote> currencyQuotes, string currency)
        {
            var registeredQuotes = new Dictionary<int, List<CurrencyQuote>>();
            var groupedDates = new Dictionary<int, int>();
            currencyQuotes.ForEach(x => groupedDates.TryAdd(x.QuoteDate.Month, x.QuoteDate.Year));

            foreach (var groupedDate in groupedDates)
            {
                var key = $"{currency}-{groupedDate.Key}-{groupedDate.Value}";
                var quotes = (await _redisRepository.ObterObjetoAssincrono<IEnumerable<CurrencyQuote>>(key))?.ToList() ?? new List<CurrencyQuote>();
                registeredQuotes.TryAdd(groupedDate.Key, quotes);
            }

            return registeredQuotes;
        }

        public async Task UpdadteCurrenyQuotes(Dictionary<int, List<CurrencyQuote>> registeredQuotes, List<CurrencyQuote> newQuotes)
        {
            foreach (var registeredQuote in registeredQuotes)
            {
                var month = registeredQuote.Key;

                foreach (var newQuote in newQuotes)
                {
                    if (newQuote.QuoteDate.Month == month)
                        registeredQuote.Value.Add(newQuote);
                }
                var currency = registeredQuote.Value.FirstOrDefault().Currency;
                var quoteYear = registeredQuote.Value.FirstOrDefault().QuoteDate.Year;
                var key = $"{currency}-{registeredQuote.Key}-{quoteYear}";
                await _redisRepository.SalvarObjetoAssincrono<IEnumerable<CurrencyQuote>>( registeredQuote.Value, key, null);
            }
        }

        public List<CurrencyQuote> GetNewQuotes(Dictionary<int, List<CurrencyQuote>> registeredQuotes, List<CurrencyQuote> quotes)
        {
            var newQuotes = new List<CurrencyQuote>();
            foreach (var registeredQuote in registeredQuotes)
            {
                var month = registeredQuote.Key;
                foreach (var quote in quotes)
                {
                    if (quote.QuoteDate.Month == month && !registeredQuote.Value.Any(x => x.QuoteDate.Day == quote.QuoteDate.Day))
                        newQuotes.Add(quote);
                }
            }
            return newQuotes;
        }
    }
}
