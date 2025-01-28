using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BillingMVC.Core.Contracts.Repositories
{
    public interface IBillRepository : IBaseRepository<Bill>
    {
        void Add(Bill bill);
        void Update(Bill bill);
        void Delete(Bill bill);
        IEnumerable<Bill> GetBillsWithFilter
                          (Expression<Func<Bill, bool>> predicate);
        IEnumerable<Bill> GetAll();
        bool FindIdentical(string name, Currency currency, 
                           double value, BillType type, 
                           DateTime expDate, string source, 
                           CustomBoolean isPaid, 
                           CustomBoolean isRecurring);
    }
}
