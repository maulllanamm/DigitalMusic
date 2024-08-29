using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalMusic.Persistence.Migrations
{
    public partial class create_songs_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "songs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    performer = table.Column<string>(type: "text", nullable: false),
                    genre = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    album_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: true),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "text", nullable: false, defaultValue: "system"),
                    modified_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    modified_by = table.Column<string>(type: "text", nullable: false, defaultValue: "system")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_songs", x => x.id);
                    table.ForeignKey(
                        name: "FK_songs_albums_album_id",
                        column: x => x.album_id,
                        principalTable: "albums",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_songs_album_id",
                table: "songs",
                column: "album_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "songs");
        }
    }
}
