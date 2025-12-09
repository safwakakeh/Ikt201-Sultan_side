using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ikt201_Sultan_side.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniquePersonIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Personer_Epost",
                table: "Personer",
                column: "Epost",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personer_Telefon",
                table: "Personer",
                column: "Telefon",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Personer_Epost",
                table: "Personer");

            migrationBuilder.DropIndex(
                name: "IX_Personer_Telefon",
                table: "Personer");
        }
    }
}
