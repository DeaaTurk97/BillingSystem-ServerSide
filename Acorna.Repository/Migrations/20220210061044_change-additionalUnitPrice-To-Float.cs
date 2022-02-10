using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class changeadditionalUnitPriceToFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "AdditionalUnitPrice",
                table: "PlanService",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AdditionalUnitPrice",
                table: "PlanService",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 9, 17, 12, 51, 801, DateTimeKind.Local).AddTicks(2280));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 9, 17, 12, 51, 806, DateTimeKind.Local).AddTicks(4113));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9d02f63b-1a0d-4fa1-b483-776064cbbfaa");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b61e0769-3a69-496e-a77c-87c9ff26543e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "387dce89-d60d-48dd-97df-966a1a15ea80");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "7f3bccfb-7b1e-4b5a-9909-d1ad851b40f9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "603490a4-9541-4810-aab4-7801612efb78", "AQAAAAEAACcQAAAAEGmi6+0HkN0k7ClVhZwJ7oGXT5bzGp/BEuTxJIlmr+JtXBxcgAzqSAak6S3xUWQtdQ==", "4062339a-e87e-43d7-b9fd-e0da9d8e7e23" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c41c6765-e19c-4683-8586-2dfed702d601", "AQAAAAEAACcQAAAAEJj09afZX6n310VJGum0fSfyj1wEG+h6nZB2AgzhvXQvj2zV6xKDkKlaW6ts0Qmnow==", "97f82e72-e95d-440a-8e8e-4fb0e90f1c35" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efa74019-4179-4c2d-844e-8f7ad01e042e", "AQAAAAEAACcQAAAAEGuvTVMTrvSm5tR4nmQchLhwVtuM8jluWtA+5xlraD7neCHUcgliCtPBaX3TiDgJPA==", "102c1701-7dfa-4a6c-95cb-54c1ac372878" });
        }
    }
}
