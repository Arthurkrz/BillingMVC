using BillingMVC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BillingMVC.Core.Contracts.Services
{
    public interface IBillService
    {
        ServiceResponse CreateBill(Bill entity);
        IEnumerable<Bill> GetBillsWithFilter
            (Expression<Func<Bill, bool>> predicate);
        IEnumerable<Bill> List();
        ServiceResponse UpdateBill(Bill bill);
        ServiceResponse DeleteBill(Guid id);
    }
}
