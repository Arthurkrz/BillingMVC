﻿using BillingMVC.Core.Entities;
using BillingMVC.Service.Filters;
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
                predicate = predicate.And(b => b.Name.Contains(filter.NameContains));
            }

            if (filter.ValueRangeStart.HasValue)
            {
                predicate = predicate.And(b => b.Value >= filter.ValueRangeStart.Value);
            }
            if (filter.ValueRangeEnd.HasValue)
            {
                predicate = predicate.And(b => b.Value <= filter.ValueRangeEnd.Value);
            }

            if (filter.DateRangeStart.HasValue)
            {
                predicate = predicate.And(b => b.ExpirationDate >= filter.DateRangeStart.Value);
            }
            if (filter.DateRangeEnd.HasValue)
            {
                predicate = predicate.And(b => b.ExpirationDate <= filter.DateRangeEnd.Value);
            }

            if (filter.IsPaid.HasValue)
            {
                bool paid = filter.IsPaid.Value;
                predicate = predicate.And(b => b.IsPaid == paid);
            }

            if (filter.Currency.HasValue)
            {
                var chosenType = filter.Currency.Value;
                predicate = predicate.And(b => b.Currency == chosenType);
            }

            if (filter.Type.HasValue)
            {
                var chosenType = filter.Type.Value;
                predicate = predicate.And(b => b.Type == chosenType);
            }
            
            if (filter.IsRecurring.HasValue)
            {
                var chosenType = filter.IsRecurring.Value;
                predicate = predicate.And(b => b.IsRecurring == chosenType);
            }

            return predicate;
        }
    }
}
