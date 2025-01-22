using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BillingMVC.Core.Contracts.Repositories
{
    public interface IBillRepository
    {
        void Add(Bill bill);
        void Update(Bill bill);
        void Delete(Bill bill);
        IEnumerable<Bill> GetBillsWithFilter(Expression<Func<Bill, bool>> predicate);
        bool FindIdentical(string name, Currency currency, 
                           double value, BillType type, 
                           DateTime expDate, string source, 
                           bool isPaid);
    }
}
