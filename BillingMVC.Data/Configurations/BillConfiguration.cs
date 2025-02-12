using BillingMVC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillingMVC.Data.Configurations
{
    internal class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Contas").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();

            builder.Property(x => x.Name).HasColumnName("Nome")
                   .HasMaxLength(50).IsRequired();

            builder.Property(x => x.Currency).HasColumnName("Moeda")
                   .IsRequired();

            builder.Property(x => x.Value).HasColumnName("Valor")
                   .IsRequired();

            builder.Property(x => x.Type).HasColumnName("Tipo")
                   .IsRequired();

            builder.Property(x => x.PurchaseDate)
                   .HasColumnName("DataDespesa").IsRequired();

            builder.Property(x => x.Source).HasColumnName("Origem")
                   .HasMaxLength(50).IsRequired();
        }
    }
}
