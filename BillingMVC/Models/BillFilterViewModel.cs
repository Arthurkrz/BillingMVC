using BillingMVC.Web.Models.Enum;
using System;
using System.Collections.Generic;

namespace BillingMVC.Web.Models
{
    public class BillFilterViewModel
    {
        public string? NameContains { get; set; }
        public double? ValueRangeStart { get; set; }
        public double? ValueRangeEnd { get; set; }
        public DateTime? DateRangeStart { get; set; }
        public DateTime? DateRangeEnd { get; set; }
        public bool? IsPaid { get; set; }
        public CurrencyVM? Currency { get; set; }
        public BillTypeVM? Type { get; set; }
        public bool? IsRecurring { get; set; }
        public List<BillViewModel> Bills { get; set; }
            = new List<BillViewModel>();
    }
}
