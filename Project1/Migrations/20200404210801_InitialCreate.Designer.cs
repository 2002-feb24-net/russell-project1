﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project1.Data;

namespace Project1.Migrations
{
    [DbContext(typeof(RGProject1Context))]
    [Migration("20200404210801_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project1.Models.Customers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardNumber")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("LastFirstName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Customers","p1");
                });

            modelBuilder.Entity("Project1.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductId");

                    b.ToTable("Inventory","p1");
                });

            modelBuilder.Entity("Project1.Models.Locations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Locations","p1");
                });

            modelBuilder.Entity("Project1.Models.OrderItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems","p1");
                });

            modelBuilder.Entity("Project1.Models.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<decimal>("Total")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LocationId");

                    b.ToTable("Orders","p1");
                });

            modelBuilder.Entity("Project1.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("Products","p1");
                });

            modelBuilder.Entity("Project1.Models.Inventory", b =>
                {
                    b.HasOne("Project1.Models.Locations", "Location")
                        .WithMany("Inventory")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK__Inventory__Locat__4F7CD00D")
                        .IsRequired();

                    b.HasOne("Project1.Models.Products", "Product")
                        .WithMany("Inventory")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__Inventory__Produ__5070F446")
                        .IsRequired();
                });

            modelBuilder.Entity("Project1.Models.OrderItems", b =>
                {
                    b.HasOne("Project1.Models.Orders", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderItem__Order__5CD6CB2B")
                        .IsRequired();

                    b.HasOne("Project1.Models.Products", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__OrderItem__Produ__5DCAEF64")
                        .IsRequired();
                });

            modelBuilder.Entity("Project1.Models.Orders", b =>
                {
                    b.HasOne("Project1.Models.Customers", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__Orders__Customer__5441852A")
                        .IsRequired();

                    b.HasOne("Project1.Models.Locations", "Location")
                        .WithMany("Orders")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK__Orders__Location__534D60F1")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
