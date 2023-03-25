using CurrencyQuoter.Domain.Parameters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CurrencyQuoter.CrossCutting.IoC
{
    public static class DependencyResolverHttpClient
    {
        public static void AddDependencyResolverHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient(Parameters.YahooFinanceApi.Name,
                config =>
                {
                config.BaseAddress = new Uri(Parameters.YahooFinanceApi.BaseUrl);
                config.DefaultRequestVersion = HttpVersion.Version20;
                config.Timeout = TimeSpan.FromSeconds(20);
                 });
        }
    }
}
