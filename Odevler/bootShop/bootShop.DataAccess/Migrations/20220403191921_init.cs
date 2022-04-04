using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bootShop.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Telefonlar" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Bilgisayarlar" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Konsollar" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "Discount", "ImageUrl", "ModifiedDate", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 4, 3, 22, 19, 20, 885, DateTimeKind.Local).AddTicks(4139), null, 0.14999999999999999, "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp", null, "IPhone 13", 1200.0 },
                    { 2, 1, new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3515), null, 0.14999999999999999, "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp", null, "Samsung S22", 1200.0 },
                    { 3, 1, new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3555), null, 0.14999999999999999, "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp", null, "Xiaomi", 1200.0 },
                    { 4, 2, new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3559), null, 0.14999999999999999, "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp", null, "Apple Mac", 2000.0 },
                    { 5, 2, new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3561), null, 0.14999999999999999, "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp", null, "Lenovo", 1200.0 },
                    { 6, 3, new DateTime(2022, 4, 3, 22, 19, 20, 887, DateTimeKind.Local).AddTicks(3564), null, 0.14999999999999999, "https://productimages.hepsiburada.net/s/49/550/10986386784306.jpg/format:webp", null, "XBox", 1200.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
