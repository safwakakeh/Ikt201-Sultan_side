using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ikt201_Sultan_side.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBordTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bord",
                columns: table => new
                {
                    BordId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Plasser = table.Column<short>(type: "INTEGER", nullable: false),
                    MaksPlasser = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bord", x => x.BordId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bord");
        }
    }
}
