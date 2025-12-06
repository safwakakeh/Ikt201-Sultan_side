using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ikt201_Sultan_side.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookinger",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    BordId = table.Column<int>(type: "INTEGER", nullable: false),
                    Tid = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TidSlutt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AntallGjester = table.Column<short>(type: "INTEGER", nullable: false),
                    Bekreftet = table.Column<bool>(type: "INTEGER", nullable: false),
                    BekreftetAdminId = table.Column<int>(type: "INTEGER", nullable: true),
                    BekreftetTid = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookinger", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookinger_Bord_BordId",
                        column: x => x.BordId,
                        principalTable: "Bord",
                        principalColumn: "BordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookinger_Personer_BekreftetAdminId",
                        column: x => x.BekreftetAdminId,
                        principalTable: "Personer",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookinger_Personer_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Personer",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Egenskaper",
                columns: table => new
                {
                    EgenskapId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Navn = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egenskaper", x => x.EgenskapId);
                });

            migrationBuilder.CreateTable(
                name: "Kategorier",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Navn = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorier", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Retter",
                columns: table => new
                {
                    RettId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Navn = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    KategoriId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pris = table.Column<float>(type: "REAL", nullable: false),
                    Tilgjengelighet = table.Column<bool>(type: "INTEGER", nullable: false),
                    Beskrivelse = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retter", x => x.RettId);
                });

            migrationBuilder.CreateTable(
                name: "RetterEgenskaper",
                columns: table => new
                {
                    RetterEgenskaperId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RettId = table.Column<int>(type: "INTEGER", nullable: false),
                    EgenskapId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetterEgenskaper", x => x.RetterEgenskaperId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookinger_BekreftetAdminId",
                table: "Bookinger",
                column: "BekreftetAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookinger_BordId",
                table: "Bookinger",
                column: "BordId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookinger_PersonId",
                table: "Bookinger",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookinger");

            migrationBuilder.DropTable(
                name: "Egenskaper");

            migrationBuilder.DropTable(
                name: "Kategorier");

            migrationBuilder.DropTable(
                name: "Retter");

            migrationBuilder.DropTable(
                name: "RetterEgenskaper");
        }
    }
}
