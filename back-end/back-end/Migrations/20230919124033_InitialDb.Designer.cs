﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SECODashBackend.Database;

#nullable disable

namespace SECODashBackend.Migrations
{
    [DbContext(typeof(EcosystemsContext))]
    [Migration("20230919124033_InitialDb")]
    partial class InitialDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("SECODashBackend.Models.Ecosystem", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int?>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("NumberOfStars")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Ecosystems");
                });

            modelBuilder.Entity("SECODashBackend.Models.Project", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<long?>("Id"));

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<int?>("EcosystemId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("NumberOfStars")
                        .HasColumnType("integer");

                    b.Property<string>("Owner")
                        .HasColumnType("text");

                    b.Property<string>("ReadMe")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EcosystemId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("SECODashBackend.Models.Project", b =>
                {
                    b.HasOne("SECODashBackend.Models.Ecosystem", null)
                        .WithMany("Projects")
                        .HasForeignKey("EcosystemId");
                });

            modelBuilder.Entity("SECODashBackend.Models.Ecosystem", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
