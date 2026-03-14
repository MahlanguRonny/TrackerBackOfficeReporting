using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trader.Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class TradeRateNewRecordInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "traderapi",
                table: "TradeRates",
                columns: new[] { "Id", "AsOfDate", "FromCurrency", "RateAmount", "ToCurrency" },
                values: new object[] { 2, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "USD", 0.917m, "EUR" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "traderapi",
                table: "TradeRates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TradeTime",
                value: new DateTime(2026, 3, 14, 18, 14, 42, 165, DateTimeKind.Local).AddTicks(4806));

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "TradeTime",
                value: new DateTime(2026, 3, 14, 18, 14, 42, 165, DateTimeKind.Local).AddTicks(4826));
        }
    }
}
