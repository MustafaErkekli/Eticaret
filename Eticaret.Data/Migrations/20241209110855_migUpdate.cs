using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class migUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("533b755b-5c54-4f7d-aec3-0eea3fb1ddee"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("991adcb1-cfaa-45fd-be70-8c5ff84a0b70"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("054cdee3-fcda-4cad-aafa-4e6bf005f5f9"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreateDate", "Email", "IsActive", "IsAdmin", "Name", "Password", "Phone", "Surname", "UserGuid", "UserName" },
                values: new object[,]
                {
                    { new Guid("533b755b-5c54-4f7d-aec3-0eea3fb1ddee"), new DateTime(2024, 12, 7, 14, 7, 41, 145, DateTimeKind.Local).AddTicks(6470), "admineticaret.com", true, true, "Test", "123456*", null, "User", new Guid("02d0618c-d94a-4dfe-8070-dd8daabe466f"), "Admin" },
                    { new Guid("991adcb1-cfaa-45fd-be70-8c5ff84a0b70"), new DateTime(2024, 12, 7, 14, 7, 41, 145, DateTimeKind.Local).AddTicks(3107), "admineticaret.com", true, true, "Test", "123456*", null, "User", new Guid("91ab2b4b-fa48-4975-a012-0f7e39286632"), "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 7, 14, 7, 41, 145, DateTimeKind.Local).AddTicks(9155));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 7, 14, 7, 41, 145, DateTimeKind.Local).AddTicks(9162));
        }
    }
}
