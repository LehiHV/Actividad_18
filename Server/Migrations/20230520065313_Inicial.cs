using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Actividad_18.Server.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trabajadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Puesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salario = table.Column<int>(type: "int", nullable: false),
                    Id_Sup = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabajadorId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Horas = table.Column<int>(type: "int", nullable: false),
                    FechaI = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaF = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_Edificios = table.Column<int>(type: "int", nullable: false),
                    Nombre_Edificio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trabajadores2Id = table.Column<int>(type: "int", nullable: false),
                    Trabajador2Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comandas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comandas_Trabajadores_Trabajador2Id",
                        column: x => x.Trabajador2Id,
                        principalTable: "Trabajadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Edificios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Edificio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dirreccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrabajadoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edificios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Edificios_Trabajadores_TrabajadoresId",
                        column: x => x.TrabajadoresId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comandas_Trabajador2Id",
                table: "Comandas",
                column: "Trabajador2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Edificios_TrabajadoresId",
                table: "Edificios",
                column: "TrabajadoresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comandas");

            migrationBuilder.DropTable(
                name: "Edificios");

            migrationBuilder.DropTable(
                name: "Trabajadores");
        }
    }
}
