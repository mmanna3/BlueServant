﻿// <auto-generated />
using System;
using Api.Persistence.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210404032310_Agrega_Datos_a_Huesped_y_Huesped_A_Reserva")]
    partial class Agrega_Datos_a_Huesped_y_Huesped_A_Reserva
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Api.Core.Models.Cama", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Camas");

                    b.HasDiscriminator<string>("Tipo").HasValue("Cama");
                });

            modelBuilder.Entity("Api.Core.Models.CamaCucheta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AbajoId")
                        .HasColumnType("int");

                    b.Property<int?>("ArribaId")
                        .HasColumnType("int");

                    b.Property<int>("HabitacionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AbajoId");

                    b.HasIndex("ArribaId");

                    b.HasIndex("HabitacionId");

                    b.ToTable("CamasCuchetas");
                });

            modelBuilder.Entity("Api.Core.Models.Habitacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EsPrivada")
                        .HasColumnType("bit");

                    b.Property<string>("InformacionAdicional")
                        .HasColumnType("nvarchar(140)")
                        .HasMaxLength(140);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.Property<bool>("TieneBanio")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Habitaciones");
                });

            modelBuilder.Entity("Api.Core.Models.Huesped", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DniOPasaporte")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.ToTable("Huespedes");
                });

            modelBuilder.Entity("Api.Core.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HuespedId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PrimeraNoche")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UltimaNoche")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HuespedId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("Api.Core.Models.ReservaCama", b =>
                {
                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.Property<int>("CamaId")
                        .HasColumnType("int");

                    b.HasKey("ReservaId", "CamaId");

                    b.HasIndex("CamaId");

                    b.ToTable("ReservaCama");
                });

            modelBuilder.Entity("Api.Core.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Api.Core.Models.CamaCuchetaDeAbajo", b =>
                {
                    b.HasBaseType("Api.Core.Models.Cama");

                    b.HasDiscriminator().HasValue("CamaCuchetaDeAbajo");
                });

            modelBuilder.Entity("Api.Core.Models.CamaCuchetaDeArriba", b =>
                {
                    b.HasBaseType("Api.Core.Models.Cama");

                    b.HasDiscriminator().HasValue("CamaCuchetaDeArriba");
                });

            modelBuilder.Entity("Api.Core.Models.CamaIndividual", b =>
                {
                    b.HasBaseType("Api.Core.Models.Cama");

                    b.Property<int>("HabitacionId")
                        .HasColumnName("Individual_HabitacionId")
                        .HasColumnType("int");

                    b.HasIndex("HabitacionId");

                    b.HasDiscriminator().HasValue("CamaIndividual");
                });

            modelBuilder.Entity("Api.Core.Models.CamaMatrimonial", b =>
                {
                    b.HasBaseType("Api.Core.Models.Cama");

                    b.Property<int>("HabitacionId")
                        .HasColumnName("Matrimonial_HabitacionId")
                        .HasColumnType("int");

                    b.HasIndex("HabitacionId");

                    b.HasDiscriminator().HasValue("CamaMatrimonial");
                });

            modelBuilder.Entity("Api.Core.Models.CamaCucheta", b =>
                {
                    b.HasOne("Api.Core.Models.CamaCuchetaDeAbajo", "Abajo")
                        .WithMany()
                        .HasForeignKey("AbajoId");

                    b.HasOne("Api.Core.Models.CamaCuchetaDeArriba", "Arriba")
                        .WithMany()
                        .HasForeignKey("ArribaId");

                    b.HasOne("Api.Core.Models.Habitacion", "Habitacion")
                        .WithMany("CamasCuchetas")
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Models.Reserva", b =>
                {
                    b.HasOne("Api.Core.Models.Huesped", "Huesped")
                        .WithMany()
                        .HasForeignKey("HuespedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Models.ReservaCama", b =>
                {
                    b.HasOne("Api.Core.Models.Cama", "Cama")
                        .WithMany("ReservaCamas")
                        .HasForeignKey("CamaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Core.Models.Reserva", "Reserva")
                        .WithMany("ReservaCamas")
                        .HasForeignKey("ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Models.CamaIndividual", b =>
                {
                    b.HasOne("Api.Core.Models.Habitacion", "Habitacion")
                        .WithMany("CamasIndividuales")
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Models.CamaMatrimonial", b =>
                {
                    b.HasOne("Api.Core.Models.Habitacion", "Habitacion")
                        .WithMany("CamasMatrimoniales")
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
