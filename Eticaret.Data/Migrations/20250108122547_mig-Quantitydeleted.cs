using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class migQuantitydeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d9d58dc1-afe1-4325-9c28-2e1fb570c5d1"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreateDate", "Email", "IsActive", "IsAdmin", "Name", "Password", "Phone", "Surname", "UserGuid", "UserName" },
                values: new object[] { new Guid("180ab7f7-de83-44c5-8166-9fb6c9f4c24c"), new DateTime(2025, 1, 8, 15, 25, 46, 289, DateTimeKind.Local).AddTicks(8215), "admineticaret.com", true, true, "Test", "123456*", null, "User", new Guid("f9eb5b50-e0d0-4a85-8723-f1352bc98a04"), "Admin" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 1, 8, 15, 25, 46, 290, DateTimeKind.Local).AddTicks(1035));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 1, 8, 15, 25, 46, 290, DateTimeKind.Local).AddTicks(1043));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("180ab7f7-de83-44c5-8166-9fb6c9f4c24c"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreateDate", "Email", "IsActive", "IsAdmin", "Name", "Password", "Phone", "Surname", "UserGuid", "UserName" },
                values: new object[] { new Guid("d9d58dc1-afe1-4325-9c28-2e1fb570c5d1"), new DateTime(2025, 1, 8, 15, 22, 55, 528, DateTimeKind.Local).AddTicks(7801), "admineticaret.com", true, true, "Test", "123456*", null, "User", new Guid("fbc7ada1-fe9a-4821-94e4-6a66a16c5f5d"), "Admin" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 1, 8, 15, 22, 55, 529, DateTimeKind.Local).AddTicks(728));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 1, 8, 15, 22, 55, 529, DateTimeKind.Local).AddTicks(735));
        }
    }
}
