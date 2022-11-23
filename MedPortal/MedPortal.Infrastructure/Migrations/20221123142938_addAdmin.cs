using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedPortal.Infrastructure.Migrations
{
    public partial class addAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f091cd0-b589-4b77-b951-480ed62fa46f", "5fce0a67-70da-4e40-af4e-42ef38d13348", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f07bb450-f007-40f1-89af-f44600213e06", "AQAAAAEAACcQAAAAEHfkc8TVzPUFHwJx6PAL5Ej74mSyoLIrNjJF7xCXWYZS2qGutmN1zDrscP5K0vYkAg==", "20052cb8-6499-44ee-a9f8-8cd5c417fe55" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "0af91f65-fc3f-4205-accb-bb79e748e0e3", "admin@mail.com", "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAED3fYThGooCU5BHFhBekHapCM4/ftj1bNtl9X2KRXW4im7Cx5zLlI07pjmXCGd6u4w==", "3afd7601-ad9a-4b09-9569-f87dc904bde1", "admin@mail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f091cd0-b589-4b77-b951-480ed62fa46f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01ac6b9a-e374-4542-aaec-22b8be0d6565", "AQAAAAEAACcQAAAAEInju/TCw5LTHeV1wq+D8CZcayAFJZX54lXJwM4UmqWFzA8r+iJWhHzZyMpL6vdcog==", "eab83fda-816b-4fb1-a34f-84bb3cf0c582" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "5a2fe623-1612-4c1f-bafd-8918f065f611", "ivan@mail.com", "IVAN@MAIL.COM", "IVAN@MAIL.COM", "AQAAAAEAACcQAAAAEBIBXQ0RS7uo/oNKBh3/ndKkl2qiAxu4jX9eYmJ2746wHDr7eMXaxCC1UydTaIJyPg==", "c7346238-0f9f-4ffd-a273-03ac108f5451", "ivan@mail.com" });
        }
    }
}
