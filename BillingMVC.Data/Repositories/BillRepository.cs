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
        //private readonly AppDbContext _context;
        //public BillRepository(AppDbContext context)
        //{
        //    _context = context;
        //}

        private readonly List<Bill> _bills = new List<Bill>();
        public void Add(Bill bill)
        {
            
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
