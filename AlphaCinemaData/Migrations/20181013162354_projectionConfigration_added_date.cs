using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaCinemaData.Migrations
{
    public partial class projectionConfigration_added_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId",
                table: "Projections");

            migrationBuilder.CreateIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId_Date",
                table: "Projections",
                columns: new[] { "MovieId", "CityId", "OpenHourId", "Date" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId_Date",
                table: "Projections");

            migrationBuilder.CreateIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId",
                table: "Projections",
                columns: new[] { "MovieId", "CityId", "OpenHourId" },
                unique: true);
        }
    }
}
