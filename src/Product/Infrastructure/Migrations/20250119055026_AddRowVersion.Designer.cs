﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SaBooBo.Product.Infrastructure;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ProductAppContext))]
    [Migration("20250119055026_AddRowVersion")]
    partial class AddRowVersion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("SaBooBo")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SaBooBo.Product.Domain.AggregatesModel.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2025, 1, 19, 5, 50, 26, 69, DateTimeKind.Utc).AddTicks(4240))
                        .HasColumnOrder(6);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnOrder(4);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnOrder(1);

                    b.Property<long>("Price")
                        .HasColumnType("bigint")
                        .HasColumnOrder(3);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<string>("Sku")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnOrder(2);

                    b.Property<string>("UrlImage")
                        .HasColumnType("text")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.ToTable("Product", "SaBooBo");
                });

            modelBuilder.Entity("SaBooBo.Product.Domain.AggregatesModel.Product", b =>
                {
                    b.OwnsMany("SaBooBo.Product.Domain.AggregatesModel.Topping", "Toppings", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("CreatedDate")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("timestamp with time zone")
                                .HasDefaultValue(new DateTime(2025, 1, 19, 5, 50, 26, 89, DateTimeKind.Utc).AddTicks(2440));

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("character varying(500)");

                            b1.Property<long>("Price")
                                .HasColumnType("bigint");

                            b1.Property<byte[]>("RowVersion")
                                .IsConcurrencyToken()
                                .ValueGeneratedOnAddOrUpdate()
                                .HasColumnType("bytea");

                            b1.HasKey("Id", "ProductId");

                            b1.HasIndex("ProductId");

                            b1.ToTable("Topping", "SaBooBo");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Toppings");
                });
#pragma warning restore 612, 618
        }
    }
}
