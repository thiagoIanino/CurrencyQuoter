using CurrencyQuoter.Application.Interfaces;
using CurrencyQuoter.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CurrencyQuoter.Domain.Repositories;
using CurrencyQuoter.Infrastructure.Repositories;

namespace CurrencyQuoter.CrossCutting.IoC
{
    public static class DependencyResolverRepository
    {
        public static void AddDependencyResolverRepository(this IServiceCollection services)
        {
            services.AddScoped<IYahooFinanceRepository, YahooFinanceRepository>();
        }
    }
}
