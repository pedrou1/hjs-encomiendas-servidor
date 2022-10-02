using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    public partial class choferUnidadTransporte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM UnidadTransporte", true);
            
            migrationBuilder.AddColumn<int>(
                name: "idChofer",
                table: "UnidadTransporte",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UnidadTransporte_idChofer",
                table: "UnidadTransporte",
                column: "idChofer");

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadTransporte_Usuarios_idChofer",
                table: "UnidadTransporte",
                column: "idChofer",
                principalTable: "Usuarios",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnidadTransporte_Usuarios_idChofer",
                table: "UnidadTransporte");

            migrationBuilder.DropIndex(
                name: "IX_UnidadTransporte_idChofer",
                table: "UnidadTransporte");

            migrationBuilder.DropColumn(
                name: "idChofer",
                table: "UnidadTransporte");
        }
    }
}
