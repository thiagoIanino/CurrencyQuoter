using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoter.Domain.Dtos
{
    public class CurrencyQuoteDto
    {
        public ChartDto Chart { get; set; }

        public object Where()
        {
            throw new NotImplementedException();
        }
    }
}
