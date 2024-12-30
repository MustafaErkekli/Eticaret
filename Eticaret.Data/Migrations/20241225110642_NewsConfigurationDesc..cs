using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewsConfigurationDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("054cdee3-fcda-4cad-aafa-4e6bf005f5f9"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "News",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(750)",
                oldMaxLength: 750,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreateDate", "Email", "IsActive", "IsAdmin", "Name", "Password", "Phone", "Surname", "UserGuid", "UserName" },
                values: new object[] { new Guid("d58aa4da-3164-41b1-b17b-7a2af9c578a4"), new DateTime(2024, 12, 25, 14, 6, 40, 783, DateTimeKind.Local).AddTicks(3876), "admineticaret.com", true, true, "Test", "123456*", null, "User", new Guid("cc8d50bd-14b6-475c-bb7b-09a8f450412a"), "Admin" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 25, 14, 6, 40, 783, DateTimeKind.Local).AddTicks(8310));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 25, 14, 6, 40, 783, DateTimeKind.Local).AddTicks(8320));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d58aa4da-3164-41b1-b17b-7a2af9c578a4"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "News",
                type: "nvarchar(750)",
                maxLength: 750,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreateDate", "Email", "IsActive", "IsAdmin", "Name", "Password", "Phone", "Surname", "UserGuid", "UserName" },
                values: new object[] { new Guid("054cdee3-fcda-4cad-aafa-4e6bf005f5f9"), new DateTime(2024, 12, 9, 14, 8, 54, 605, DateTimeKind.Local).AddTicks(2157), "admineticaret.com", true, true, "Test", "123456*", null, "User", new Guid("652b0c4d-2dc5-4faf-a590-5173ee3cbf2d"), "Admin" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 9, 14, 8, 54, 605, DateTimeKind.Local).AddTicks(6501));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 9, 14, 8, 54, 605, DateTimeKind.Local).AddTicks(6535));
        }
    }
}
