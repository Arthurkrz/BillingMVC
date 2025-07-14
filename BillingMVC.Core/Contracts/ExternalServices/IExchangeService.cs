using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillingMVC.Core.Contracts.ExternalServices
{
    public interface IExchangeService
    {
        Task<Dictionary<string, double>> GetExchangeAsync();
    }
}
