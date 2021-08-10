using Microsoft.EntityFrameworkCore.Migrations;

namespace Wordtrack.Data.Migrations
{
    public partial class Rename_Collumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishedYear",
                table: "Books",
                newName: "YearPublished");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearPublished",
                table: "Books",
                newName: "PublishedYear");
        }
    }
}
