using BillingMVC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillingMVC.Data.Configurations
{
    internal class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();

            builder.Property(x => x.Name).HasColumnName("Name")
                   .HasMaxLength(50).IsRequired();

            builder.Property(x => x.Currency).HasColumnName("Currency")
                   .IsRequired();

            builder.Property(x => x.Value).HasColumnName("Valor")
                   .IsRequired();

            builder.Property(x => x.Type).HasColumnName("Type")
                   .IsRequired();

            builder.Property(x => x.ExpenseDate)
                   .HasColumnName("ExpenseDate").IsRequired();

            builder.Property(x => x.Source).HasColumnName("Source")
                   .HasMaxLength(50).IsRequired();
        }
    }
}
