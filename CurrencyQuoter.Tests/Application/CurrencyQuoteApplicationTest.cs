using CurrencyQuoter.Application;
using CurrencyQuoter.Domain.Dtos;
using CurrencyQuoter.Domain.Entities;
using CurrencyQuoter.Domain.Interfaces;
using CurrencyQuoter.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyQuoter.Tests.Application
{
    public class CurrencyQuoteApplicationTest
    {
        private Mock<IYahooFinanceRepository> _yahooFinanceRepository;
        private Mock<IDomainCurrencyQuoteService> _domainCurrencyQuoteService;
        private CurrencyQuoteApplication _currencyQuoteApplication;

        /// <summary>
        /// .ctor.
        /// </summary>
        public CurrencyQuoteApplicationTest()
        {
            _yahooFinanceRepository = new Mock<IYahooFinanceRepository>();
            _domainCurrencyQuoteService = new Mock<IDomainCurrencyQuoteService>();
            _currencyQuoteApplication = new CurrencyQuoteApplication(_yahooFinanceRepository.Object, _domainCurrencyQuoteService.Object);
        }

        [Fact]
        public async Task GetCurrencyQuotesValues_ExceptionToGetQuotes_Test()
        {
            var resteredQuotes = _yahooFinanceRepository.Setup(mock => mock.GetCurrencyQuotes(It.IsAny<string>())).ThrowsAsync(new Exception());
            await Assert.ThrowsAsync<Exception>(async () => await _currencyQuoteApplication.GetCurrencyQuotesValues("USD"));
        }

        [Fact]
        public async Task GetCurrencyQuotesValues_Ok_Test()
        {

            _yahooFinanceRepository.Setup(mock => mock.GetCurrencyQuotes(It.IsAny<string>())).ReturnsAsync(CurrencyQuoteDtoMock());
            _domainCurrencyQuoteService.Setup(mock => mock.CalculateVariancePercentage(It.IsAny<List<CurrencyQuote>>())).Returns(new List<CurrencyQuote>());
            _domainCurrencyQuoteService.Setup(mock => mock.GetRegisteredQuotes(It.IsAny<List<CurrencyQuote>>(),It.IsAny<string>())).ReturnsAsync(new Dictionary<int, List<CurrencyQuote>>());
            _domainCurrencyQuoteService.Setup(mock => mock.GetNewQuotes(It.IsAny<Dictionary<int, List<CurrencyQuote>>>(),It.IsAny<List<CurrencyQuote>>())).Returns(new List<CurrencyQuote>());

            await _currencyQuoteApplication.GetCurrencyQuotesValues("USD");
            _domainCurrencyQuoteService.Verify(x => x.UpdadteCurrenyQuotes(It.IsAny<Dictionary<int, List<CurrencyQuote>>>(), It.IsAny<List<CurrencyQuote>>()),Times.Once);
        }

        private CurrencyQuoteDto CurrencyQuoteDtoMock()
        {
            return new CurrencyQuoteDto
            {
                Chart = new ChartDto
                {
                    Result = new List<ResultCurrencyQuoteDto>
                    {
                        new ResultCurrencyQuoteDto{
                        Indicators = new IndicatorsDto
                        {
                            Quote = new List<QuoteDto>
                            {
                                new QuoteDto
                                { Open = new List<double> { 43 }
                                }

                            }
                        },
                        Timestamp = new List<long>{43}
                        }
                    }
                }
            };
        }

    }
}
