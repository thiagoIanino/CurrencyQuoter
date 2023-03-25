using CurrencyQuoter.Domain.Entities;
using CurrencyQuoter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Domain.Services
{
    public class DomainCurrencyQuoteService : IDomainCurrencyQuoteService
    {
        public DomainCurrencyQuoteService() { }

        public List<CurrencyQuote> CalculateVariancePercentage(List<CurrencyQuote> currencyQuotes)
        {
            var firstQuote = currencyQuotes.FirstOrDefault();

            for(var i = 0; i < currencyQuotes.Count;i++)
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

            return Math.Round(percentage,2);
        }
    }
}
