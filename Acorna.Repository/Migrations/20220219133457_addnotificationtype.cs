using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class addnotificationtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "NotificationType",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 160, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NewServiceAdded", null, null },
                    { 170, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ServiceRemoved", null, null },
                    { 180, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ServicePriceGraterThanServicePlan", null, null }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationType",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "NotificationType",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "NotificationType",
                keyColumn: "Id",
                keyValue: 180);

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
    }
}
