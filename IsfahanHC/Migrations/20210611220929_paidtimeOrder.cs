using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsfahanHC.Migrations
{
    public partial class paidtimeOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaidTime",
                table: "Orders",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterTime",
                value: new DateTime(2021, 6, 12, 2, 39, 29, 338, DateTimeKind.Local).AddTicks(9053));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidTime",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterTime",
                value: new DateTime(2021, 6, 9, 17, 32, 59, 412, DateTimeKind.Local).AddTicks(1165));
        }
    }
}
