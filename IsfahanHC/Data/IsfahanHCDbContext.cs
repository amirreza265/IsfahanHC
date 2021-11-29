using IsfahanHC.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace IsfahanHC.Data
{
    public class IsfahanHCDbContext : DbContext
    {
        public IsfahanHCDbContext(DbContextOptions<IsfahanHCDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Stor> Stors  { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Item> Items  { get; set; }
        public DbSet<PImage> PImages  { get; set; }
        public DbSet<SImage> SImages { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<EditTicket> EditTickets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seed data
            modelBuilder.Entity<User>().HasData(new User {
            UserName = "amirreza",
            Password = HashString.GetHashString("123456"),
            IsAdmin = true,
            Email = "amir@gmail.com",
            RegisterTime = DateTime.Now,
            IsDeleted = false,
            UserId=1
            });

            modelBuilder.Entity<SImage>().HasData(new SImage
            {
                ImageId = 1,
                FileName = "1.jpg"
            });

            modelBuilder.Entity<Stor>().HasData(new Stor() { 
            StorId = 1,
            Name = "فروشگاه من",
            ImageId = 1
            });

            modelBuilder.Entity<Product>().HasData(new Product()
            {
                ProductId = 1,
                StorId = 1,
                Name = "سرویس تابه 6 پارچه آرک مدل s500 ",
                Description = "این سرویس تابه از با کیفیت ترین مواد تشکیل دهنده ظروف طراحی و ساخت شده و با ورق های سه میلیمتری طراحی شده هست که بر خلاف سایر سرویس ها ضخامت بالایی دارد و مانع سوختن و از بین رفتن غذا می شود و به صورتی طراحی شده است که با کمترین روغن امکان پخت غذا هست ",
            },
            new Product
            {
                ProductId = 2,
                Name = "ست کیف پول و جاکلیدی و جاکارتی چرمی کیهان کد K5 ",
                StorId = 1,
                Description = " این کیف پول ساخته شده از چرم طبیعی درجه یک میباشد که تولید آن کاملا ایرانی و دست دوز بوده و از بهترین یراق آلات موجود در بازار تهیه گردیده است. با داشتن این کیف چند منظوره میتوانید تمامی وسایل همراهتان نظیر کارت های اعتباری ، سیم کارت، پول و از همه مهمتر گوشی تلفن همراه خود را در کنار هم داشته باشید. همچنین به همراه این کیف یک عدد جاسویچی و جاکارتی نیز دریافت خواهید کرد. "
            }
            );
            modelBuilder.Entity<PImage>().HasData(
                new PImage
                {
                    ImageId = 1,
                    FileName = "1.jpg",
                    ProductId = 1
                }, 
                new PImage
                {
                    ImageId = 2,
                    FileName = "2.jpg",
                    ProductId = 2
                },
                new PImage
                {
                    ImageId = 3,
                    FileName = "3.jpg",
                    ProductId = 2
                },
                new PImage
                {
                    ImageId = 4,
                    FileName = "4.jpg",
                    ProductId = 2
                }

                );

            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    QuantityInStock = 10,
                    ItemId = 1,
                    Price = 130000M,
                    ProductId = 1
                },
                new Item()
                {
                    QuantityInStock = 210,
                    ItemId = 2,
                    Price = 220000M,
                    ProductId = 2
                }
            );

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
