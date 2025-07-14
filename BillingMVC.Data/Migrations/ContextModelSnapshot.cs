using BillingMVC.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BillingMVC.Data.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BillingMVC.Core.Entities.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Currency")
                        .HasColumnName("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnName("Source")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Type")
                        .HasColumnName("Type")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnName("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Contas");
                });
#pragma warning restore 612, 618
        }
    }
}
