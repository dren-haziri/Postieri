using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postieri.Migrations
{
    public partial class Usersuspense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)


        {

        

            migrationBuilder.RenameColumn(
                name: "IsSuspened",
                table: "Users",
                newName: "IsSuspended");

            migrationBuilder.AddColumn<string>(
                    name: "PasswordResetToken",
                    table: "Users",
                    type: "nvarchar(max)",
                    nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);
 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
      
            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Users");
            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "Users");
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShelfId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "ShelfId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ShelfId",
                table: "Product",
                column: "ShelfId");
        }
    }
}
