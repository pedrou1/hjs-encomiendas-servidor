using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    public partial class fechaRetiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fechaCreacion",
                table: "Pedido",
                newName: "fechaRetiro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fechaRetiro",
                table: "Pedido",
                newName: "fechaCreacion");
        }
    }
}
