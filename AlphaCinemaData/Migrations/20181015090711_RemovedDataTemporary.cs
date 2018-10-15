using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaCinemaData.Migrations
{
    public partial class RemovedDataTemporary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId_Date",
                table: "Projections");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Projections");

            migrationBuilder.CreateIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId",
                table: "Projections",
                columns: new[] { "MovieId", "CityId", "OpenHourId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId",
                table: "Projections");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Projections",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId_Date",
                table: "Projections",
                columns: new[] { "MovieId", "CityId", "OpenHourId", "Date" },
                unique: true);
        }
    }
}
