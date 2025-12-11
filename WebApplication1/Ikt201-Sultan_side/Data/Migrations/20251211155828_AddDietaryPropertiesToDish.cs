using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ikt201_Sultan_side.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDietaryPropertiesToDish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHalal",
                table: "Dishes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegan",
                table: "Dishes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHalal",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "IsVegan",
                table: "Dishes");
        }
    }
}
