using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class changeLimittypetofloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Limit",
                table: "PlanService",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 10, 10, 29, 56, 779, DateTimeKind.Local).AddTicks(456));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 10, 10, 29, 56, 782, DateTimeKind.Local).AddTicks(8199));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8c741115-1956-4b7a-80ec-41640a18ad3e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "eee2358e-d589-4897-aab0-ef75d3898bd5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "525505f2-c95a-417d-938b-a262de92d383");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "0157c8f4-3381-4258-baa5-e6983fa59a29");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1f45b44-0443-46f8-b457-34de2c868cb1", "AQAAAAEAACcQAAAAEArLm2+2hZhfI6gEbssFFqe3qr98eX92YeEEDAO7615dDU7aQZu6HUUu4FU9nGQyaA==", "73ff4d31-18c7-4465-b219-97c4d7f3b1fb" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9aa2fd6-cda9-44cc-81d2-2f845028f3b3", "AQAAAAEAACcQAAAAECJTNOl8Rz4x7dHxds8MkDyLCdNAa2146iObid2lJMaxLSOxI51h7I9xiyEHsvOm3w==", "aa815dcb-edf7-48a8-8530-fd28aa8463c4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52e89a49-5621-42e1-92d7-f52a94cf0b02", "AQAAAAEAACcQAAAAENt9PzhDETnYOskyXp1UDhLts3LTtALNyJtDazJem//5IoKhqjQq5SJVw30NGMyyww==", "c2b3cc71-0494-4be1-a4dd-838f813a9d09" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Limit",
                table: "PlanService",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 10, 8, 10, 43, 240, DateTimeKind.Local).AddTicks(1843));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 10, 8, 10, 43, 244, DateTimeKind.Local).AddTicks(5983));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5958c928-71fa-484c-aa56-c0bdaa085c72");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a585a815-f3d4-4c04-82cc-f0e28d241095");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "30537be8-63f0-46ae-8a6c-4da923277db4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "dee7c4f4-f1bb-4b81-b554-9e741fb79ebe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c9f6fda-2e8e-46ef-bb9c-fa59f4ccea47", "AQAAAAEAACcQAAAAEB9a9VhtxZsRzpOS0LGqj6cnRlnYAq+prFati42YE2xZUIJM9TZXiPr/FWFvKXJrDQ==", "35c3c158-2dda-455b-9e3c-e5e7929d4d9c" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af174bd0-8ddc-4f30-b3e0-4139b90e9452", "AQAAAAEAACcQAAAAELWsaKxaOr3vKV+LxPAv8bvdrQk7CFdh77yLFaSywVcbk+IoNOhvepj7Wt5p9WKStQ==", "d2106411-0b39-404c-b562-50396404d24a" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc85e536-ab68-4887-910d-9ad892097e39", "AQAAAAEAACcQAAAAEK/kp+c3SIMMV0UEYmVMS6uQOJ/fQYvVX4Bz6AIdALhWPHLt0lgbEctI/qHJqyoxCQ==", "a3104bf8-60fe-4218-b311-c50621e0f6be" });
        }
    }
}
