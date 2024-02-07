using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedCountriesCitiesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "Deleted", "Name", "Sort", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("2d5234e2-e36b-4d78-8650-8d3eacdd3a64"), true, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6223), new Guid("00000000-0000-0000-0000-000000000000"), false, "Беларусь", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6223), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4106ad6f-1b50-4f9b-8957-7517e999471a"), true, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6225), new Guid("00000000-0000-0000-0000-000000000000"), false, "Казахстан", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6225), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("92b13c12-7613-4643-8e37-2f70b58086e0"), true, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6209), new Guid("00000000-0000-0000-0000-000000000000"), false, "Россия", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6211), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Active", "CountryId", "CreatedAt", "CreatedBy", "Deleted", "Name", "Sort", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("121403f7-5beb-412c-89f4-5d762fded6b1"), true, new Guid("92b13c12-7613-4643-8e37-2f70b58086e0"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6277), new Guid("00000000-0000-0000-0000-000000000000"), false, "Москва", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6278), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("38ef0274-8a9e-4395-af49-3a5a3478316b"), true, new Guid("4106ad6f-1b50-4f9b-8957-7517e999471a"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6314), new Guid("00000000-0000-0000-0000-000000000000"), false, "Нур-Султан", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6314), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4dfce24f-f97c-4e2a-bbed-cf753dc9ea47"), true, new Guid("92b13c12-7613-4643-8e37-2f70b58086e0"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6281), new Guid("00000000-0000-0000-0000-000000000000"), false, "Санкт-Петербург", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6281), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("58eca970-2ea6-4753-beeb-80759b59f0c1"), true, new Guid("92b13c12-7613-4643-8e37-2f70b58086e0"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6293), new Guid("00000000-0000-0000-0000-000000000000"), false, "Екатеринбург", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6293), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7092f36d-8388-4267-831a-42cde3152d36"), true, new Guid("4106ad6f-1b50-4f9b-8957-7517e999471a"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6320), new Guid("00000000-0000-0000-0000-000000000000"), false, "Караганда", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6320), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("716872d5-971e-46b5-b7e9-5f23567ee7ee"), true, new Guid("92b13c12-7613-4643-8e37-2f70b58086e0"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6284), new Guid("00000000-0000-0000-0000-000000000000"), false, "Волгоград", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6284), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("72a95a1e-7c22-4005-8359-11d9cf8c71ed"), true, new Guid("4106ad6f-1b50-4f9b-8957-7517e999471a"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6325), new Guid("00000000-0000-0000-0000-000000000000"), false, "Петропавловск", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6325), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9b664a67-1ba0-4f24-a4f5-7611c0a066c4"), true, new Guid("4106ad6f-1b50-4f9b-8957-7517e999471a"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6311), new Guid("00000000-0000-0000-0000-000000000000"), false, "Алма-Ата", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6312), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("b648ac26-b05e-4bb4-98f9-efd598830c49"), true, new Guid("4106ad6f-1b50-4f9b-8957-7517e999471a"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6322), new Guid("00000000-0000-0000-0000-000000000000"), false, "Шымкент", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6323), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c2f87884-1e71-4c6c-b68a-6bfbab088c02"), true, new Guid("2d5234e2-e36b-4d78-8650-8d3eacdd3a64"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6306), new Guid("00000000-0000-0000-0000-000000000000"), false, "Гомель", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6306), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c9cda8a1-528b-42e3-84d1-fcead269260c"), true, new Guid("2d5234e2-e36b-4d78-8650-8d3eacdd3a64"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6301), new Guid("00000000-0000-0000-0000-000000000000"), false, "Витебск", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6301), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d0c7d163-9c48-4931-8d12-6f43eb6a4113"), true, new Guid("92b13c12-7613-4643-8e37-2f70b58086e0"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6286), new Guid("00000000-0000-0000-0000-000000000000"), false, "Владивосток", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6287), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f043be63-ac20-4d8c-be06-d2487bb331b6"), true, new Guid("2d5234e2-e36b-4d78-8650-8d3eacdd3a64"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6296), new Guid("00000000-0000-0000-0000-000000000000"), false, "Минск", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6296), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f948dcc1-b5e3-4c36-9d22-a2218cc4a1ec"), true, new Guid("2d5234e2-e36b-4d78-8650-8d3eacdd3a64"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6309), new Guid("00000000-0000-0000-0000-000000000000"), false, "Гродно", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6309), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f99c42d9-7467-4e5a-8700-1ccd16281d62"), true, new Guid("2d5234e2-e36b-4d78-8650-8d3eacdd3a64"), new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6298), new Guid("00000000-0000-0000-0000-000000000000"), false, "Брест", 500, new DateTime(2024, 2, 7, 13, 22, 27, 755, DateTimeKind.Utc).AddTicks(6299), new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("121403f7-5beb-412c-89f4-5d762fded6b1"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("38ef0274-8a9e-4395-af49-3a5a3478316b"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4dfce24f-f97c-4e2a-bbed-cf753dc9ea47"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("58eca970-2ea6-4753-beeb-80759b59f0c1"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7092f36d-8388-4267-831a-42cde3152d36"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("716872d5-971e-46b5-b7e9-5f23567ee7ee"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("72a95a1e-7c22-4005-8359-11d9cf8c71ed"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("9b664a67-1ba0-4f24-a4f5-7611c0a066c4"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b648ac26-b05e-4bb4-98f9-efd598830c49"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c2f87884-1e71-4c6c-b68a-6bfbab088c02"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c9cda8a1-528b-42e3-84d1-fcead269260c"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0c7d163-9c48-4931-8d12-6f43eb6a4113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f043be63-ac20-4d8c-be06-d2487bb331b6"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f948dcc1-b5e3-4c36-9d22-a2218cc4a1ec"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f99c42d9-7467-4e5a-8700-1ccd16281d62"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2d5234e2-e36b-4d78-8650-8d3eacdd3a64"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("4106ad6f-1b50-4f9b-8957-7517e999471a"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("92b13c12-7613-4643-8e37-2f70b58086e0"));
        }
    }
}
