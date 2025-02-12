using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BillingMVC.Core.Contracts.Repositories
{
    public interface IBillRepository : IBaseRepository<Bill>
    {
        IEnumerable<Bill> GetBillsWithFilter
                          (Expression<Func<Bill, bool>> predicate);
        Bill GetById(Guid id);
    }
}
