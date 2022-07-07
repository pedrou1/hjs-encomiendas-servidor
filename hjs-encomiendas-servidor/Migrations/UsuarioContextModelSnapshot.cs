﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using hjs_encomiendas_servidor.Persistencia;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    [DbContext(typeof(UsuarioContext))]
    partial class UsuarioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.CategoriaUsuario", b =>
                {
                    b.Property<int>("idCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCategoria"), 1L, 1);

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("idCategoria");

                    b.ToTable("CategoriaUsuario");
                });

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.Usuario", b =>
                {
                    b.Property<int>("idUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUsuario"), 1L, 1);

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("idCategoria")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("telefono")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("usuario")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("idUsuario");

                    b.HasIndex("idCategoria");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.Usuario", b =>
                {
                    b.HasOne("hjs_encomiendas_servidor.Modelo.CategoriaUsuario", "categoriaUsuario")
                        .WithMany()
                        .HasForeignKey("idCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoriaUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}
