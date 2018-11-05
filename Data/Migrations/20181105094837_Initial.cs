using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(maxLength: 500, nullable: false),
                    AuthorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotes_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 1, "Albert Einstein" },
                    { 2, "Dr. Seuss" },
                    { 3, "Mahatma Gandhi" },
                    { 4, "Mark Twain" }
                });

            migrationBuilder.InsertData(
                table: "Quotes",
                columns: new[] { "Id", "AuthorId", "Content" },
                values: new object[,]
                {
                    { 1, 1, "Two things are infinite: the universe and human stupidity; and I'm not sure about the universe." },
                    { 2, 2, "Don't cry because it's over, smile because it happened." },
                    { 3, 2, "You know you're in love when you can't fall asleep because reality is finally better than your dreams." },
                    { 4, 3, "Be the change that you wish to see in the world." },
                    { 6, 3, "Live as if you were to die tomorrow. Learn as if you were to live forever." },
                    { 5, 4, "If you tell the truth, you don't have to remember anything" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_AuthorId",
                table: "Quotes",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
