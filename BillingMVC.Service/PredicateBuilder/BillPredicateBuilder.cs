using BillingMVC.Core.Entities;
using LinqKit;
using System;
using System.Linq.Expressions;

namespace BillingMVC.Service.PredicateBuilder
{
    public static class BillPredicateBuilder
    {
        public static Expression<Func<Bill, bool>> Build(BillFilter filter)
        {
            Expression<Func<Bill, bool>> predicate = b => true;

            if (!string.IsNullOrEmpty(filter.NameContains))
            {
                string filterName = filter.NameContains.Trim().ToLower();
                predicate = predicate.And(b => b.Name.ToLower()
                                     .Contains(filterName));
            }

            if (!string.IsNullOrEmpty(filter.SourceContains))
            {
                string filterSource = filter.SourceContains.Trim().ToLower();
                predicate = predicate.And(b => b.Source.ToLower()
                                     .Contains(filterSource));
            }

            if (filter.ValueRangeStart.HasValue)
            {
                predicate = predicate.And(b => b.Value >= 
                                      filter.ValueRangeStart.Value);
            }
            if (filter.ValueRangeEnd.HasValue)
            {
                predicate = predicate.And(b => b.Value <= 
                                      filter.ValueRangeEnd.Value);
            }

            if (filter.DateRangeStart.HasValue)
            {
                predicate = predicate.And(b => b.ExpenseDate >= 
                                      filter.DateRangeStart.Value);
            }
            if (filter.DateRangeEnd.HasValue)
            {
                predicate = predicate.And(b => b.ExpenseDate <= 
                                      filter.DateRangeEnd.Value);
            }

            if (filter.Currency.HasValue)
            {
                var chosenCurrency = filter.Currency.Value;
                predicate = predicate.And(b => b.Currency == 
                                             chosenCurrency);
            }

            if (filter.Type.HasValue)
            {
                var chosenType = filter.Type.Value;
                predicate = predicate.And(b => b.Type == chosenType);
            }

            if (filter.Month.HasValue)
            {
                DateTime filterDateStart = new DateTime(DateTime.Now.Year, 
                                                (int)filter.Month.Value, 1);
                DateTime filterDateEnd = filterDateStart.AddMonths(1)
                                                        .AddDays(-1);

                predicate = predicate.And(b => b.ExpenseDate >= filterDateStart 
                                             && b.ExpenseDate <= filterDateEnd);
            }

            return predicate;
        }
    }
}
