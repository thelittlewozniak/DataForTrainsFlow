using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDataTrainsFlow.Migrations
{
    public partial class AddingDayAndMonthOnAnalyze : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Analyzes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Analyzes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Analyzes");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Analyzes");
        }
    }
}
