using BillingMVC.Core.Contracts.ExternalServices;
using BillingMVC.Core.DTOS;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BillingMVC.ExternalServices
{
    public class ExchangeHandler : IExchangeHandler
    {
        public async Task<ExchangeResult> GetExchangeOfTheDay()
        {
            ExchangeResult result = new ExchangeResult();
            using (var client = new HttpClient())
            {
                string token = "dbcd1ba81b22497fc218af43f6658209";
                string _base = "EUR";
                string symbols = "BRL";
                var url = new Uri($"https://api.exchangeratesapi.io/v1/latest?access_key={token}&base={_base}&symbols={symbols}");
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                        WriteIndented = true
                    };

                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<ExchangeResult>(content, options);
                    var json = JsonSerializer.Serialize(result, options);
                    return result;
                }
            }

            return result;
        }
    }
}