using BillingMVC.Core.Entities;
using BillingMVC.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BillingMVC.Data.Repositories
{
    public class Context : DbContext
    {
        public DbSet<Bill> Bills { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BillConfiguration());
        }
    }
}
