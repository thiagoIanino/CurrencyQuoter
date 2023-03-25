using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoter.Domain.Parameters
{
    public static class Parameters
    {
        public static class YahooFinanceApi
        {
            public static string Name => nameof(YahooFinanceApi);
            public static string BaseUrl => "https://query2.finance.yahoo.com/v8/finance/";
        }

        public static class Exception
        {
            public static string InvalidRequestParameter => "Invalid parameter";
            public static string GerenalCurrencyQuoteError => "Couldn`t calculate the last quotes. ";
            public static string CurrencyQuoteNotFound => "Currency not found";
        }
    }
}
