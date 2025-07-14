using BillingMVC.Core.DTOS;
using System.Threading.Tasks;

namespace BillingMVC.Core.Contracts.ExternalServices
{
    public interface IExchangeHandler
    {
        Task<ExchangeResultDTO> GetExchangeOfTheDay();
    }
}
