﻿// <auto-generated />
using System;
using IsfahanHC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IsfahanHC.Migrations
{
    [DbContext(typeof(IsfahanHCDbContext))]
    [Migration("20210603084807_changeuserdltTime")]
    partial class changeuserdltTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.PImage", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("PImages");
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StorId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("StorId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.SImage", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("SImages");
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Seller", b =>
                {
                    b.Property<int>("SellerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StorId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SellerId");

                    b.HasIndex("StorId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Stor", b =>
                {
                    b.Property<int>("StorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.HasKey("StorId");

                    b.HasIndex("ImageId")
                        .IsUnique();

                    b.ToTable("Stors");
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<DateTime>("RegisterTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Item", b =>
                {
                    b.HasOne("IsfahanHC.Models.DataModels.Product", "Product")
                        .WithOne("Item")
                        .HasForeignKey("IsfahanHC.Models.DataModels.Item", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.PImage", b =>
                {
                    b.HasOne("IsfahanHC.Models.DataModels.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Product", b =>
                {
                    b.HasOne("IsfahanHC.Models.DataModels.Stor", "Stor")
                        .WithMany("Products")
                        .HasForeignKey("StorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Seller", b =>
                {
                    b.HasOne("IsfahanHC.Models.DataModels.Stor", "Stor")
                        .WithMany("Sellers")
                        .HasForeignKey("StorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IsfahanHC.Models.DataModels.User", "User")
                        .WithOne("Seller")
                        .HasForeignKey("IsfahanHC.Models.DataModels.Seller", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Stor", b =>
                {
                    b.HasOne("IsfahanHC.Models.DataModels.SImage", "Image")
                        .WithOne("Stor")
                        .HasForeignKey("IsfahanHC.Models.DataModels.Stor", "ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
