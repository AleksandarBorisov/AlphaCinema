using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaCinemaData.Migrations
{
    public partial class Removed_IAuditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchedMovies",
                table: "WatchedMovies");

            migrationBuilder.DropIndex(
                name: "IX_WatchedMovies_UserId",
                table: "WatchedMovies");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "WatchedMovies");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Projections");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "OpenHours");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "OpenHours");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "OpenHours");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "MoviesGenres");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Cities");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Movies",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchedMovies",
                table: "WatchedMovies",
                columns: new[] { "UserId", "ProjectionId" });

            migrationBuilder.CreateIndex(
                name: "IX_WatchedMovies_ProjectionId",
                table: "WatchedMovies",
                column: "ProjectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OpenHours_StartHour",
                table: "OpenHours",
                column: "StartHour",
                unique: true,
                filter: "[StartHour] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Name",
                table: "Movies",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchedMovies",
                table: "WatchedMovies");

            migrationBuilder.DropIndex(
                name: "IX_WatchedMovies_ProjectionId",
                table: "WatchedMovies");

            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_OpenHours_StartHour",
                table: "OpenHours");

            migrationBuilder.DropIndex(
                name: "IX_Movies_Name",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "WatchedMovies",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Projections",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "OpenHours",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "OpenHours",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "OpenHours",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "MoviesGenres",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Movies",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Genres",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Genres",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Genres",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Cities",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchedMovies",
                table: "WatchedMovies",
                columns: new[] { "ProjectionId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_WatchedMovies_UserId",
                table: "WatchedMovies",
                column: "UserId");
        }
    }
}
