using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornitori",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornitori", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Trama = table.Column<string>(type: "TEXT", nullable: false),
                    Autore = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prezzo = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    QuantitaDisponibile = table.Column<int>(type: "int", nullable: false),
                    DataInserimento = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Fk_fornitore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libri_Fornitori_Fk_fornitore",
                        column: x => x.Fk_fornitore,
                        principalTable: "Fornitori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libri_Fk_fornitore",
                table: "Libri",
                column: "Fk_fornitore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libri");

            migrationBuilder.DropTable(
                name: "Fornitori");
        }
    }
}
