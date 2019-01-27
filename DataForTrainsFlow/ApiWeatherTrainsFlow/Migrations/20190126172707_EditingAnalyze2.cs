using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDataTrainsFlow.Migrations
{
    public partial class EditingAnalyze2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyzes_Weathers_weatherId",
                table: "Analyzes");

            migrationBuilder.RenameColumn(
                name: "weatherId",
                table: "Analyzes",
                newName: "WeatherId");

            migrationBuilder.RenameColumn(
                name: "vehicle",
                table: "Analyzes",
                newName: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "Analyzes",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "stationDepart",
                table: "Analyzes",
                newName: "StationDepart");

            migrationBuilder.RenameColumn(
                name: "stationArrival",
                table: "Analyzes",
                newName: "StationArrival");

            migrationBuilder.RenameColumn(
                name: "delay",
                table: "Analyzes",
                newName: "Delay");

            migrationBuilder.RenameIndex(
                name: "IX_Analyzes_weatherId",
                table: "Analyzes",
                newName: "IX_Analyzes_WeatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyzes_Weathers_WeatherId",
                table: "Analyzes",
                column: "WeatherId",
                principalTable: "Weathers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyzes_Weathers_WeatherId",
                table: "Analyzes");

            migrationBuilder.RenameColumn(
                name: "WeatherId",
                table: "Analyzes",
                newName: "weatherId");

            migrationBuilder.RenameColumn(
                name: "Vehicle",
                table: "Analyzes",
                newName: "vehicle");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Analyzes",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "StationDepart",
                table: "Analyzes",
                newName: "stationDepart");

            migrationBuilder.RenameColumn(
                name: "StationArrival",
                table: "Analyzes",
                newName: "stationArrival");

            migrationBuilder.RenameColumn(
                name: "Delay",
                table: "Analyzes",
                newName: "delay");

            migrationBuilder.RenameIndex(
                name: "IX_Analyzes_WeatherId",
                table: "Analyzes",
                newName: "IX_Analyzes_weatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyzes_Weathers_weatherId",
                table: "Analyzes",
                column: "weatherId",
                principalTable: "Weathers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
