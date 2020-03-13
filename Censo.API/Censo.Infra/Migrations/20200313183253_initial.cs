using Microsoft.EntityFrameworkCore.Migrations;

namespace Censo.Infra.Migrations
{
    public partial class initial : Migration
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

            migrationBuilder.InsertData(
                table: "Census_Ethnicity",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "Branco" },
                    { 2, "Pardo" },
                    { 3, "Preto" },
                    { 4, "Amarelo" },
                    { 5, "Indígena" }
                });

            migrationBuilder.InsertData(
                table: "Census_Gender",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "Masculino" },
                    { 2, "Feminino" }
                });

            migrationBuilder.InsertData(
                table: "Census_Region",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 5, "Norte" },
                    { 4, "Nordeste" },
                    { 3, "Centro-Oeste" },
                    { 2, "Sul" },
                    { 1, "Sudeste" }
                });

            migrationBuilder.InsertData(
                table: "Census_Schooling",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "Fundamental I Incompleto" },
                    { 2, "Fundamental I Completo" },
                    { 3, "Fundamental II Incompleto" },
                    { 4, "Fundamental II Completo" },
                    { 5, "Médio Imcompleto" },
                    { 6, "Médio Completo" },
                    { 7, "Superior Incompleto" },
                    { 8, "Superior Completo" }
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
