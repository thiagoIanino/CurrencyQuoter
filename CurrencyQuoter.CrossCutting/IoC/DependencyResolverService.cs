using CurrencyQuoter.Application.Interfaces;
using CurrencyQuoter.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CurrencyQuoter.Domain.Interfaces;
using CurrencyQuoter.Domain.Services;

namespace CurrencyQuoter.CrossCutting.IoC
{
    public static class DependencyResolverService
    {
        public static void AddDependencyResolverService(this IServiceCollection services)
        {
            services.AddScoped<IDomainCurrencyQuoteService, DomainCurrencyQuoteService>();
        }
    }
}
