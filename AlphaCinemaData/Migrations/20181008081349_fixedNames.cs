using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaCinemaData.Migrations
{
    public partial class fixedNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projection_Cities_CityId",
                table: "Projection");

            migrationBuilder.DropForeignKey(
                name: "FK_Projection_Movies_MovieId",
                table: "Projection");

            migrationBuilder.DropForeignKey(
                name: "FK_Projection_OpenHour_OpenHourId",
                table: "Projection");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchedMovies_Projection_ProjectionId",
                table: "WatchedMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projection",
                table: "Projection");

            migrationBuilder.DropIndex(
                name: "IX_Projection_MovieId",
                table: "Projection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenHour",
                table: "OpenHour");

            migrationBuilder.RenameTable(
                name: "Projection",
                newName: "Projections");

            migrationBuilder.RenameTable(
                name: "OpenHour",
                newName: "OpenHours");

            migrationBuilder.RenameIndex(
                name: "IX_Projection_OpenHourId",
                table: "Projections",
                newName: "IX_Projections_OpenHourId");

            migrationBuilder.RenameIndex(
                name: "IX_Projection_CityId",
                table: "Projections",
                newName: "IX_Projections_CityId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projections",
                table: "Projections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenHours",
                table: "OpenHours",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId",
                table: "Projections",
                columns: new[] { "MovieId", "CityId", "OpenHourId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projections_Cities_CityId",
                table: "Projections",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projections_Movies_MovieId",
                table: "Projections",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projections_OpenHours_OpenHourId",
                table: "Projections",
                column: "OpenHourId",
                principalTable: "OpenHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedMovies_Projections_ProjectionId",
                table: "WatchedMovies",
                column: "ProjectionId",
                principalTable: "Projections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projections_Cities_CityId",
                table: "Projections");

            migrationBuilder.DropForeignKey(
                name: "FK_Projections_Movies_MovieId",
                table: "Projections");

            migrationBuilder.DropForeignKey(
                name: "FK_Projections_OpenHours_OpenHourId",
                table: "Projections");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchedMovies_Projections_ProjectionId",
                table: "WatchedMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projections",
                table: "Projections");

            migrationBuilder.DropIndex(
                name: "IX_Projections_MovieId_CityId_OpenHourId",
                table: "Projections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenHours",
                table: "OpenHours");

            migrationBuilder.RenameTable(
                name: "Projections",
                newName: "Projection");

            migrationBuilder.RenameTable(
                name: "OpenHours",
                newName: "OpenHour");

            migrationBuilder.RenameIndex(
                name: "IX_Projections_OpenHourId",
                table: "Projection",
                newName: "IX_Projection_OpenHourId");

            migrationBuilder.RenameIndex(
                name: "IX_Projections_CityId",
                table: "Projection",
                newName: "IX_Projection_CityId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projection",
                table: "Projection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenHour",
                table: "OpenHour",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projection_MovieId",
                table: "Projection",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projection_Cities_CityId",
                table: "Projection",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projection_Movies_MovieId",
                table: "Projection",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projection_OpenHour_OpenHourId",
                table: "Projection",
                column: "OpenHourId",
                principalTable: "OpenHour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedMovies_Projection_ProjectionId",
                table: "WatchedMovies",
                column: "ProjectionId",
                principalTable: "Projection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
