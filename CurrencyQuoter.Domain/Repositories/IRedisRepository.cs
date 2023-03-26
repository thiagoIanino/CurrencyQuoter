using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Domain.Repositories
{
    public interface IRedisRepository
    {
        Task<T> GetValue<T>(string key);
        Task Add<T>(string key, T value);
    }
}
