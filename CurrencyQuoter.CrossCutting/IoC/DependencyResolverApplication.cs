using CurrencyQuoter.Application;
using CurrencyQuoter.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoter.CrossCutting.IoC
{
    public static class DependencyResolverApplication
    {
        public static void AddDependencyResolverApplication(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyQuoteApplication, CurrencyQuoteApplication>();
        }
    }
}
