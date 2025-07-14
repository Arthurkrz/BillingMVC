using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using BillingMVC.Web.Models.Enum;
using BillingMVC.Web.Models;
using System.Collections.Generic;
using System;

namespace BillingMVC.Tests.ObjectGenerators
{
    public class MappingData
    {
        public static IEnumerable<object[]> GetValidObjects()
        {
            yield return new object[]
            {
                new Bill()
                {
                    Id = new Guid(),
                    Name = "Televisão",
                    Currency = Currency.Euro,
                    Value = 1000,
                    Type = BillType.House,
                    ExpenseDate = DateTime.Now.AddMonths(-1).Date,
                    Source = "Casas Bahia"
                },

                new BillViewModel()
                {
                    Id = new Guid(),
                    Name = "Televisão",
                    Currency = CurrencyVM.Euro,
                    ValueString = "1000.00",
                    Type = BillTypeVM.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1).Date,
                    Source = "Casas Bahia"
                }
            };

            yield return new object[]
            {
                new BillViewModel()
                {
                    Id = new Guid(),
                    Name = "Televisão",
                    Currency = CurrencyVM.Euro,
                    ValueString = "1000.00",
                    Type = BillTypeVM.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1).Date,
                    Source = "Casas Bahia"
                },

                new Bill()
                {
                    Id = new Guid(),
                    Name = "Televisão",
                    Currency = Currency.Euro,
                    Value = 1000,
                    Type = BillType.House,
                    ExpenseDate = DateTime.Now.AddMonths(-1).Date,
                    Source = "Casas Bahia"
                }
            };

            yield return new object[]
            {
                new BillFilter()
                {
                    NameContains = "Motocicleta",
                    SourceContains = "Yamaha",
                    ValueRangeStart = 9000,
                    ValueRangeEnd = 20000,
                    DateRangeStart = DateTime.Now.AddMonths(-6).Date,
                    DateRangeEnd = DateTime.Now.Date,
                    Currency = Currency.Real,
                    Type = BillType.Transport,
                    Month = PurchaseMonth.July
                },

                new BillFilterViewModel()
                {
                    NameContains = "Motocicleta",
                    SourceContains = "Yamaha",
                    ValueStringRangeStart = "9000.00",
                    ValueStringRangeEnd = "20000.00",
                    DateRangeStart = DateTime.Now.AddMonths(-6).Date,
                    DateRangeEnd = DateTime.Now.Date,
                    Currency = CurrencyVM.Real,
                    Type = BillTypeVM.Transport,
                    Month = PurchaseMonthVM.July,
                }
            };

            yield return new object[]
            {
                new BillFilterViewModel()
                {
                    NameContains = "Motocicleta",
                    SourceContains = "Yamaha",
                    ValueStringRangeStart = "9000.00",
                    ValueStringRangeEnd = "20000.00",
                    DateRangeStart = DateTime.Now.AddMonths(-6).Date,
                    DateRangeEnd = DateTime.Now.Date,
                    Currency = CurrencyVM.Real,
                    Type = BillTypeVM.Transport,
                    Month = PurchaseMonthVM.July,
                },

                new BillFilter()
                {
                    NameContains = "Motocicleta",
                    SourceContains = "Yamaha",
                    ValueRangeStart = 9000,
                    ValueRangeEnd = 20000,
                    DateRangeStart = DateTime.Now.AddMonths(-6).Date,
                    DateRangeEnd = DateTime.Now.Date,
                    Currency = Currency.Real,
                    Type = BillType.Transport,
                    Month = PurchaseMonth.July
                }
            };
        }
    }
}
