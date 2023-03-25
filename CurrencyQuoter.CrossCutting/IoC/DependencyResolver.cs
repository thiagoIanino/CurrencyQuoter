using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyQuoter.CrossCutting.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            services.AddDependencyResolverHttpClient();
        }
    }
}
