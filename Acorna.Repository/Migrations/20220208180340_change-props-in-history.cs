using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class changepropsinhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewUserName",
                table: "History");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "History",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 8, 20, 3, 39, 196, DateTimeKind.Local).AddTicks(1014));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 8, 20, 3, 39, 201, DateTimeKind.Local).AddTicks(2805));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "1c4318ee-7497-416c-b744-aa83cb5e3539");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "aad3f268-cca3-4488-83b3-35c9f62e6863");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "51a6181d-9e4f-4aa8-ac89-5fcef6949233");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "ca576732-e49e-4693-81b2-c7a9a64a8eb5");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68c12d04-d355-4d8e-b95e-eb1a387e3c59", "AQAAAAEAACcQAAAAEIqwoGpNqmDZIUqbbJPc9r/t5j/lTeHqFMA/mMivu8/hLj8ebb7R9WinoVyr/7r9ZA==", "5b1f34ce-c200-486f-8c53-15484bd9a43d" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bfaea0a0-5605-497d-b309-7cc72eb49c7c", "AQAAAAEAACcQAAAAEKiawRHLoUAEIzfmq2NqZc4A+BnCXn9ePqZIRqK0dhaBW3PvS1cYBKApyRLUYj5DCA==", "43838ade-b9d9-4fb4-8574-2d09f2c2e4a7" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b8c9814-5e9c-4145-8813-8983a013fdfb", "AQAAAAEAACcQAAAAEBYO7lYLTzxWp9fQzhwb6ayBvYkBsMtlmGPNuG4Q135uNtdikLfUZMUKagldv8e34Q==", "028c7914-c563-4224-850c-19cf78f69fe6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "History");

            migrationBuilder.AddColumn<string>(
                name: "NewUserName",
                table: "History",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 8, 18, 50, 27, 211, DateTimeKind.Local).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 8, 18, 50, 27, 217, DateTimeKind.Local).AddTicks(426));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "27fedafc-9427-4ace-896d-342c94d011c0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "90a76e28-5462-4217-8dc1-8bdd3d1fd543");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7e5dfd46-572e-4082-9de7-732874ff185e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "0e88838f-d598-429a-9eff-a108a24f79b0");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba1ce4de-1af6-4f46-a6e9-5b24f426c92a", "AQAAAAEAACcQAAAAEJJ2BdU+tgtWgRpZ+s7h8JaYI/D1g806FADlXezsoLRxsafaE2lE0ison6IZbaGH0A==", "5ce26394-8085-48bd-bad5-c3206799dbd0" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0228789-0e3a-4434-abea-e4cc396a6414", "AQAAAAEAACcQAAAAEKQRf6k6ZcBJhOc6ZWTGiyO2TXA8j0QWtB6+nFcsHlWBaC1c1gAzGCK/36FbV3MAyA==", "3a9f7a2b-bdc9-48f4-9f6b-6ae371543561" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6681b312-419a-472e-9534-26769d2bfff8", "AQAAAAEAACcQAAAAEF5Ht5Imx5r1XS5GzCaTuZsWfM9K/FC5ezm+qdGsfsMMS3VHKNbpOxuwrqDX1hu1EA==", "0346ede3-84f9-4334-92bf-8e96542ac64a" });
        }
    }
}
