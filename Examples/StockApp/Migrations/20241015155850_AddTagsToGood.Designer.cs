﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StockApp;

#nullable disable

namespace StockApp.Migrations
{
    [DbContext(typeof(StockDbContext))]
    [Migration("20241015155850_AddTagsToGood")]
    partial class AddTagsToGood
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("stock")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StockAppCore.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries", "stock");
                });

            modelBuilder.Entity("StockAppCore.Models.Good", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("Tags")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<int>("UnitId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("UnitId");

                    b.ToTable("Goods", "stock");
                });

            modelBuilder.Entity("StockAppCore.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Manufacturers", "stock");
                });

            modelBuilder.Entity("StockAppCore.Models.MoveDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DestStorageId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SourceStorageId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("MoveDocuments", "stock");
                });

            modelBuilder.Entity("StockAppCore.Models.MoveDocumentItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Count")
                        .HasColumnType("numeric");

                    b.Property<int>("GoodId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("MoveDocumentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GoodId");

                    b.HasIndex("MoveDocumentId");

                    b.ToTable("MoveDocumentItems", "stock");
                });

            modelBuilder.Entity("StockAppCore.Models.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Storages", "stock");
                });

            modelBuilder.Entity("StockAppCore.Models.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Units", "stock");
                });

            modelBuilder.Entity("StockAppCore.Models.Good", b =>
                {
                    b.HasOne("StockAppCore.Models.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockAppCore.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("StockAppCore.Models.Manufacturer", b =>
                {
                    b.HasOne("StockAppCore.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("StockAppCore.Models.MoveDocumentItem", b =>
                {
                    b.HasOne("StockAppCore.Models.Good", "Good")
                        .WithMany()
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StockAppCore.Models.MoveDocument", null)
                        .WithMany("Items")
                        .HasForeignKey("MoveDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Good");
                });

            modelBuilder.Entity("StockAppCore.Models.MoveDocument", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
