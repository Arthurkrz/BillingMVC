using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BillingMVC.Data.Repositories
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        private readonly Context _context;
        public BillRepository(Context context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Bill> GetBillsWithFilter(Expression<Func<Bill, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Bill GetById(Guid id)
        {
            var bill = this.GetAll().FirstOrDefault(b => b.Id == id);
            return bill;
        }
    }
}
