using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDataTrainsFlow.Migrations
{
    public partial class EditingAnalyze : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "station",
                table: "Analyzes",
                newName: "stationDepart");

            migrationBuilder.AddColumn<string>(
                name: "stationArrival",
                table: "Analyzes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "stationArrival",
                table: "Analyzes");

            migrationBuilder.RenameColumn(
                name: "stationDepart",
                table: "Analyzes",
                newName: "station");
        }
    }
}
