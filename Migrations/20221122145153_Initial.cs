using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmerApp.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Size = table.Column<float>(type: "real", nullable: false),
                    Seson = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpendTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpendTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    MeasurementUnitId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_MeasurementUnits_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalTable: "MeasurementUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseReceptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invoice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MeasurementUnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseReceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseReceptions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalFromWarehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalFromWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WithdrawalFromWarehouses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    SpendTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    WithdrawalFromWarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spends_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spends_SpendTypes_SpendTypeId",
                        column: x => x.SpendTypeId,
                        principalTable: "SpendTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spends_WithdrawalFromWarehouses_WithdrawalFromWarehouseId",
                        column: x => x.WithdrawalFromWarehouseId,
                        principalTable: "WithdrawalFromWarehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "MeasurementUnits",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Кілограми" },
                    { 2, "Літри" },
                    { 3, "Тони" }
                });

            migrationBuilder.InsertData(
                table: "OwnResources",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Вода" },
                    { 2, "Зерно" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Мінеральні добрива" },
                    { 2, "Засоби захисту" },
                    { 3, "Насіння" },
                    { 4, "Паливо" },
                    { 5, "інше" }
                });

            migrationBuilder.InsertData(
                table: "SpendTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Продукти із складу" },
                    { 2, "Власні русурси" },
                    { 3, "Зарплати працівникам" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_MeasurementUnitId",
                table: "Products",
                column: "MeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Spends_LocationId",
                table: "Spends",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Spends_SpendTypeId",
                table: "Spends",
                column: "SpendTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Spends_WithdrawalFromWarehouseId",
                table: "Spends",
                column: "WithdrawalFromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseReceptions_ProductId",
                table: "WarehouseReceptions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_ProductId",
                table: "Warehouses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalFromWarehouses_ProductId",
                table: "WithdrawalFromWarehouses",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnResources");

            migrationBuilder.DropTable(
                name: "Spends");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WarehouseReceptions");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "SpendTypes");

            migrationBuilder.DropTable(
                name: "WithdrawalFromWarehouses");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "MeasurementUnits");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
