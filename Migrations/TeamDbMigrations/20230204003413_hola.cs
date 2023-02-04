using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace chessAPI.Migrations.TeamDbMigrations
{
    /// <inheritdoc />
    public partial class hola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idjugador1 = table.Column<int>(type: "integer", nullable: false),
                    email1 = table.Column<string>(type: "text", nullable: false),
                    idjugador2 = table.Column<int>(type: "integer", nullable: false),
                    email2 = table.Column<string>(type: "text", nullable: false),
                    punteoequipo1 = table.Column<int>(type: "integer", nullable: false),
                    punteoequipo2 = table.Column<int>(type: "integer", nullable: false),
                    idjugador3 = table.Column<int>(type: "integer", nullable: false),
                    email3 = table.Column<string>(type: "text", nullable: false),
                    idjugador4 = table.Column<int>(type: "integer", nullable: false),
                    email4 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipos");
        }
    }
}
