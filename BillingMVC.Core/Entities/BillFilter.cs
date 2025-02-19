using BillingMVC.Core.Enum;
using System;

namespace BillingMVC.Core.Entities
{
    public class BillFilter
    {
        public string NameContains { get; set; }
        public string SourceContains { get; set; }
        public double? ValueRangeStart { get; set; }
        public double? ValueRangeEnd { get; set; }
        public DateTime? DateRangeStart { get; set; }
        public DateTime? DateRangeEnd { get; set; }
        public Currency? Currency { get; set; }
        public BillType? Type { get; set; }
        public PurchaseMonth? Month { get; set; }
    }
}
