using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trader.Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBatchIdToTradeTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatchId",
                schema: "traderapi",
                table: "TradeTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BatchId", "TradeTime" },
                values: new object[] { 0, new DateTime(2026, 3, 15, 11, 26, 57, 480, DateTimeKind.Local).AddTicks(150) });

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BatchId", "TradeTime" },
                values: new object[] { 0, new DateTime(2026, 3, 15, 11, 26, 57, 480, DateTimeKind.Local).AddTicks(165) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchId",
                schema: "traderapi",
                table: "TradeTransactions");

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
    }
}
