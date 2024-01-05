﻿// <auto-generated />
using System;
using BurgerRoyale.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BurgerRoyale.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BurgerRoyale.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PaymentRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BurgerRoyale.Domain.Entities.OrderProduct", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("BurgerRoyale.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2e168b17-5e08-4049-bcbd-248ced33f901"),
                            Category = 0,
                            Description = "Hambúrguer de carne bovina.",
                            Name = "Burger Tradicional",
                            Price = 19.2m
                        },
                        new
                        {
                            Id = new Guid("c59afddd-23ad-4e56-bc48-c6b76a2813bf"),
                            Category = 0,
                            Description = "Hambúrguer de carne bovina com o dobro de bacon.",
                            Name = "Burger Duplo Bacon",
                            Price = 22.9m
                        },
                        new
                        {
                            Id = new Guid("ad7bc119-0b84-48fe-9d4b-943c1598263f"),
                            Category = 0,
                            Description = "Hambúrguer de carne bovina com o dobro de cheddar.",
                            Name = "Burger Duplo Cheddar",
                            Price = 23.9m
                        },
                        new
                        {
                            Id = new Guid("0cbf684b-be68-4a28-bd57-77ac9066ce55"),
                            Category = 1,
                            Description = "Porção de fritas pequena.",
                            Name = "Fritas Pequena",
                            Price = 4.9m
                        },
                        new
                        {
                            Id = new Guid("d8d71926-fa7b-4381-ac1f-23e4fe76d1f6"),
                            Category = 1,
                            Description = "Porção de fritas.",
                            Name = "Fritas",
                            Price = 6.9m
                        },
                        new
                        {
                            Id = new Guid("6668ab9b-b0e1-448d-80a5-ec9abf7bed36"),
                            Category = 1,
                            Description = "Porção de fritas grande.",
                            Name = "Fritas Grande",
                            Price = 8.9m
                        },
                        new
                        {
                            Id = new Guid("98f1d726-6650-490d-9b0e-82c2a773210f"),
                            Category = 2,
                            Description = "500 ml com ou sem gás",
                            Name = "Água",
                            Price = 4m
                        },
                        new
                        {
                            Id = new Guid("9344aef2-5f25-45d3-9fc1-76e70c432ebf"),
                            Category = 2,
                            Description = "Copo 400 ml",
                            Name = "Refrigerante",
                            Price = 6m
                        },
                        new
                        {
                            Id = new Guid("40d5632e-83c5-4cad-afe8-a0a5ef6b21aa"),
                            Category = 3,
                            Description = "Sundae de diversos sabores",
                            Name = "Sundae",
                            Price = 7m
                        },
                        new
                        {
                            Id = new Guid("3453ea67-2380-4d99-b6fb-07c2f1f39439"),
                            Category = 3,
                            Description = "Sorvete de diversos sabores",
                            Name = "Sorvete",
                            Price = 7m
                        });
                });

            modelBuilder.Entity("BurgerRoyale.Domain.Entities.ProductImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("BurgerRoyale.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BurgerRoyale.Domain.Entities.OrderProduct", b =>
                {
                    b.HasOne("BurgerRoyale.Domain.Entities.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BurgerRoyale.Domain.Entities.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BurgerRoyale.Domain.Entities.ProductImage", b =>
                {
                    b.HasOne("BurgerRoyale.Domain.Entities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BurgerRoyale.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("BurgerRoyale.Domain.Entities.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
