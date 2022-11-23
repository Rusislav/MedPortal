using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedPortal.Infrastructure.Migrations
{
    public partial class SetRoleToAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1f091cd0-b589-4b77-b951-480ed62fa46f", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "213228ef-b5bb-473f-888c-73b3e0711f91", "AQAAAAEAACcQAAAAEHu7w7Tkjr32zO+tORXdlMUxJe0cp6v7Mouy8tSRXSjMtkf0b80aOZX+CNRWRNX3+A==", "7518aeaf-b545-4ad3-8e00-e4262a88b5d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f4815895-606a-41dc-ae42-d558df5f580e", "AQAAAAEAACcQAAAAEEC6+FGeZZWLAbetU1YPLzm1FcuLT8WqyRvmf2gDLnjww7+R6wtSKv6l90OCGfjpVw==", "3dab6efc-fd31-4977-b655-22a83be6b630" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1f091cd0-b589-4b77-b951-480ed62fa46f", "dea12856-c198-4129-b3f3-b893d8395082" });

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0af91f65-fc3f-4205-accb-bb79e748e0e3", "AQAAAAEAACcQAAAAED3fYThGooCU5BHFhBekHapCM4/ftj1bNtl9X2KRXW4im7Cx5zLlI07pjmXCGd6u4w==", "3afd7601-ad9a-4b09-9569-f87dc904bde1" });
        }
    }
}
