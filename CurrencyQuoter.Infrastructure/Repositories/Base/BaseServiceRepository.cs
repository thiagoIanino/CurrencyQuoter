using CurrencyQuoter.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyQuoter.Infrastructure.Repositories.Base
{
    public class BaseServiceRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseServiceRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<T> Get<T>(string clientName, string requestUrl, Dictionary<string, string> headers, int? timeOutInSeconds)
        {
            var client = _httpClientFactory.CreateClient(clientName);
            if (timeOutInSeconds.HasValue)
                client.Timeout = TimeSpan.FromSeconds(timeOutInSeconds.Value);

            if (!Uri.TryCreate(client.BaseAddress, requestUrl, out Uri verifiedUri))
            {
                throw new InvalidDataException(Parameters.Exception.InvalidRequestParameter);
            }

            if(headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            using var response = await client.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);

            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            throw new WebException(await response.Content.ReadAsStringAsync());
        }
    }
}
