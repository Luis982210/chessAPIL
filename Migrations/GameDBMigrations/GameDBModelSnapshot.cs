﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using chessAPI.models;

#nullable disable

namespace chessAPI.Migrations.GameDBMigrations
{
    [DbContext(typeof(GameDB))]
    partial class GameDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("chessAPI.models.Juego", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("emailaway")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("emailhome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("idaway")
                        .HasColumnType("integer");

                    b.Property<int>("idhome")
                        .HasColumnType("integer");

                    b.Property<int>("punteoaway")
                        .HasColumnType("integer");

                    b.Property<int>("punteohome")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("partido");
                });
#pragma warning restore 612, 618
        }
    }
}