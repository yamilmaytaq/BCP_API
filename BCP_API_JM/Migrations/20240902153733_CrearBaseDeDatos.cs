using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCP_API_JM.Migrations
{
    /// <inheritdoc />
    public partial class CrearBaseDeDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BD_CLIENTES_JM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Paterno = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Materno = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CI = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Celular = table.Column<int>(type: "int", nullable: false),
                    NivelIngresos = table.Column<double>(type: "decimal", maxLength: 18, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cuenta = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BD_CLIENTES_JM", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BD_CLIENTES_JM");
        }
    }
}
