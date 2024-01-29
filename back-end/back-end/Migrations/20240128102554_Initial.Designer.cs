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
    [Migration("20240128102554_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("BannedTopicEcosystem", b =>
                {
                    b.Property<string>("BannedTopicsTerm")
                        .HasColumnType("text");

                    b.Property<string>("EcosystemsId")
                        .HasColumnType("text");

                    b.HasKey("BannedTopicsTerm", "EcosystemsId");

                    b.HasIndex("EcosystemsId");

                    b.ToTable("BannedTopicEcosystem");
                });

            modelBuilder.Entity("EcosystemTaxonomy", b =>
                {
                    b.Property<string>("EcosystemsId")
                        .HasColumnType("text");

                    b.Property<string>("TaxonomyTerm")
                        .HasColumnType("text");

                    b.HasKey("EcosystemsId", "TaxonomyTerm");

                    b.HasIndex("TaxonomyTerm");

                    b.ToTable("EcosystemTaxonomy");
                });

            modelBuilder.Entity("EcosystemTechnology", b =>
                {
                    b.Property<string>("EcosystemsId")
                        .HasColumnType("text");

                    b.Property<string>("TechnologiesTerm")
                        .HasColumnType("text");

                    b.HasKey("EcosystemsId", "TechnologiesTerm");

                    b.HasIndex("TechnologiesTerm");

                    b.ToTable("EcosystemTechnology");
                });

            modelBuilder.Entity("EcosystemUser", b =>
                {
                    b.Property<string>("EcosystemsId")
                        .HasColumnType("text");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("EcosystemsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("EcosystemUser");
                });

            modelBuilder.Entity("SECODashBackend.Models.BannedTopic", b =>
                {
                    b.Property<string>("Term")
                        .HasColumnType("text");

                    b.HasKey("Term");

                    b.ToTable("BannedTopics");
                });

            modelBuilder.Entity("SECODashBackend.Models.Ecosystem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

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

            modelBuilder.Entity("SECODashBackend.Models.Taxonomy", b =>
                {
                    b.Property<string>("Term")
                        .HasColumnType("text");

                    b.HasKey("Term");

                    b.HasIndex("Term")
                        .IsUnique();

                    b.ToTable("Taxonomy");
                });

            modelBuilder.Entity("SECODashBackend.Models.Technology", b =>
                {
                    b.Property<string>("Term")
                        .HasColumnType("text");

                    b.HasKey("Term");

                    b.HasIndex("Term")
                        .IsUnique();

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("SECODashBackend.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BannedTopicEcosystem", b =>
                {
                    b.HasOne("SECODashBackend.Models.BannedTopic", null)
                        .WithMany()
                        .HasForeignKey("BannedTopicsTerm")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SECODashBackend.Models.Ecosystem", null)
                        .WithMany()
                        .HasForeignKey("EcosystemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EcosystemTaxonomy", b =>
                {
                    b.HasOne("SECODashBackend.Models.Ecosystem", null)
                        .WithMany()
                        .HasForeignKey("EcosystemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SECODashBackend.Models.Taxonomy", null)
                        .WithMany()
                        .HasForeignKey("TaxonomyTerm")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EcosystemTechnology", b =>
                {
                    b.HasOne("SECODashBackend.Models.Ecosystem", null)
                        .WithMany()
                        .HasForeignKey("EcosystemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SECODashBackend.Models.Technology", null)
                        .WithMany()
                        .HasForeignKey("TechnologiesTerm")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EcosystemUser", b =>
                {
                    b.HasOne("SECODashBackend.Models.Ecosystem", null)
                        .WithMany()
                        .HasForeignKey("EcosystemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SECODashBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
