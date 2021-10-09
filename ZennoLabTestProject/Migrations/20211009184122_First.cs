using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZennoLabTestProject.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDataSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HasCyrillic = table.Column<bool>(type: "bit", nullable: false),
                    HasLatin = table.Column<bool>(type: "bit", nullable: false),
                    HasNumber = table.Column<bool>(type: "bit", nullable: false),
                    HasSymbol = table.Column<bool>(type: "bit", nullable: false),
                    CaseSensitive = table.Column<bool>(type: "bit", nullable: false),
                    AnswerType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDataSets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDataSets");
        }
    }
}
