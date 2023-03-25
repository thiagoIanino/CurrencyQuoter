using CurrencyQuoter.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Domain.Repositories
{
    public interface IYahooFinanceRepository
    {
        Task<CurrencyQuoteDto> GetCurrencyQuotes(string currency);
    }
}
