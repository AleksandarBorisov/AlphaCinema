using Microsoft.EntityFrameworkCore.Migrations;

namespace AlphaCinemaData.Migrations
{
    public partial class edo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MoviesGenres",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MoviesGenres");
        }
    }
}
