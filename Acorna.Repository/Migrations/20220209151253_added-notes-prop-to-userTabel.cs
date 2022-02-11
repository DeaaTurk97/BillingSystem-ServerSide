using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class addednotesproptouserTabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Users",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Users");

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
    }
}
