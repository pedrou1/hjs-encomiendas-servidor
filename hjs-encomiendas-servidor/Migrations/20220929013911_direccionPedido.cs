using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    public partial class direccionPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "latitude",
                table: "Pedido",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "longitude",
                table: "Pedido",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nombreDireccion",
                table: "Pedido",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitude",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "nombreDireccion",
                table: "Pedido");
        }
    }
}
