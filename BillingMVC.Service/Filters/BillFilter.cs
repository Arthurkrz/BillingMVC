using BillingMVC.Core.Enum;
using System;
using System.Collections.Generic;

namespace BillingMVC.Service.Filters
{
    public class BillFilter
    {
        public string? NameContains { get; set; }
        public double? ValueRangeStart { get; set; }
        public double? ValueRangeEnd { get; set; }
        public DateTime? DateRangeStart { get; set; }
        public DateTime? DateRangeEnd { get; set; }
        public bool? IsPaid { get; set; }
        public Currency? Currency { get; set; }
        public BillType? Type { get; set; }
        public bool? IsRecurring { get; set; }
    }
}
