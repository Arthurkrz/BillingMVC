using BillingMVC.Core.Entities;
using BillingMVC.Web.Models;
using BillingMVC.Core.Enum;
using System;

namespace BillingMVC.Web.Factories
{
    public class BillEntityFactory
    {
        public Bill CreateBillEntityFactory(BillViewModel model)
        {
            return new Bill()
            {
                Id = model.Id,
                Name = model.Name,
                Currency = Enum.Parse<Currency>(model.Currency.ToString()),
                ExpirationDate = model.ExpirationDate,
                IsPaid = model.IsPaid,
                Source = model.Source,
                Type = Enum.Parse<BillingMVC.Core.Enum.BillType>(model.Type.ToString()),
                Value = model.Value
            };
        }
    }
}
