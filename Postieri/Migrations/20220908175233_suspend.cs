using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postieri.Migrations
{
    public partial class suspend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSuspened",
                table: "Users",
                newName: "IsSuspended");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsSuspended",
                table: "Users",
                newName: "IsSuspened");
        }
    }
}
