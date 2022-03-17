using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class FicedAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FixedAmount",
                table: "Users",
                type: "decimal(18, 3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FixedAmount",
                table: "CallSummary",
                type: "decimal(18, 3)",
                nullable: false,
                defaultValue: 0m);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedAmount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FixedAmount",
                table: "CallSummary");

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 19, 15, 34, 55, 769, DateTimeKind.Local).AddTicks(2989));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 19, 15, 34, 55, 789, DateTimeKind.Local).AddTicks(4623));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "eff9b363-dbe6-4298-8113-1e598bffce21");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3e4ccaf1-6165-45c1-966d-c4b2cdf7af3b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "576739d4-839b-4094-ad52-ca2c9bf6a7c1");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "61c77ca1-94aa-4967-a120-0bbfad99aa23");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a1c5d69-ff4c-4588-b23c-92a0f03c2035", "AQAAAAEAACcQAAAAEJKIOTwt6sdKBRlADB9Nvo76rWCLPS1+irotM832FSen4lYRaf++RFWSOBxBOKcTnA==", "e380aae7-f0f3-45d9-b919-66d486f38857" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "792bb5ed-b848-4f22-bba3-fad7014e7ef3", "AQAAAAEAACcQAAAAELxXqyas3aogsOa4KAURZde5YBloLA6DonwKk/kOLwjvFYreYVboCoUUSQc/mzhSUA==", "a4dc8312-a94d-4eb0-a782-c47e6586cd59" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25b286af-884c-4cf1-a3a3-a3de15dccaab", "AQAAAAEAACcQAAAAECXgun6PSS3MTGy7Wjh1lxoqEDHDibuQLur/yFHWjcuPAeKwurUfhTr/D9cjE/IXfA==", "be430919-5f42-4888-b3bf-92566b565aa5" });
        }
    }
}
