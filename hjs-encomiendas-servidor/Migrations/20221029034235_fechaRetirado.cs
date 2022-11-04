using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    public partial class fechaRetirado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fechaRetiro",
                table: "Pedido",
                newName: "fechaRetirado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fechaRetirado",
                table: "Pedido",
                newName: "fechaRetiro");
        }
    }
}
