using CurrencyQuoter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Application.Interfaces
{
    public interface ICurrencyQuoteApplication
    {
        Task<List<CurrencyQuote>> GetCurrencyQuotesValues(string currency);
    }
}
