using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLib.Data.Migrations
{
    public partial class RenameScheduledToFavourite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ScheduledBook",
                newName: "FavouriteBook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "FavouriteBook",
                newName: "ScheduledBook");
        }
    }
}
