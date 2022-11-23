using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedPortal.Infrastructure.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "Mladost 3 bl 13 vh c et 7 ap 24", "01ac6b9a-e374-4542-aaec-22b8be0d6565", "User", "guest@mail.com", false, false, null, "GUEST@MAIL.COM", "GUEST@MAIL.COM", "AQAAAAEAACcQAAAAEInju/TCw5LTHeV1wq+D8CZcayAFJZX54lXJwM4UmqWFzA8r+iJWhHzZyMpL6vdcog==", null, false, "eab83fda-816b-4fb1-a34f-84bb3cf0c582", false, "guest@mail.com" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "Mladost 1 bl 43 vh a et 4 ap 54", "5a2fe623-1612-4c1f-bafd-8918f065f611", "User", "ivan@mail.com", false, false, null, "IVAN@MAIL.COM", "IVAN@MAIL.COM", "AQAAAAEAACcQAAAAEBIBXQ0RS7uo/oNKBh3/ndKkl2qiAxu4jX9eYmJ2746wHDr7eMXaxCC1UydTaIJyPg==", null, false, "c7346238-0f9f-4ffd-a273-03ac108f5451", false, "ivan@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Аlergy" },
                    { 2, "Headache" },
                    { 3, "Flu and cold" },
                    { 4, "Cough" },
                    { 5, "Vomiting" },
                    { 6, "Diarrhea" },
                    { 7, "Antibiotic" },
                    { 8, "Vitamins" },
                    { 9, "Immunostimulants" },
                    { 10, "Probiotic" },
                    { 11, "Memory and Dew" },
                    { 12, "Sleep and relaxation" },
                    { 13, "Beauty and skin" },
                    { 14, "Stress and anxiety" }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "CountryName", "Name", "YearFounded" },
                values: new object[,]
                {
                    { 1, "Canada", "Natural Factors", new DateTime(1984, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Bulgaria", "Fortex", new DateTime(1999, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "USA", "Centrum", new DateTime(1999, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Pharmacies",
                columns: new[] { "Id", "CloseTime", "Location", "Name", "OpenTime" },
                values: new object[,]
                {
                    { 1, "18:30", "Blvd.Alexander Stamboliyski 24, Center, Sofia", "SOpharmacy", "8:30" },
                    { 2, "19:30", "str.Mesta 8001 zh.k.Brothers Miladinovi Burgas", "Framar", "9:30" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategotyId", "Description", "ImageUrl", "ManufacturerId", "Name", "Prescription", "Price" },
                values: new object[] { 1, 3, "Nurofen Forte is intended for symptomatic relief of mild to moderate pain such as: \r\n migraine headache, back pain, toothache, neuralgia, menstrual pain, rheumatic and muscle pain.\r\nNurofen Forte relieves pain, reduces inflammation and temperature.", "https://uploads.remediumapi.com/629af5c0ba14cc001a9a43b0/1/9e05403d419703a002da042be6cda776/480.jpeg", 2, "Nurofen", false, 8.9m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategotyId", "Description", "ImageUrl", "ManufacturerId", "Name", "Prescription", "Price" },
                values: new object[] { 2, 2, "Analgin e is an analgesic medicinal product that is used to affect pain syndromes of various origins:\r\n toothache, neuralgia, neuritis, myalgia, trauma, burns, postoperative pain, phantom pain, dysmenorrhea, renal and biliary colic and headache", "https://cdn.epharm.bg/media/catalog/product/cache/eceadc04885f658154b13d5b2f18d6d8/s/o/sopharma-analgin-500mg-7633.jpg", 1, "Analgin", false, 4.9m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategotyId", "Description", "ImageUrl", "ManufacturerId", "Name", "Prescription", "Price" },
                values: new object[] { 3, 1, "Centrum A-Z is a nutritional supplement with a combination of essential vitamins and minerals to maintain good health. B-vitamins (B1, B2, B6, B12) and magnesium contribute to normal metabolism and energy production. Vitamin A and C, copper and zinc contribute to the normal function of the immune system.", "https://cdn.epharm.bg/media/catalog/product/cache/eceadc04885f658154b13d5b2f18d6d8/c/e/centrum-ot-a-do-q-3060.png", 3, "Centrum", false, 25.9m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Pharmacies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pharmacies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
