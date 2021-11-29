using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsfahanHC.Migrations
{
    public partial class seedproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "StorId" },
                values: new object[] { 2, " این کیف پول ساخته شده از چرم طبیعی درجه یک میباشد که تولید آن کاملا ایرانی و دست دوز بوده و از بهترین یراق آلات موجود در بازار تهیه گردیده است. با داشتن این کیف چند منظوره میتوانید تمامی وسایل همراهتان نظیر کارت های اعتباری ، سیم کارت، پول و از همه مهمتر گوشی تلفن همراه خود را در کنار هم داشته باشید. همچنین به همراه این کیف یک عدد جاسویچی و جاکارتی نیز دریافت خواهید کرد. ", "ست کیف پول و جاکلیدی و جاکارتی چرمی کیهان کد K5 ", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterTime",
                value: new DateTime(2021, 6, 3, 23, 57, 31, 0, DateTimeKind.Local).AddTicks(4167));

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Price", "ProductId", "QuantityInStock" },
                values: new object[] { 2, 220000m, 2, 210 });

            migrationBuilder.InsertData(
                table: "PImages",
                columns: new[] { "ImageId", "FileName", "ProductId" },
                values: new object[,]
                {
                    { 2, "2.jpg", 2 },
                    { 3, "3.jpg", 2 },
                    { 4, "4.jpg", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PImages",
                keyColumn: "ImageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PImages",
                keyColumn: "ImageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PImages",
                keyColumn: "ImageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterTime",
                value: new DateTime(2021, 6, 3, 21, 35, 54, 58, DateTimeKind.Local).AddTicks(6782));
        }
    }
}
