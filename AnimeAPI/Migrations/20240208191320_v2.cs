using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    IdMovie = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tittle = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Review = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.IdMovie);
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    IdActor = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MoviesIdMovie = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.IdActor);
                    table.ForeignKey(
                        name: "FK_Actors_Movies_MoviesIdMovie",
                        column: x => x.MoviesIdMovie,
                        principalTable: "Movies",
                        principalColumn: "IdMovie");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_MoviesIdMovie",
                table: "Actors",
                column: "MoviesIdMovie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
