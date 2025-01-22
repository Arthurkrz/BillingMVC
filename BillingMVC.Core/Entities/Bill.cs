﻿using BillingMVC.Core.Enum;
using System;

namespace BillingMVC.Core.Entities
{
    public class Bill : Entity
    {
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public double Value { get; set; }
        public BillType Type { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Source { get; set; }
        public bool IsPaid { get; set; }
        public bool IsRecurring { get; set; }
    }
}
