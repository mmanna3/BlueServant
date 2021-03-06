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
    [Migration("20210524174709_Renombra_Huesped_A_Pasajero")]
    partial class Renombra_Huesped_A_Pasajero
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Api.Core.Entidades.Cama", b =>
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

            modelBuilder.Entity("Api.Core.Entidades.CamaCucheta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AbajoId")
                        .HasColumnType("int");

                    b.Property<int>("ArribaId")
                        .HasColumnType("int");

                    b.Property<int>("HabitacionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HabitacionId");

                    b.ToTable("CamasCuchetas");
                });

            modelBuilder.Entity("Api.Core.Entidades.Habitacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InformacionAdicional")
                        .HasColumnType("nvarchar(140)")
                        .HasMaxLength(140);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.Property<bool>("TieneBanio")
                        .HasColumnType("bit");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Habitaciones");

                    b.HasDiscriminator<string>("Tipo").HasValue("Habitacion");
                });

            modelBuilder.Entity("Api.Core.Entidades.Pasajero", b =>
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

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.HasIndex("DniOPasaporte")
                        .IsUnique();

                    b.ToTable("Pasajeros");
                });

            modelBuilder.Entity("Api.Core.Entidades.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Canal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CantidadDePasajeros")
                        .HasColumnType("int");

                    b.Property<int>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<TimeSpan>("HoraEstimadaDeLlegada")
                        .HasColumnType("time");

                    b.Property<int>("PasajeroTitularId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PrimeraNoche")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UltimaNoche")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PasajeroTitularId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("Api.Core.Entidades.ReservaCama", b =>
                {
                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.Property<int>("CamaId")
                        .HasColumnType("int");

                    b.HasKey("ReservaId", "CamaId");

                    b.HasIndex("CamaId");

                    b.ToTable("ReservaCama");
                });

            modelBuilder.Entity("Api.Core.Entidades.ReservaHabitacionPrivada", b =>
                {
                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.Property<int>("HabitacionPrivadaId")
                        .HasColumnType("int");

                    b.HasKey("ReservaId", "HabitacionPrivadaId");

                    b.HasIndex("HabitacionPrivadaId");

                    b.ToTable("ReservaHabitacionPrivada");
                });

            modelBuilder.Entity("Api.Core.Entidades.Usuario", b =>
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

            modelBuilder.Entity("Api.Core.Entidades.CamaCuchetaDeAbajo", b =>
                {
                    b.HasBaseType("Api.Core.Entidades.Cama");

                    b.Property<int>("CamaCuchetaId")
                        .HasColumnName("CamaCuchetaDeAbajo_CuchetaId")
                        .HasColumnType("int");

                    b.HasIndex("CamaCuchetaId")
                        .IsUnique()
                        .HasFilter("[CamaCuchetaDeAbajo_CuchetaId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("CamaCuchetaDeAbajo");
                });

            modelBuilder.Entity("Api.Core.Entidades.CamaCuchetaDeArriba", b =>
                {
                    b.HasBaseType("Api.Core.Entidades.Cama");

                    b.Property<int>("CamaCuchetaId")
                        .HasColumnName("CamaCuchetaDeArriba_CuchetaId")
                        .HasColumnType("int");

                    b.HasIndex("CamaCuchetaId")
                        .IsUnique()
                        .HasFilter("[CamaCuchetaDeArriba_CuchetaId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("CamaCuchetaDeArriba");
                });

            modelBuilder.Entity("Api.Core.Entidades.CamaIndividual", b =>
                {
                    b.HasBaseType("Api.Core.Entidades.Cama");

                    b.Property<int>("HabitacionId")
                        .HasColumnName("Individual_HabitacionId")
                        .HasColumnType("int");

                    b.HasIndex("HabitacionId");

                    b.HasDiscriminator().HasValue("CamaIndividual");
                });

            modelBuilder.Entity("Api.Core.Entidades.CamaMatrimonial", b =>
                {
                    b.HasBaseType("Api.Core.Entidades.Cama");

                    b.Property<int>("HabitacionId")
                        .HasColumnName("Matrimonial_HabitacionId")
                        .HasColumnType("int");

                    b.HasIndex("HabitacionId");

                    b.HasDiscriminator().HasValue("CamaMatrimonial");
                });

            modelBuilder.Entity("Api.Core.Entidades.HabitacionCompartida", b =>
                {
                    b.HasBaseType("Api.Core.Entidades.Habitacion");

                    b.HasDiscriminator().HasValue("HabitacionCompartida");
                });

            modelBuilder.Entity("Api.Core.Entidades.HabitacionPrivada", b =>
                {
                    b.HasBaseType("Api.Core.Entidades.Habitacion");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue("HabitacionPrivada");
                });

            modelBuilder.Entity("Api.Core.Entidades.CamaCucheta", b =>
                {
                    b.HasOne("Api.Core.Entidades.Habitacion", "Habitacion")
                        .WithMany("CamasCuchetas")
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Entidades.Reserva", b =>
                {
                    b.HasOne("Api.Core.Entidades.Pasajero", "PasajeroTitular")
                        .WithMany()
                        .HasForeignKey("PasajeroTitularId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Entidades.ReservaCama", b =>
                {
                    b.HasOne("Api.Core.Entidades.Cama", "Cama")
                        .WithMany("ReservaCamas")
                        .HasForeignKey("CamaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Core.Entidades.Reserva", "Reserva")
                        .WithMany("ReservaCamas")
                        .HasForeignKey("ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Entidades.ReservaHabitacionPrivada", b =>
                {
                    b.HasOne("Api.Core.Entidades.HabitacionPrivada", "HabitacionPrivada")
                        .WithMany("ReservaHabitacionesPrivadas")
                        .HasForeignKey("HabitacionPrivadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Api.Core.Entidades.Reserva", "Reserva")
                        .WithMany("ReservaHabitacionesPrivadas")
                        .HasForeignKey("ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Entidades.CamaCuchetaDeAbajo", b =>
                {
                    b.HasOne("Api.Core.Entidades.CamaCucheta", "CamaCucheta")
                        .WithOne("Abajo")
                        .HasForeignKey("Api.Core.Entidades.CamaCuchetaDeAbajo", "CamaCuchetaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Entidades.CamaCuchetaDeArriba", b =>
                {
                    b.HasOne("Api.Core.Entidades.CamaCucheta", "CamaCucheta")
                        .WithOne("Arriba")
                        .HasForeignKey("Api.Core.Entidades.CamaCuchetaDeArriba", "CamaCuchetaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Entidades.CamaIndividual", b =>
                {
                    b.HasOne("Api.Core.Entidades.Habitacion", "Habitacion")
                        .WithMany("CamasIndividuales")
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Entidades.CamaMatrimonial", b =>
                {
                    b.HasOne("Api.Core.Entidades.Habitacion", "Habitacion")
                        .WithMany("CamasMatrimoniales")
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
