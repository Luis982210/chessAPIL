﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using chessAPI.models;

#nullable disable

namespace chessAPI.Migrations.TeamDbMigrations
{
    [DbContext(typeof(TeamDb))]
    [Migration("20230204003413_hola")]
    partial class hola
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("chessAPI.models.Equipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("email1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email3")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email4")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("idjugador1")
                        .HasColumnType("integer");

                    b.Property<int>("idjugador2")
                        .HasColumnType("integer");

                    b.Property<int>("idjugador3")
                        .HasColumnType("integer");

                    b.Property<int>("idjugador4")
                        .HasColumnType("integer");

                    b.Property<int>("punteoequipo1")
                        .HasColumnType("integer");

                    b.Property<int>("punteoequipo2")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Equipos");
                });
#pragma warning restore 612, 618
        }
    }
}
