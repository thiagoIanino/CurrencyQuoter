using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoter.Domain.Entities
{
    public class CurrencyQuote
    {
        public string Currency { get; set; }
        public DateTime QuoteDate { get; set; }
        public double Value { get; set; }
        public double PercentageVarianceToLastDate { get; set; }
        public double PercentageVarianceToFirstDate { get; set; }
    }
}
