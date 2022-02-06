using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acorna.Repository.Migrations
{
    public partial class addhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 6, 13, 11, 41, 147, DateTimeKind.Local).AddTicks(6638));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 6, 13, 11, 41, 151, DateTimeKind.Local).AddTicks(92));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7f0e95b4-ac9d-41de-97c7-b2a93b3c2026");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5520012c-4bc2-4d90-8aaf-1ac7e9af2bb1");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6f673da4-1b49-44b3-8489-ec86203ea875");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a636845f-2e5d-4249-8b9a-84dfdc269669");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61724d34-be1b-488f-9b28-bf3c56d621cb", "AQAAAAEAACcQAAAAEDGIdabGsRHuP8ZZMxSabvW6V4rY52hNXSxLkYLp9r+GcMHDD9VNsuCqqGdYYFENDA==", "a4e36dff-b5e9-461e-878f-d96232b763de" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5045d452-8b60-4716-9c31-44af9ed60f36", "AQAAAAEAACcQAAAAEGhLn2D/JydkYdDVr7qr1BOlIeLdSG3QcsuvfRtwOAtHNWR+tbAfmqGI8XfbT2B1zw==", "ed7e433e-ec3e-4347-b20f-45098d663ca7" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d87f5b31-a072-4dd9-9ce7-e20d1eb99bee", "AQAAAAEAACcQAAAAEE7m/g4mdB5KvuFg+jOwU9EzMMmr+b85clt6EJ1HgiKs+z/NE4/kBJqgQJ9sJPR/Ig==", "fd53454d-77f6-4df1-8463-64b8e3519ed2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 6, 12, 45, 25, 124, DateTimeKind.Local).AddTicks(3892));

            migrationBuilder.UpdateData(
                table: "Language",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 2, 6, 12, 45, 25, 129, DateTimeKind.Local).AddTicks(8181));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "670443ff-d43f-42ff-b951-5278c5843e90");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6f919296-0f4e-46a8-a3f7-2f91ce6d853e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6bdeda4e-fa7f-41ba-bd85-36389205c84d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "84c0ce61-cb35-43c2-9dbd-923c40d78b6e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f153eff2-9b7f-418a-992c-b1457df15d8a", "AQAAAAEAACcQAAAAEJhixZI5vOb7zwvURrWFkqz5nu2cbdgNZvruUQyhkN2XPOIAZnqbAlakWBCDcgvBbA==", "fdc0495f-7b24-43cd-bc23-fd9954d25ae2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e45f55ba-0c70-4309-9df4-2d981706e7f9", "AQAAAAEAACcQAAAAEMajzB0pH7eb7SnbewNJiTFyHq0CSXcMGbDKvPQjbMS/chZqUCnHnXI/p8ipX0eSPQ==", "987a7c04-9995-4a95-8df2-c08693b06c43" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe727327-5976-44fc-b498-6dc741b3bba5", "AQAAAAEAACcQAAAAEM8RgvZ5HET3wP4e+xMzqorwrvIc+7pk507YpQeQmlYPY22PI65w6VoSieNrW8pBng==", "c3222cd1-bddf-4062-af21-c09ae3f61c97" });
        }
    }
}
