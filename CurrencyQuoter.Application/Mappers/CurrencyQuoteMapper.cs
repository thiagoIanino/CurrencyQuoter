using CurrencyQuoter.Domain.Dtos;
using CurrencyQuoter.Domain.Entities;
using CurrencyQuoter.Domain.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyQuoter.Application.Mappers
{
    public static class CurrencyQuoteMapper
    {
        public static List<CurrencyQuote> ToCurrencyQuoteList(this CurrencyQuoteDto currentQuotesDto)
        {
            var currencyQuotes = new List<CurrencyQuote>();
            var prices = currentQuotesDto.Chart.Result.FirstOrDefault().Indicators.Quote.FirstOrDefault().Open;
            var timestamps = currentQuotesDto.Chart.Result.FirstOrDefault().Timestamp;

            for(int i = 0; i < prices.Count; i++)
            {
                var quoteDate = timestamps.ElementAt(i).ToDateTime();
                var price = prices.ElementAt(i);

                currencyQuotes.Add(new CurrencyQuote {
                    Currency = "",
                    Value = Math.Round(price,4),
                    QuoteDate = quoteDate
                });
            }
            return currencyQuotes.OrderBy(c => c.QuoteDate).ToList(); ;
        }
    }
}
