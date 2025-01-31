using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class migUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 1, 27, 12, 31, 36, 407, DateTimeKind.Local).AddTicks(8499), new Guid("16a553cc-3fa6-4517-8161-44b1c5da415d") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 1, 27, 12, 31, 36, 410, DateTimeKind.Local).AddTicks(6184));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 1, 27, 12, 31, 36, 410, DateTimeKind.Local).AddTicks(7530));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 1, 27, 12, 26, 15, 34, DateTimeKind.Local).AddTicks(1415), new Guid("75136c6c-4da2-42fc-ae54-192c580bd93c") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 1, 27, 12, 26, 15, 36, DateTimeKind.Local).AddTicks(7936));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 1, 27, 12, 26, 15, 36, DateTimeKind.Local).AddTicks(9335));
        }
    }
}
