using BillingMVC.Core.Contracts.ExternalServices;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillingMVC.Service
{
    public class ExchangeService : IExchangeService
    {
        private readonly IExchangeHandler _exchangeHandler;
        private readonly IMemoryCacheService _memoryCacheService;

        public ExchangeService(IExchangeHandler exchangeHandler, 
                               IMemoryCacheService memoryCacheService)
        {
            _exchangeHandler = exchangeHandler;
            _memoryCacheService = memoryCacheService;
        }

        public async Task<Dictionary<string, double>> GetExchangeAsync()
        {
            var result = await _memoryCacheService.GetOrCreateAsync("exchange", async () =>
            {
                return await _exchangeHandler.GetExchangeOfTheDay();
            });

            return result.Rates;
        }
    }
}
