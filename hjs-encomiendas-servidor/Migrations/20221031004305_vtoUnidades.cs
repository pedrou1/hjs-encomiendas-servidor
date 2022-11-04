using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    public partial class vtoUnidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_UnidadTransporte_idTransporte",
                table: "Gasto");

            migrationBuilder.AddColumn<DateTime>(
                name: "vtoApplus",
                table: "UnidadTransporte",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "vtoMinisterio",
                table: "UnidadTransporte",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "vtoPatente",
                table: "UnidadTransporte",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "vtoSeguro",
                table: "UnidadTransporte",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idTransporte",
                table: "Gasto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_UnidadTransporte_idTransporte",
                table: "Gasto",
                column: "idTransporte",
                principalTable: "UnidadTransporte",
                principalColumn: "idUnidadTransporte");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gasto_UnidadTransporte_idTransporte",
                table: "Gasto");

            migrationBuilder.DropColumn(
                name: "vtoApplus",
                table: "UnidadTransporte");

            migrationBuilder.DropColumn(
                name: "vtoMinisterio",
                table: "UnidadTransporte");

            migrationBuilder.DropColumn(
                name: "vtoPatente",
                table: "UnidadTransporte");

            migrationBuilder.DropColumn(
                name: "vtoSeguro",
                table: "UnidadTransporte");

            migrationBuilder.AlterColumn<int>(
                name: "idTransporte",
                table: "Gasto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gasto_UnidadTransporte_idTransporte",
                table: "Gasto",
                column: "idTransporte",
                principalTable: "UnidadTransporte",
                principalColumn: "idUnidadTransporte",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
