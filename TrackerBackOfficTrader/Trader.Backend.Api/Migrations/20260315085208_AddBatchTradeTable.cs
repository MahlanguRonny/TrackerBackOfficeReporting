using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trader.Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBatchTradeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeBatches",
                schema: "traderapi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeBatches", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "traderapi",
                table: "TradeBatches",
                columns: new[] { "Id", "DateCreated" },
                values: new object[] { 1, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TradeTime",
                value: new DateTime(2026, 3, 15, 10, 52, 7, 669, DateTimeKind.Local).AddTicks(7504));

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "TradeTime",
                value: new DateTime(2026, 3, 15, 10, 52, 7, 669, DateTimeKind.Local).AddTicks(7518));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeBatches",
                schema: "traderapi");

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TradeTime",
                value: new DateTime(2026, 3, 14, 18, 52, 6, 294, DateTimeKind.Local).AddTicks(2307));

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "TradeTime",
                value: new DateTime(2026, 3, 14, 18, 52, 6, 294, DateTimeKind.Local).AddTicks(2327));
        }
    }
}
