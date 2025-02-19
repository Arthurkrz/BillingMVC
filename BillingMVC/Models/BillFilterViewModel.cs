using BillingMVC.Web.Models.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BillingMVC.Web.Models
{
    public class BillFilterViewModel
    {
        public string NameContains { get; set; }
        public string SourceContains { get; set; }
        public double? ValueRangeStart
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ValueStringRangeStart))
                    return null;

                string valorStringStart = ValueStringRangeStart
                                          .Replace(',', '.');
                double.TryParse(valorStringStart,
                    System.Globalization.NumberStyles.Currency,
                    CultureInfo.InvariantCulture,
                    out var valorStart);
                return valorStart;
            }
        }
        public string ValueStringRangeStart { get; set; }
        public string ValueStringRangeEnd { get; set; }
        public double? ValueRangeEnd
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ValueStringRangeEnd))
                    return null;

                string valorStringEnd = ValueStringRangeEnd
                                        .Replace(',', '.');
                double.TryParse(valorStringEnd,
                    System.Globalization.NumberStyles.Currency,
                    CultureInfo.InvariantCulture,
                    out var valorEnd);
                return valorEnd;
            }
        }
        public DateTime? DateRangeStart { get; set; }
        public DateTime? DateRangeEnd { get; set; }
        public CurrencyVM? Currency { get; set; }
        public BillTypeVM? Type { get; set; }
        public PurchaseMonthVM? Month { get; set; }
        public List<BillViewModel> Bills { get; set; } = 
           new List<BillViewModel>();
    }
}
