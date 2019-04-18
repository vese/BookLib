using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLib.Data.Migrations
{
    public partial class UserCounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetUserRoles]", true);
            migrationBuilder.Sql("DELETE FROM [AspNetUsers]", true);

            migrationBuilder.DropForeignKey(
                name: "FK_Availability_Book_IdBook",
                table: "Availability");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_IdAuthor",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category_IdCategory",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_IdGenre",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publisher_IdPublisher",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Series_IdSeries",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_BookOnHands_Availability_IdBook",
                table: "BookOnHands");

            migrationBuilder.DropForeignKey(
                name: "FK_BookOnHands_AspNetUsers_IdUser",
                table: "BookOnHands");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Book_IdBook",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_IdUser",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Category_IdCategory",
                table: "Genre");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueOnBook_Availability_IdBook",
                table: "QueueOnBook");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueOnBook_AspNetUsers_IdUser",
                table: "QueueOnBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadBook_Book_IdBook",
                table: "ReadBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadBook_AspNetUsers_IdUser",
                table: "ReadBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledBook_Book_IdBook",
                table: "ScheduledBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledBook_AspNetUsers_IdUser",
                table: "ScheduledBook");

            migrationBuilder.DropIndex(
                name: "IX_Book_IdGenre",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "ScheduledBook",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdBook",
                table: "ScheduledBook",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledBook_IdUser",
                table: "ScheduledBook",
                newName: "IX_ScheduledBook_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledBook_IdBook",
                table: "ScheduledBook",
                newName: "IX_ScheduledBook_BookId");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "ReadBook",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdBook",
                table: "ReadBook",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadBook_IdUser",
                table: "ReadBook",
                newName: "IX_ReadBook_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadBook_IdBook",
                table: "ReadBook",
                newName: "IX_ReadBook_BookId");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "QueueOnBook",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdBook",
                table: "QueueOnBook",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_QueueOnBook_IdUser",
                table: "QueueOnBook",
                newName: "IX_QueueOnBook_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_QueueOnBook_IdBook",
                table: "QueueOnBook",
                newName: "IX_QueueOnBook_BookId");

            migrationBuilder.RenameColumn(
                name: "IdCategory",
                table: "Genre",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Genre_IdCategory",
                table: "Genre",
                newName: "IX_Genre_CategoryId");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Comment",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdBook",
                table: "Comment",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_IdUser",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_IdBook",
                table: "Comment",
                newName: "IX_Comment_BookId");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "BookOnHands",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IdBook",
                table: "BookOnHands",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookOnHands_IdUser",
                table: "BookOnHands",
                newName: "IX_BookOnHands_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookOnHands_IdBook",
                table: "BookOnHands",
                newName: "IX_BookOnHands_BookId");

            migrationBuilder.RenameColumn(
                name: "IdSeries",
                table: "Book",
                newName: "SeriesId");

            migrationBuilder.RenameColumn(
                name: "IdPublisher",
                table: "Book",
                newName: "PublisherId");

            migrationBuilder.RenameColumn(
                name: "IdCategory",
                table: "Book",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "IdGenre",
                table: "Book",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "IdAuthor",
                table: "Book",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_IdSeries",
                table: "Book",
                newName: "IX_Book_SeriesId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_IdPublisher",
                table: "Book",
                newName: "IX_Book_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_IdCategory",
                table: "Book",
                newName: "IX_Book_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_IdAuthor",
                table: "Book",
                newName: "IX_Book_AuthorId");

            migrationBuilder.RenameColumn(
                name: "IdBook",
                table: "Availability",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "ExpiredCount",
                table: "Availability",
                newName: "NotReturnedCount");

            migrationBuilder.RenameIndex(
                name: "IX_Availability_IdBook",
                table: "Availability",
                newName: "IX_Availability_BookId");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Book",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<int>(
                name: "Expired",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotReturned",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OnHands",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Returned",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_Book_BookId",
                table: "Availability",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_AuthorId",
                table: "Book",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category_CategoryId",
                table: "Book",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId",
                table: "Book",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                table: "Book",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Series_SeriesId",
                table: "Book",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookOnHands_Availability_BookId",
                table: "BookOnHands",
                column: "BookId",
                principalTable: "Availability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookOnHands_AspNetUsers_UserId",
                table: "BookOnHands",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Book_BookId",
                table: "Comment",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Category_CategoryId",
                table: "Genre",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueOnBook_Availability_BookId",
                table: "QueueOnBook",
                column: "BookId",
                principalTable: "Availability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueOnBook_AspNetUsers_UserId",
                table: "QueueOnBook",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadBook_Book_BookId",
                table: "ReadBook",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadBook_AspNetUsers_UserId",
                table: "ReadBook",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledBook_Book_BookId",
                table: "ScheduledBook",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledBook_AspNetUsers_UserId",
                table: "ScheduledBook",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_Book_BookId",
                table: "Availability");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_AuthorId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category_CategoryId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publisher_PublisherId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Series_SeriesId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_BookOnHands_Availability_BookId",
                table: "BookOnHands");

            migrationBuilder.DropForeignKey(
                name: "FK_BookOnHands_AspNetUsers_UserId",
                table: "BookOnHands");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Book_BookId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Category_CategoryId",
                table: "Genre");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueOnBook_Availability_BookId",
                table: "QueueOnBook");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueOnBook_AspNetUsers_UserId",
                table: "QueueOnBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadBook_Book_BookId",
                table: "ReadBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadBook_AspNetUsers_UserId",
                table: "ReadBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledBook_Book_BookId",
                table: "ScheduledBook");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledBook_AspNetUsers_UserId",
                table: "ScheduledBook");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Expired",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NotReturned",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OnHands",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Returned",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ScheduledBook",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "ScheduledBook",
                newName: "IdBook");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledBook_UserId",
                table: "ScheduledBook",
                newName: "IX_ScheduledBook_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledBook_BookId",
                table: "ScheduledBook",
                newName: "IX_ScheduledBook_IdBook");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ReadBook",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "ReadBook",
                newName: "IdBook");

            migrationBuilder.RenameIndex(
                name: "IX_ReadBook_UserId",
                table: "ReadBook",
                newName: "IX_ReadBook_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_ReadBook_BookId",
                table: "ReadBook",
                newName: "IX_ReadBook_IdBook");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "QueueOnBook",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "QueueOnBook",
                newName: "IdBook");

            migrationBuilder.RenameIndex(
                name: "IX_QueueOnBook_UserId",
                table: "QueueOnBook",
                newName: "IX_QueueOnBook_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_QueueOnBook_BookId",
                table: "QueueOnBook",
                newName: "IX_QueueOnBook_IdBook");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Genre",
                newName: "IdCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Genre_CategoryId",
                table: "Genre",
                newName: "IX_Genre_IdCategory");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comment",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Comment",
                newName: "IdBook");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                newName: "IX_Comment_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_BookId",
                table: "Comment",
                newName: "IX_Comment_IdBook");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookOnHands",
                newName: "IdUser");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookOnHands",
                newName: "IdBook");

            migrationBuilder.RenameIndex(
                name: "IX_BookOnHands_UserId",
                table: "BookOnHands",
                newName: "IX_BookOnHands_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_BookOnHands_BookId",
                table: "BookOnHands",
                newName: "IX_BookOnHands_IdBook");

            migrationBuilder.RenameColumn(
                name: "SeriesId",
                table: "Book",
                newName: "IdSeries");

            migrationBuilder.RenameColumn(
                name: "PublisherId",
                table: "Book",
                newName: "IdPublisher");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Book",
                newName: "IdCategory");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Book",
                newName: "IdGenre");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Book",
                newName: "IdAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_Book_SeriesId",
                table: "Book",
                newName: "IX_Book_IdSeries");

            migrationBuilder.RenameIndex(
                name: "IX_Book_PublisherId",
                table: "Book",
                newName: "IX_Book_IdPublisher");

            migrationBuilder.RenameIndex(
                name: "IX_Book_CategoryId",
                table: "Book",
                newName: "IX_Book_IdCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                newName: "IX_Book_IdAuthor");

            migrationBuilder.RenameColumn(
                name: "NotReturnedCount",
                table: "Availability",
                newName: "ExpiredCount");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Availability",
                newName: "IdBook");

            migrationBuilder.RenameIndex(
                name: "IX_Availability_IdBook",
                table: "Availability",
                newName: "IX_Availability_BookId");

            migrationBuilder.AlterColumn<int>(
                name: "IdGenre",
                table: "Book",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Book_IdGenre",
                table: "Book",
                column: "IdGenre");

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_Book_IdBook",
                table: "Availability",
                column: "IdBook",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_IdAuthor",
                table: "Book",
                column: "IdAuthor",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category_IdCategory",
                table: "Book",
                column: "IdCategory",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_IdGenre",
                table: "Book",
                column: "IdGenre",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publisher_IdPublisher",
                table: "Book",
                column: "IdPublisher",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Series_IdSeries",
                table: "Book",
                column: "IdSeries",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookOnHands_Availability_IdBook",
                table: "BookOnHands",
                column: "IdBook",
                principalTable: "Availability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookOnHands_AspNetUsers_IdUser",
                table: "BookOnHands",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Book_IdBook",
                table: "Comment",
                column: "IdBook",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_IdUser",
                table: "Comment",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Category_IdCategory",
                table: "Genre",
                column: "IdCategory",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueOnBook_Availability_IdBook",
                table: "QueueOnBook",
                column: "IdBook",
                principalTable: "Availability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueOnBook_AspNetUsers_IdUser",
                table: "QueueOnBook",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadBook_Book_IdBook",
                table: "ReadBook",
                column: "IdBook",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadBook_AspNetUsers_IdUser",
                table: "ReadBook",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledBook_Book_IdBook",
                table: "ScheduledBook",
                column: "IdBook",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledBook_AspNetUsers_IdUser",
                table: "ScheduledBook",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
