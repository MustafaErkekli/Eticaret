using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnumOrderStateEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1001,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 1, 27, 14, 10, 36, 456, DateTimeKind.Local).AddTicks(8166), new Guid("abd66752-8dab-4e8d-8d5e-4df91f016840") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 1, 27, 14, 10, 36, 460, DateTimeKind.Local).AddTicks(1020));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 1, 27, 14, 10, 36, 460, DateTimeKind.Local).AddTicks(2366));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Orders");

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
    }
}
