using Microsoft.EntityFrameworkCore.Migrations;

namespace Censo.Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Census_Ethnicity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Census_Ethnicity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Census_Gender",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Census_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Census_Region",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Census_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Census_Schooling",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Census_Schooling", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Census_Ethnicity");

            migrationBuilder.DropTable(
                name: "Census_Gender");

            migrationBuilder.DropTable(
                name: "Census_Region");

            migrationBuilder.DropTable(
                name: "Census_Schooling");
        }
    }
}
