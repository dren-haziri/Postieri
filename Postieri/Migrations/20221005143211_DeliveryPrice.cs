using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postieri.Migrations
{
    public partial class DeliveryPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryPrices",
                columns: table => new
                {
                    DeliveryPriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCodeTo = table.Column<int>(type: "int", nullable: false),
                    DimensionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPrices", x => x.DeliveryPriceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryPrices");
        }
    }
}
