using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    public partial class reservaPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "reservado",
                table: "Pedido",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reservado",
                table: "Pedido");
        }
    }
}
