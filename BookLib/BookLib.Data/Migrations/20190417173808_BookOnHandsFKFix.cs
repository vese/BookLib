using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLib.Data.Migrations
{
    public partial class BookOnHandsFKFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookOnHands_Availability_BookId",
                table: "BookOnHands");

            migrationBuilder.AddForeignKey(
                name: "FK_BookOnHands_Book_BookId",
                table: "BookOnHands",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookOnHands_Book_BookId",
                table: "BookOnHands");

            migrationBuilder.AddForeignKey(
                name: "FK_BookOnHands_Availability_BookId",
                table: "BookOnHands",
                column: "BookId",
                principalTable: "Availability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
