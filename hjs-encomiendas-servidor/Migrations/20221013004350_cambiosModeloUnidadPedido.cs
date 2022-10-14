using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    public partial class cambiosModeloUnidadPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "anio",
                table: "UnidadTransporte",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "marca",
                table: "UnidadTransporte",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "matricula",
                table: "UnidadTransporte",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "modelo",
                table: "UnidadTransporte",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "padron",
                table: "UnidadTransporte",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "apartamento",
                table: "Pedido",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "Pedido",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nroPuerta",
                table: "Pedido",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "anio",
                table: "UnidadTransporte");

            migrationBuilder.DropColumn(
                name: "marca",
                table: "UnidadTransporte");

            migrationBuilder.DropColumn(
                name: "matricula",
                table: "UnidadTransporte");

            migrationBuilder.DropColumn(
                name: "modelo",
                table: "UnidadTransporte");

            migrationBuilder.DropColumn(
                name: "padron",
                table: "UnidadTransporte");

            migrationBuilder.DropColumn(
                name: "apartamento",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "nroPuerta",
                table: "Pedido");
        }
    }
}
