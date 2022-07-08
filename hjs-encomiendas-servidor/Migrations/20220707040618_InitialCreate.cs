using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaUsuario",
                columns: table => new
                {
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaUsuario", x => x.idCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCategoria = table.Column<int>(type: "int", nullable: false),
                    usuario = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_CategoriaUsuario_idCategoria",
                        column: x => x.idCategoria,
                        principalTable: "CategoriaUsuario",
                        principalColumn: "idCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idCategoria",
                table: "Usuarios",
                column: "idCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "CategoriaUsuario");
        }
    }
}
