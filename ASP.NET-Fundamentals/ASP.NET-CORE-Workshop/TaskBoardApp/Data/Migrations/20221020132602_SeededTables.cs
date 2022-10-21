using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class SeededTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f7756144-e1b3-4a25-be4e-98553f343ef7", 0, "553370ef-ac96-4ace-b846-a18e9c4dd229", "guest@mail.com", false, "Guest", "User", false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAEP9wvVkqR2FTo0wHg+aZZpmwALeJPctxy7fwXF+VUIS7y+Oo9G9Rz/e2prDXkQhlGQ==", null, false, "ae1501e1-5f5a-4af7-ab9a-bc5a1556eb77", false, "guest" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 9, 20, 16, 26, 2, 271, DateTimeKind.Local).AddTicks(5081), "Learn using ASP.NET Core Identity", "f7756144-e1b3-4a25-be4e-98553f343ef7", "Prepare for ASP.NET Fundamentals exam" },
                    { 2, 3, new DateTime(2022, 5, 20, 16, 26, 2, 271, DateTimeKind.Local).AddTicks(5208), "Learn using EF Core and MS SQL Server Management Studio", "f7756144-e1b3-4a25-be4e-98553f343ef7", "Improve EF Core skills" },
                    { 3, 2, new DateTime(2022, 10, 10, 16, 26, 2, 271, DateTimeKind.Local).AddTicks(5213), "Learn using ASP.NET Core Identity", "f7756144-e1b3-4a25-be4e-98553f343ef7", "Improve ASP.NET Core skills" },
                    { 4, 3, new DateTime(2021, 10, 20, 16, 26, 2, 271, DateTimeKind.Local).AddTicks(5222), "Prepare by solving old Mid and Final exams", "f7756144-e1b3-4a25-be4e-98553f343ef7", "Prepare C# Fundamentals exam" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f7756144-e1b3-4a25-be4e-98553f343ef7");

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
