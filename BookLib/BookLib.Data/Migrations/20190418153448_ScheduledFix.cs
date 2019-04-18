using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLib.Data.Migrations
{
    public partial class ScheduledFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SheduledBook",
                newName: "ScheduledBook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ScheduledBook",
                newName: "SheduledBook");
        }
    }
}
