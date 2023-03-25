using CurrencyQuoter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoter.Domain.Interfaces
{
    public interface IDomainCurrencyQuoteService
    {
        List<CurrencyQuote> CalculateVariancePercentage(List<CurrencyQuote> currencyQuotes);
    }
}
