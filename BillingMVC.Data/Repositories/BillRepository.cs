using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BillingMVC.Data.Repositories
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        private readonly Context _context;
        public BillRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bill>> GetBillsWithFilter(Expression<Func<Bill, bool>> predicate)
        {
            return await _context.Set<Bill>().Where(predicate).ToListAsync();
        }

        public async Task<Bill> GetById(Guid id)
        {
            var queryableBills = await this.GetAll();
            return await queryableBills.FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
