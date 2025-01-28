using BillingMVC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace BillingMVC.Data.Repositories
{
    internal class Context : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        private readonly string _connectionString;

        public Context(DbContextOptions<Context> options) : base(options) { }

        public Context(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseMySQL(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .ToTable("Contas")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Bill>()
                .Property(x => x.Name)
                .HasColumnName("Nome")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
