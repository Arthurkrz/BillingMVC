using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BillingMVC.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Moeda = table.Column<int>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Vencimento = table.Column<DateTime>(nullable: false),
                    Origem = table.Column<string>(maxLength: 50, nullable: false),
                    Pago = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contas");
        }
    }
}
