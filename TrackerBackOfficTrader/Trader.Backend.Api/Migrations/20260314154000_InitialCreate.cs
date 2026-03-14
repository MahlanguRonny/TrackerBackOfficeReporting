using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Trader.Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "traderapi");

            migrationBuilder.CreateTable(
                name: "TradeAccounts",
                schema: "traderapi",
                columns: table => new
                {
                    TradeAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeAccounts", x => x.TradeAccountId);
                });

            migrationBuilder.CreateTable(
                name: "TradeRates",
                schema: "traderapi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCurrency = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ToCurrency = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    RateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AsOfDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeTransactions",
                schema: "traderapi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeAccountId = table.Column<int>(type: "int", nullable: false),
                    TradeRateId = table.Column<int>(type: "int", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Side = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TradeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeTransactions_TradeAccounts_TradeAccountId",
                        column: x => x.TradeAccountId,
                        principalSchema: "traderapi",
                        principalTable: "TradeAccounts",
                        principalColumn: "TradeAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "traderapi",
                table: "TradeAccounts",
                columns: new[] { "TradeAccountId", "Name" },
                values: new object[] { 1, "T-001" });

            migrationBuilder.InsertData(
                schema: "traderapi",
                table: "TradeRates",
                columns: new[] { "Id", "AsOfDate", "FromCurrency", "RateAmount", "ToCurrency" },
                values: new object[] { 1, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "EUR", 1.09m, "USD" });

            migrationBuilder.InsertData(
                schema: "traderapi",
                table: "TradeTransactions",
                columns: new[] { "Id", "Currency", "Price", "Quantity", "Side", "Symbol", "TradeAccountId", "TradeRateId", "TradeTime" },
                values: new object[,]
                {
                    { 1, "USD", 100m, 100, "Side1", "Symbol1", 1, 1, new DateTime(2026, 3, 14, 17, 39, 59, 720, DateTimeKind.Local).AddTicks(5189) },
                    { 2, "USD", 250m, 50, "Side2", "Symbol2", 1, 1, new DateTime(2026, 3, 14, 17, 39, 59, 720, DateTimeKind.Local).AddTicks(5211) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeTransactions_TradeAccountId",
                schema: "traderapi",
                table: "TradeTransactions",
                column: "TradeAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeRates",
                schema: "traderapi");

            migrationBuilder.DropTable(
                name: "TradeTransactions",
                schema: "traderapi");

            migrationBuilder.DropTable(
                name: "TradeAccounts",
                schema: "traderapi");
        }
    }
}
