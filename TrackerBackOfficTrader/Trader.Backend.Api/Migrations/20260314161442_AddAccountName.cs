using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trader.Backend.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Account",
                schema: "traderapi",
                table: "TradeTransactions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Account", "TradeTime" },
                values: new object[] { "ACC-123", new DateTime(2026, 3, 14, 18, 14, 42, 165, DateTimeKind.Local).AddTicks(4806) });

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Account", "TradeTime" },
                values: new object[] { "ACC-123", new DateTime(2026, 3, 14, 18, 14, 42, 165, DateTimeKind.Local).AddTicks(4826) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account",
                schema: "traderapi",
                table: "TradeTransactions");

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TradeTime",
                value: new DateTime(2026, 3, 14, 17, 39, 59, 720, DateTimeKind.Local).AddTicks(5189));

            migrationBuilder.UpdateData(
                schema: "traderapi",
                table: "TradeTransactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "TradeTime",
                value: new DateTime(2026, 3, 14, 17, 39, 59, 720, DateTimeKind.Local).AddTicks(5211));
        }
    }
}
