using CurrencyQuoter.Domain.Entities;
using CurrencyQuoter.Domain.Repositories;
using CurrencyQuoter.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyQuoter.Tests.Domain
{
    public class DomainCurrentQuoteService
    {
        private readonly Mock<IRedisRepository> _redisRepository;
        private readonly DomainCurrencyQuoteService _domainCurrencyQuoteService;
        public DomainCurrentQuoteService()
        {
            _redisRepository = new Mock<IRedisRepository>();
            _domainCurrencyQuoteService = new DomainCurrencyQuoteService(_redisRepository.Object);
        }

        /// <summary>
        /// lista reservas_ valido_ test.
        /// </summary>
        /// <returns>The result.</returns>
        [Fact]
        public void CalculateVariancePercentage_EmptyList_Test()
        {
            var currencyQuotes = new List<CurrencyQuote>();

            var quotes = _domainCurrencyQuoteService.CalculateVariancePercentage(currencyQuotes);
            Assert.NotNull(quotes);
            Assert.Empty(quotes);
        }
        [Fact]
        public void CalculateVariancePercentage_QuoteListOk_Test()
        {
            var currencyQuotes = CurrencyQuoteMock();
            var expectedPercentagesToLFisrtDate = new List<double> { 0,4,8,-6 };
            var expectedPercentagesToLastDate = new List<double> { 0,4,3.85,-12.96 };

            var quotes = _domainCurrencyQuoteService.CalculateVariancePercentage(currencyQuotes);
            for(int i = 0; i < quotes.Count; i++)
            {
                Assert.Equal(quotes[i].PercentageVarianceToLastDate, expectedPercentagesToLastDate[i]);
                Assert.Equal(quotes[i].PercentageVarianceToFirstDate, expectedPercentagesToLFisrtDate[i]);
            }
        }

        [Fact]
        public async Task GetRegisteredQuotes_NoQuotesInDatabase_Test()
        {
            var currencyQuotes = CurrencyQuoteMock();
            List<CurrencyQuote> emptyList = null;

            var resteredQuotes = _redisRepository.Setup(mock => mock.ObterObjetoAssincrono<IEnumerable<CurrencyQuote>>(It.IsAny<string>())).ReturnsAsync(emptyList);
            var quotes = await _domainCurrencyQuoteService.GetRegisteredQuotes(currencyQuotes, "USD");

            Assert.Equal(10, quotes.FirstOrDefault().Key);
            Assert.Empty(quotes.FirstOrDefault().Value);
        }

        [Fact]
        public async Task GetRegisteredQuotes_HaveQuotesInDatabase_Test()
        {
            var currencyQuotes = CurrencyQuoteMock();
            var registereQuotes = new List<CurrencyQuote>
            {
            new CurrencyQuote { Currency = "USD", QuoteDate = new DateTime(2023,10,10), Value = 5.0 },
            new CurrencyQuote { Currency = "USD", QuoteDate = new DateTime(2023,10,11), Value = 5.2 },
             };

            var resteredQuotes = _redisRepository.Setup(mock => mock.ObterObjetoAssincrono<IEnumerable<CurrencyQuote>>(It.IsAny<string>())).ReturnsAsync(registereQuotes);
            var quotes = await _domainCurrencyQuoteService.GetRegisteredQuotes(currencyQuotes, "USD");

            Assert.Equal(10, quotes.FirstOrDefault().Key);
            Assert.Equal(2, quotes.FirstOrDefault().Value.Count);
            Assert.Equal(5, quotes.FirstOrDefault().Value.FirstOrDefault().Value);
        }

        [Fact]
        public void GetNewQuotes_NoNewQuotes_Test()
        {
            var currencyQuotes = DictionaryQuoteMock();
            var quotes =  _domainCurrencyQuoteService.GetNewQuotes(currencyQuotes, CurrencyQuoteMock());

            Assert.Empty(quotes);
        }

        public List<CurrencyQuote> CurrencyQuoteMock()
        {
            var currencyQuotes = new List<CurrencyQuote>
            {
            new CurrencyQuote { Currency = "USD", QuoteDate = new DateTime(2023,10,10), Value = 5.0 },
            new CurrencyQuote { Currency = "USD", QuoteDate = new DateTime(2023,10,11), Value = 5.2 },
            new CurrencyQuote { Currency = "USD", QuoteDate = new DateTime(2023,10,12), Value = 5.4 },
            new CurrencyQuote { Currency = "USD", QuoteDate = new DateTime(2023,10,13), Value = 4.7 }
             };
            return currencyQuotes;
        }

        public Dictionary<int, List<CurrencyQuote>> DictionaryQuoteMock()
        {
            var currencyQuotes = new Dictionary<int, List<CurrencyQuote>> ();
            currencyQuotes.Add(10,CurrencyQuoteMock());
            return currencyQuotes;
        }
    }
}
