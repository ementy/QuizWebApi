using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UniqueAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Authors_FullName",
                table: "Authors",
                column: "FullName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authors_FullName",
                table: "Authors");
        }
    }
}
