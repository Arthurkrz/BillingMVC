using BillingMVC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BillingMVC.Core.Contracts.Services
{
    public interface IBillService
    {
        void CreateBill(Bill entity);
        IEnumerable<Bill> GetBillsWithFilter
            (Expression<Func<Bill, bool>> predicate);
        void UpdateBill(Bill bill);
        void DeleteBill(Bill bill);
    }
}
