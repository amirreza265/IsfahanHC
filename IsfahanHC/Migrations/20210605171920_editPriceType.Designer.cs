// <auto-generated />
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
    [Migration("20210605171920_editPriceType")]
    partial class editPriceType
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

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            Price = 130000m,
                            ProductId = 1,
                            QuantityInStock = 10
                        },
                        new
                        {
                            ItemId = 2,
                            Price = 220000m,
                            ProductId = 2,
                            QuantityInStock = 210
                        });
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("money");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
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

                    b.HasData(
                        new
                        {
                            ImageId = 1,
                            FileName = "1.jpg",
                            ProductId = 1
                        },
                        new
                        {
                            ImageId = 2,
                            FileName = "2.jpg",
                            ProductId = 2
                        },
                        new
                        {
                            ImageId = 3,
                            FileName = "3.jpg",
                            ProductId = 2
                        },
                        new
                        {
                            ImageId = 4,
                            FileName = "4.jpg",
                            ProductId = 2
                        });
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StorId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("StorId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Description = "این سرویس تابه از با کیفیت ترین مواد تشکیل دهنده ظروف طراحی و ساخت شده و با ورق های سه میلیمتری طراحی شده هست که بر خلاف سایر سرویس ها ضخامت بالایی دارد و مانع سوختن و از بین رفتن غذا می شود و به صورتی طراحی شده است که با کمترین روغن امکان پخت غذا هست ",
                            Name = "سرویس تابه 6 پارچه آرک مدل s500 ",
                            StorId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            Description = " این کیف پول ساخته شده از چرم طبیعی درجه یک میباشد که تولید آن کاملا ایرانی و دست دوز بوده و از بهترین یراق آلات موجود در بازار تهیه گردیده است. با داشتن این کیف چند منظوره میتوانید تمامی وسایل همراهتان نظیر کارت های اعتباری ، سیم کارت، پول و از همه مهمتر گوشی تلفن همراه خود را در کنار هم داشته باشید. همچنین به همراه این کیف یک عدد جاسویچی و جاکارتی نیز دریافت خواهید کرد. ",
                            Name = "ست کیف پول و جاکلیدی و جاکارتی چرمی کیهان کد K5 ",
                            StorId = 1
                        });
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

                    b.HasData(
                        new
                        {
                            ImageId = 1,
                            FileName = "1.jpg"
                        });
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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StorId");

                    b.HasIndex("ImageId")
                        .IsUnique();

                    b.ToTable("Stors");

                    b.HasData(
                        new
                        {
                            StorId = 1,
                            ImageId = 1,
                            Name = "فروشگاه من"
                        });
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

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "amir@gmail.com",
                            IsAdmin = true,
                            IsDeleted = false,
                            Password = "e10adc3949ba59abbe56e057f20f883e",
                            RegisterTime = new DateTime(2021, 6, 5, 21, 49, 19, 932, DateTimeKind.Local).AddTicks(7562),
                            UserName = "amirreza"
                        });
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Item", b =>
                {
                    b.HasOne("IsfahanHC.Models.DataModels.Product", "Product")
                        .WithOne("Item")
                        .HasForeignKey("IsfahanHC.Models.DataModels.Item", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.Order", b =>
                {
                    b.HasOne("IsfahanHC.Models.DataModels.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IsfahanHC.Models.DataModels.OrderItem", b =>
                {
                    b.HasOne("IsfahanHC.Models.DataModels.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IsfahanHC.Models.DataModels.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
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
