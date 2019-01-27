using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDataTrainsFlow.Migrations
{
    public partial class AddingAnalyze : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analyzes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    delay = table.Column<int>(nullable: false),
                    station = table.Column<string>(nullable: true),
                    time = table.Column<int>(nullable: false),
                    vehicle = table.Column<string>(nullable: true),
                    weatherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analyzes_Weathers_weatherId",
                        column: x => x.weatherId,
                        principalTable: "Weathers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyzes_weatherId",
                table: "Analyzes",
                column: "weatherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyzes");
        }
    }
}
