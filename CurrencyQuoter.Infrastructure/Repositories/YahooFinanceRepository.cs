using CurrencyQuoter.Domain.Dtos;
using CurrencyQuoter.Domain.Parameters;
using CurrencyQuoter.Domain.Repositories;
using CurrencyQuoter.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Infrastructure.Repositories
{

    public class YahooFinanceRepository : BaseServiceRepository, IYahooFinanceRepository
    {
        public YahooFinanceRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<CurrencyQuoteDto> GetCurrencyQuotes(string currency)
        {
            try
            {
                var path = $"chart/{currency}BRL=X?interval=1d&range=1mo";
                return await Get<CurrencyQuoteDto>(Parameters.YahooFinanceApi.Name, path, null, null);
            }
            catch
            {
                throw new Exception(Parameters.Exception.CurrencyQuoteNotFound);
            }
        }
    }
}
