﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using hjs_encomiendas_servidor.Persistencia;

#nullable disable

namespace hjs_encomiendas_servidor.Migrations
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20220929013911_direccionPedido")]
    partial class direccionPedido
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.Estado", b =>
                {
                    b.Property<int>("idEstado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEstado"), 1L, 1);

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("idEstado");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.EstadoPedido", b =>
                {
                    b.Property<int>("idEstado")
                        .HasColumnType("int");

                    b.Property<DateTime>("fechaEstadoPedido")
                        .HasColumnType("datetime2");

                    b.Property<int>("idPedido")
                        .HasColumnType("int");

                    b.HasKey("idEstado");

                    b.HasIndex("idPedido");

                    b.ToTable("EstadoPedido");
                });

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.Gasto", b =>
                {
                    b.Property<int>("idGasto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idGasto"), 1L, 1);

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<int>("costo")
                        .HasColumnType("int");

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("idTransporte")
                        .HasColumnType("int");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.HasKey("idGasto");

                    b.HasIndex("idTransporte");

                    b.HasIndex("idUsuario");

                    b.ToTable("Gasto");
                });

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.Pedido", b =>
                {
                    b.Property<int>("idPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPedido"), 1L, 1);

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<int>("cubicaje")
                        .HasColumnType("int");

                    b.Property<int>("distanciaRecorrida")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("fechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("fechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("fechaRetiro")
                        .HasColumnType("datetime2");

                    b.Property<string>("horaLimite")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("idChofer")
                        .HasColumnType("int");

                    b.Property<int>("idCliente")
                        .HasColumnType("int");

                    b.Property<int>("idTipoPedido")
                        .HasColumnType("int");

                    b.Property<int>("idTransporte")
                        .HasColumnType("int");

                    b.Property<float?>("latitude")
                        .HasColumnType("real");

                    b.Property<float?>("longitude")
                        .HasColumnType("real");

                    b.Property<string>("nombreDireccion")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("orden")
                        .HasColumnType("int");

                    b.Property<int>("peso")
                        .HasColumnType("int");

                    b.Property<int>("tamaño")
                        .HasColumnType("int");

                    b.Property<int>("tarifa")
                        .HasColumnType("int");

                    b.HasKey("idPedido");

                    b.HasIndex("idChofer");

                    b.HasIndex("idCliente");

                    b.HasIndex("idTipoPedido");

                    b.HasIndex("idTransporte");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.TipoPedido", b =>
                {
                    b.Property<int>("idTipoPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idTipoPedido"), 1L, 1);

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("pesoDesde")
                        .HasColumnType("int");

                    b.Property<int>("pesoHasta")
                        .HasColumnType("int");

                    b.Property<int>("tarifa")
                        .HasColumnType("int");

                    b.HasKey("idTipoPedido");

                    b.ToTable("TipoPedido");
                });

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.UnidadTransporte", b =>
                {
                    b.Property<int>("idUnidadTransporte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUnidadTransporte"), 1L, 1);

                    b.Property<bool>("activo")
                        .HasColumnType("bit");

                    b.Property<int>("capacidad")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("promedioConsumo")
                        .HasColumnType("int");

                    b.HasKey("idUnidadTransporte");

                    b.ToTable("UnidadTransporte");
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

                    b.Property<string>("direccion")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("email")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

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

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.EstadoPedido", b =>
                {
                    b.HasOne("hjs_encomiendas_servidor.Modelo.Estado", "estado")
                        .WithMany()
                        .HasForeignKey("idEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hjs_encomiendas_servidor.Modelo.Pedido", "pedido")
                        .WithMany()
                        .HasForeignKey("idPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("estado");

                    b.Navigation("pedido");
                });

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.Gasto", b =>
                {
                    b.HasOne("hjs_encomiendas_servidor.Modelo.UnidadTransporte", "transporte")
                        .WithMany()
                        .HasForeignKey("idTransporte")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hjs_encomiendas_servidor.Modelo.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("transporte");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("hjs_encomiendas_servidor.Modelo.Pedido", b =>
                {
                    b.HasOne("hjs_encomiendas_servidor.Modelo.Usuario", "chofer")
                        .WithMany()
                        .HasForeignKey("idChofer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hjs_encomiendas_servidor.Modelo.Usuario", "cliente")
                        .WithMany()
                        .HasForeignKey("idCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hjs_encomiendas_servidor.Modelo.TipoPedido", "tipoPedido")
                        .WithMany()
                        .HasForeignKey("idTipoPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hjs_encomiendas_servidor.Modelo.UnidadTransporte", "transporte")
                        .WithMany()
                        .HasForeignKey("idTransporte")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("chofer");

                    b.Navigation("cliente");

                    b.Navigation("tipoPedido");

                    b.Navigation("transporte");
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