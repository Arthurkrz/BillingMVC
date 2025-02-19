using BillingMVC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BillingMVC.Core.Contracts.Repositories
{
    public interface IBillRepository : IBaseRepository<Bill>
    {
        Task<IEnumerable<Bill>> GetBillsWithFilter
                          (Expression<Func<Bill, bool>> predicate);
        Task<Bill> GetById(Guid id);
    }
}
