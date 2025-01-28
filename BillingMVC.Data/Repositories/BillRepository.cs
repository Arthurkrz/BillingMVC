using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BillingMVC.Data.Repositories
{
    public class BillRepository : IBillRepository
    {
        public static List<Bill> _bills = new List<Bill>()
        {
            new Bill
            {
                 Name = "Luz",
                 Type = BillType.Services,
                 Currency = Currency.Euro,
                 Value = 36,
                 Source = "Copel",
                 ExpirationDate = new DateTime(2025, 02, 10),
                 IsPaid = true,
                 IsRecurring = true
            },

            new Bill
            {
                Name = "Água",
                Type = BillType.Services,
                Currency = Currency.Euro,
                Value = 6.7,
                Source = "Sanepar",
                ExpirationDate = new DateTime(2025, 01, 10),
                IsPaid = false,
                IsRecurring = true
            }
        };

        public void Add(Bill bill)
        {
            _bills.Add(bill);
        }

        public void Update(Bill bill)
        {
           
        }

        public bool FindIdentical(string name, Currency currency, double value, 
                                  BillType type, DateTime expDate, 
                                  string source, bool isPaid)
        {
            if (_bills.Any(x => x.Name == name && 
                          x.Currency == currency && 
                          x.Value == value && 
                          x.Type == type && 
                          x.ExpirationDate == expDate && 
                          x.Source == source && 
                          x.IsPaid == isPaid))
            {
                return true;
            }
            return false;
        }

        public void Delete(Bill bill)
        {
            
        }

        public IEnumerable<Bill> GetAll()
        {
            return _bills;
        }

        public IEnumerable<Bill> GetBillsWithFilter
                                 (Expression<Func<Bill, bool>> predicate)
        {
            return _bills.Where(predicate.Compile());
            //return _context.Bills
            //               .AsExpandable()
            //               .Where(predicate)
            //               .ToList();
        }
    }
}
