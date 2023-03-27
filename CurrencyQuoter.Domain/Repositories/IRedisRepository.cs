using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Domain.Repositories
{
    public interface IRedisRepository
    {
        Task SalvarObjetoAssincrono<T>(T valor, string chave, TimeSpan? tempoExpiracao);
        Task<T> ObterObjetoAssincrono<T>(string chave);
    }
}
