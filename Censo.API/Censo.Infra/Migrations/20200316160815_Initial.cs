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

            migrationBuilder.CreateTable(
                name: "Census_Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    EthnicityId = table.Column<int>(nullable: true),
                    GenderId = table.Column<int>(nullable: true),
                    SchoolingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Census_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Census_Answers_Census_Ethnicity_EthnicityId",
                        column: x => x.EthnicityId,
                        principalTable: "Census_Ethnicity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Census_Answers_Census_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Census_Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Census_Answers_Census_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Census_Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Census_Answers_Census_Schooling_SchoolingId",
                        column: x => x.SchoolingId,
                        principalTable: "Census_Schooling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Census_AnswersParentChild",
                columns: table => new
                {
                    ParentId = table.Column<int>(nullable: false),
                    ChildId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Census_AnswersParentChild", x => new { x.ParentId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_Census_AnswersParentChild_Census_Answers_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Census_Answers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Census_AnswersParentChild_Census_Answers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Census_Answers",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_Census_Answers_EthnicityId",
                table: "Census_Answers",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_Census_Answers_GenderId",
                table: "Census_Answers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Census_Answers_RegionId",
                table: "Census_Answers",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Census_Answers_SchoolingId",
                table: "Census_Answers",
                column: "SchoolingId");

            migrationBuilder.CreateIndex(
                name: "IX_Census_Answers_FirstName_LastName",
                table: "Census_Answers",
                columns: new[] { "FirstName", "LastName" },
                unique: true,
                filter: "[FirstName] IS NOT NULL AND [LastName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Census_Answers_FirstName_LastName_GenderId_RegionId_EthnicityId_SchoolingId",
                table: "Census_Answers",
                columns: new[] { "FirstName", "LastName", "GenderId", "RegionId", "EthnicityId", "SchoolingId" });

            migrationBuilder.CreateIndex(
                name: "IX_Census_AnswersParentChild_ChildId",
                table: "Census_AnswersParentChild",
                column: "ChildId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Census_AnswersParentChild");

            migrationBuilder.DropTable(
                name: "Census_Answers");

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
