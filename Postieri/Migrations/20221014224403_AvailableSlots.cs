using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postieri.Migrations
{
    public partial class AvailableSlots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "MaxProducts",
                table: "Shelves",
                newName: "AvailableSlots");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Couriers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "AvailableSlots",
                table: "Shelves",
                newName: "MaxProducts");

            
        }
    }
}
