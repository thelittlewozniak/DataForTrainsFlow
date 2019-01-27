using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDataTrainsFlow.Migrations
{
    public partial class AddingDayAndMonthOnAnalyzeAndRemovingTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Analyzes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Analyzes",
                nullable: false,
                defaultValue: 0);
        }
    }
}
