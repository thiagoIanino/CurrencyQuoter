using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoter.Domain.Dtos
{
    public class ChartDto
    {
        public List<ResultCurrencyQuoteDto> Result { get; set; }
        public ErrorCurrerncyQuoteDto Error { get; set; }
    }
}
