using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoter.Domain.Dtos
{
    public class ResultCurrencyQuoteDto
    {
        public IndicatorsDto Indicators { get; set; }
        public List<long> Timestamp { get; set; }
    }
}
