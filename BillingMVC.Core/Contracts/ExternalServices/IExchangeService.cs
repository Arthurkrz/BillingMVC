using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillingMVC.Core.Contracts.ExternalServices
{
    public interface IExchangeService
    {
        Task<Dictionary<string, double>> GetExchangeAsync();
    }
}
