using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class fornullplanID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 3, 17, 12, 18, 39, 596, DateTimeKind.Local).AddTicks(2716));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 3, 17, 12, 18, 39, 600, DateTimeKind.Local).AddTicks(9196));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "efacdda1-8fd9-44d7-8dcc-c99185ea08ae");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9e45e2f8-a6bc-476f-a98d-811bf779532e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1a09c3ac-cbcf-4181-a4af-07f071c0e50c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "db69fa0f-a868-4c4d-bf01-ff33365d43b4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "650f6f0b-bdd4-4943-8d76-e2aca7a6ae40", "AQAAAAEAACcQAAAAEE7NPbHs3ckSnuXqTFDQKXrVs38RRQ3YRVtcJ+DJNTljDBQbLc7ufnx9Vb7MeocmVA==", "3fe3d32f-8939-4fc0-b0a0-eedd04998092" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9211f3b5-b89d-401c-b4f5-115c23f3e781", "AQAAAAEAACcQAAAAEHR7FW5Q64jgzZ7NLY9AK2Rvj11MgAVJaQF/b0uSy1dApECqtwSFlvth0FMdZUBIng==", "bc4e0dfa-5897-44b0-be48-b3bed2506ba0" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ef318ba-8c33-44d0-8a1d-44485c5b5b90", "AQAAAAEAACcQAAAAEJiCFuNdw7jhmamMglHwnJgnIB/tSRv8GDE0vrtVG5L7ow/641ZlGD1hZhbMLqEIwQ==", "598ded79-c2c9-4184-8ace-bedc6c70ed43" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 3, 17, 11, 36, 57, 142, DateTimeKind.Local).AddTicks(2278));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 3, 17, 11, 36, 57, 144, DateTimeKind.Local).AddTicks(8665));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f7753f8f-46f8-4240-ac6b-83b20dc37e61");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ba2abaf9-5af4-455f-9e37-788aa1cf6f0b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "392b532e-53a3-4489-a798-f38f15377f51");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "4ccf8552-b313-45bf-9234-7b5f6871326f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e595ad2-ea18-4620-8069-9580782d3549", "AQAAAAEAACcQAAAAEBk4CW+J12yJqauzdrGxs9bFkTcY0X+BaRxrogzkGEnoaEGrFIWEwg0ZY0c4PwLhjw==", "c265d3a5-313d-4a86-ad41-fc88224b3eae" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43626a02-12cc-472e-8be9-727839eea3c5", "AQAAAAEAACcQAAAAEGRPVSNR+pj0+Y6WDzXDh3rVfUlKQuKZ5MLwTPV1EOti/LHsp63miHJ8v+Kq2xylkA==", "bcc8450a-ffe7-44c5-94c7-442409518a57" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d54f589-91b5-4766-9c6c-25913bce561c", "AQAAAAEAACcQAAAAEDB1CFEdonHW1VYcjLb6NqHV/w7eI/TgShrdtQgTp4oROPzikJFJlQbiUZcNzKhXBA==", "ebd7bd9b-d1f6-4228-91a7-19e8d499eadb" });
        }
    }
}
