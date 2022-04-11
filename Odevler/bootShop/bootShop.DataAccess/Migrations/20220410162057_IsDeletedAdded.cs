using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bootShop.DataAccess.Migrations
{
    public partial class IsDeletedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 10, 19, 20, 57, 373, DateTimeKind.Local).AddTicks(6844));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 10, 19, 20, 57, 374, DateTimeKind.Local).AddTicks(8302));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 10, 19, 20, 57, 374, DateTimeKind.Local).AddTicks(8323));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 10, 19, 20, 57, 374, DateTimeKind.Local).AddTicks(8326));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 10, 19, 20, 57, 374, DateTimeKind.Local).AddTicks(8328));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 10, 19, 20, 57, 374, DateTimeKind.Local).AddTicks(8332));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 3, 22, 19, 20, 885, DateTimeKind.Local).AddTicks(4139));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3515));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3555));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3559));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3561));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3564));
        }
    }
}
