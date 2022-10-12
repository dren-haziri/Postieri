using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postieri.Migrations
{
    public partial class LiveAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Businesses",
            //    columns: table => new
            //    {
            //        BusinessID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PhoneNumber = table.Column<int>(type: "int", nullable: false),
            //        BusinessToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Businesses", x => x.BusinessID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ClientOrders",
            //    columns: table => new
            //    {
            //        OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Date = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        OrderedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Price = table.Column<double>(type: "float", nullable: false),
            //        CompanyToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Phone = table.Column<int>(type: "int", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ClientOrders", x => x.OrderId);
            //    });

            migrationBuilder.CreateTable(
                name: "LiveAgents",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConnectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveAgents", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_LiveAgents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Businesses");

            //migrationBuilder.DropTable(
            //    name: "ClientOrders");

            migrationBuilder.DropTable(
                name: "LiveAgents");
        }
    }
}
