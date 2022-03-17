using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class generateuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 3, 13, 17, 40, 40, 716, DateTimeKind.Local).AddTicks(2706));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 3, 13, 17, 40, 40, 721, DateTimeKind.Local).AddTicks(8805));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8b0deb2c-5f19-4f87-921d-d40510750006");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "535f3ae5-702b-44dd-8f64-ca1ec319219f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8feeb3a7-b663-4d14-ab13-880ac1691b33");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "934edb9b-cbc9-4f22-baf6-cd177fabd482");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23942f0c-5139-45e3-b8e8-158d0266e725", "AQAAAAEAACcQAAAAEGHY45cDA0RwbKJfjvH+WX0+joUlpoS/qOZ6RFE/Yk8+ui2wGdA8drwfDU03CUHq9A==", "bdff4f0c-8d8f-4b87-bde3-51b334f38436" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b62b7be-dec3-4583-9bff-d607ce2d2065", "AQAAAAEAACcQAAAAEIatrZ96nplnecTXAZ3RgNY0uGlUBx6UnotDeC0zI1Y7xpW/poHaDM1GUGBNJ/B0iA==", "243fe940-5bfb-41fa-b021-d979fdac3b57" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c65eead8-f8b3-4b82-901b-ffa86165e6c2", "AQAAAAEAACcQAAAAEHaUaldV/eExg1RSzAJHvZCm8hfBmYbX07dT6CP4UOLaVhcp299FSC1XAG5dWkqJCw==", "db51627d-ad24-409c-8799-3fd76c587a24" });
        }
    }
}
