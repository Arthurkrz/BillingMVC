using BillingMVC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillingMVC.Core.Contracts.Services
{
    public interface IBillService
    {
        Task<ServiceResponse> CreateBill(Bill entity);

        Task<ServiceResponseGeneric<IEnumerable<Bill>>> GetBillsWithFilter(BillFilter filter);

        Task<IEnumerable<Bill>> List();

        Task<ServiceResponse> UpdateBill(Bill bill);

        Task<ServiceResponse> DeleteBill(Guid id);
    }
}
