using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsfahanHC.Migrations
{
    public partial class seedandedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Stors",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "SImages",
                columns: new[] { "ImageId", "FileName" },
                values: new object[] { 1, "1.jpg" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DeleteTime", "Email", "IsAdmin", "IsDeleted", "Password", "RegisterTime", "UserName" },
                values: new object[] { 1, null, "amir@gmail.com", true, false, "e10adc3949ba59abbe56e057f20f883e", new DateTime(2021, 6, 3, 21, 35, 54, 58, DateTimeKind.Local).AddTicks(6782), "amirreza" });

            migrationBuilder.InsertData(
                table: "Stors",
                columns: new[] { "StorId", "ImageId", "Name" },
                values: new object[] { 1, 1, "فروشگاه من" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "StorId" },
                values: new object[] { 1, "این سرویس تابه از با کیفیت ترین مواد تشکیل دهنده ظروف طراحی و ساخت شده و با ورق های سه میلیمتری طراحی شده هست که بر خلاف سایر سرویس ها ضخامت بالایی دارد و مانع سوختن و از بین رفتن غذا می شود و به صورتی طراحی شده است که با کمترین روغن امکان پخت غذا هست ", "سرویس تابه 6 پارچه آرک مدل s500 ", 1 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "Price", "ProductId", "QuantityInStock" },
                values: new object[] { 1, 130000m, 1, 10 });

            migrationBuilder.InsertData(
                table: "PImages",
                columns: new[] { "ImageId", "FileName", "ProductId" },
                values: new object[] { 1, "1.jpg", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PImages",
                keyColumn: "ImageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stors",
                keyColumn: "StorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SImages",
                keyColumn: "ImageId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Stors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");
        }
    }
}
