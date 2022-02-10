using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class changesOnBillUserHistoryTabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "History",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 10, 13, 38, 4, 704, DateTimeKind.Local).AddTicks(1333));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 10, 13, 38, 4, 710, DateTimeKind.Local).AddTicks(5657));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "438c57d6-58f6-4754-b4c3-d6e071fdff1a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d314a8f7-9210-49f8-a2fc-d0e46d66bb37");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9bb65a2c-55f2-418b-87ff-df77b8ae7e0f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "72aaeab2-14e4-4d9d-91db-8409640168a1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1986f73b-d7f1-4a95-af9b-70c3aa050c2e", "AQAAAAEAACcQAAAAELKZ2Bo3YhzuYLI6Tur2JgpJBD4+FWdMbxQgm9x8eaBqwRhzLcY5BtP2zhZWOR6QTA==", "dd43078d-20f1-4899-906e-af1a9ef680dd" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0616150a-e7f1-4b7d-9249-80150d3ca28b", "AQAAAAEAACcQAAAAEC+Z8zbPLo+Z5GHObIaT7aG9YAQabaktq0qX7xrye/xCOwRDuz1JA6EllsRBfKpHgg==", "13906f0f-a3c2-4634-9e35-3f6cc2db9cd2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d754d3d-4169-4b2f-96fa-b2ec9010947f", "AQAAAAEAACcQAAAAEPHiNFigt9xzh9wzEdoyCQ3CePjHbVvi6TkE9q4Orv0UAYIyetIMvqBsCyI+fFYlKQ==", "19bf21c4-69d5-4873-86bb-18146cc698f4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "History");

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
    }
}
