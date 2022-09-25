using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    public partial class Gastos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TipoPedido", true);
            migrationBuilder.Sql("DELETE FROM Pedido", true);
            
            migrationBuilder.AddColumn<int>(
                name: "idTipoPedido",
                table: "Pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Gasto",
                columns: table => new
                {
                    idGasto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idTransporte = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    costo = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gasto", x => x.idGasto);
                    table.ForeignKey(
                        name: "FK_Gasto_UnidadTransporte_idTransporte",
                        column: x => x.idTransporte,
                        principalTable: "UnidadTransporte",
                        principalColumn: "idUnidadTransporte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gasto_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_idTipoPedido",
                table: "Pedido",
                column: "idTipoPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_idTransporte",
                table: "Gasto",
                column: "idTransporte");

            migrationBuilder.CreateIndex(
                name: "IX_Gasto_idUsuario",
                table: "Gasto",
                column: "idUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_TipoPedido_idTipoPedido",
                table: "Pedido",
                column: "idTipoPedido",
                principalTable: "TipoPedido",
                principalColumn: "idTipoPedido",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_TipoPedido_idTipoPedido",
                table: "Pedido");

            migrationBuilder.DropTable(
                name: "Gasto");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_idTipoPedido",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "idTipoPedido",
                table: "Pedido");
        }
    }
}
