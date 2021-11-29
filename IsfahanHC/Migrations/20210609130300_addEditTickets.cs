using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsfahanHC.Migrations
{
    public partial class addEditTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditTickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpirationTime = table.Column<DateTime>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    EditCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditTickets", x => x.TicketId);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterTime",
                value: new DateTime(2021, 6, 9, 17, 32, 59, 412, DateTimeKind.Local).AddTicks(1165));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditTickets");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterTime",
                value: new DateTime(2021, 6, 5, 21, 49, 19, 932, DateTimeKind.Local).AddTicks(7562));
        }
    }
}
