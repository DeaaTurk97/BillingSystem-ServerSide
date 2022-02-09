using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class adddatetimetohistoryuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "History",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "History",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "History");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "History",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 7, 12, 54, 37, 209, DateTimeKind.Local).AddTicks(5395));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 7, 12, 54, 37, 213, DateTimeKind.Local).AddTicks(4299));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "510d4f36-74a9-4749-9a8d-c0605835ebf4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "42cda499-65ee-4ebc-aa9f-0c48d0d78d88");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "05f7db40-46eb-4bad-a6d0-735576ab34dd");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "2d89b723-16d8-4b75-b2ca-fe4d8c99a940");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19d5411d-9719-491c-9e12-5ea55e82c17e", "AQAAAAEAACcQAAAAEOOaoMScvkr3l1xTJs6aqbsSYEerblKrG3ecjsAy7QMkTOJFnY4XawGpNT4+rqVLYg==", "3d0dca42-fcb4-4579-832e-616a01a5dbdf" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79c75a17-a3cc-4f92-a87f-0347d13b3fd1", "AQAAAAEAACcQAAAAEIsrvCgbf+hs+m+yfoc92fMLfLHuY12bg1nXjiavTI43Eq/6IB3ipsyrOrtAz0266w==", "af08eee2-5f37-43a6-90bd-adf110d0f6a4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f51d96f3-006d-44fa-9841-3c69c5b5cf27", "AQAAAAEAACcQAAAAEB6D0zOaBPo7s8Qsyn1nXiJk1XEx+trI2QBr2crXMMbD/0mxoR5N9ic4B5rXc3jfLw==", "b3a6cb4a-87cb-4a42-9923-b74e0539cbf3" });
        }
    }
}
