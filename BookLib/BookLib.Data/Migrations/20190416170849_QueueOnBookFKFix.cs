using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLib.Data.Migrations
{
    public partial class QueueOnBookFKFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueOnBook_Availability_BookId",
                table: "QueueOnBook");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueOnBook_Book_BookId",
                table: "QueueOnBook",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueOnBook_Book_BookId",
                table: "QueueOnBook");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueOnBook_Availability_BookId",
                table: "QueueOnBook",
                column: "BookId",
                principalTable: "Availability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
