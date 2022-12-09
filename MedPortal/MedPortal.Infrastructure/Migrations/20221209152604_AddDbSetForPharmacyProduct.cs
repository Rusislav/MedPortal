using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedPortal.Infrastructure.Migrations
{
    public partial class AddDbSetForPharmacyProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PharamcyProduct_Pharmacies_PharmacyId",
                table: "PharamcyProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PharamcyProduct_Products_ProductId",
                table: "PharamcyProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PharamcyProduct",
                table: "PharamcyProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PharamcyProduct");

            migrationBuilder.RenameTable(
                name: "PharamcyProduct",
                newName: "PharamcyProducts");

            migrationBuilder.RenameIndex(
                name: "IX_PharamcyProduct_ProductId",
                table: "PharamcyProducts",
                newName: "IX_PharamcyProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PharamcyProducts",
                table: "PharamcyProducts",
                columns: new[] { "PharmacyId", "ProductId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ddaa7b4d-dbe0-4a42-80ef-4ea28673e26b", "AQAAAAEAACcQAAAAEHnTU5siNgv4prSPU6WfxZPjTKjiHLJ/kM50kiOBu7/DCFclFQF8qO4ceLlFaMlPLg==", "22a49422-6f94-4c43-831e-13fa3eb6b390" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3796771f-ead8-41e3-99cf-d7e0f8e02c2e", "AQAAAAEAACcQAAAAELG+UZCbFcmm5YArUz0ALWrYg7FXXQHg5yOjCHEMwWrOAto25IEej/MeBYDSg2yVqA==", "4c19f94d-8c64-4ee4-a235-75a445cd8a09" });

            migrationBuilder.AddForeignKey(
                name: "FK_PharamcyProducts_Pharmacies_PharmacyId",
                table: "PharamcyProducts",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PharamcyProducts_Products_ProductId",
                table: "PharamcyProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PharamcyProducts_Pharmacies_PharmacyId",
                table: "PharamcyProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PharamcyProducts_Products_ProductId",
                table: "PharamcyProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PharamcyProducts",
                table: "PharamcyProducts");

            migrationBuilder.RenameTable(
                name: "PharamcyProducts",
                newName: "PharamcyProduct");

            migrationBuilder.RenameIndex(
                name: "IX_PharamcyProducts_ProductId",
                table: "PharamcyProduct",
                newName: "IX_PharamcyProduct_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PharamcyProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PharamcyProduct",
                table: "PharamcyProduct",
                columns: new[] { "PharmacyId", "ProductId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c94f0d51-7e88-4058-af6e-900cc49bb1c3", "AQAAAAEAACcQAAAAEKZfD4SDGeFVPOEpRPEOeUSJoM736/HRgUpUEj+7X14v1HPurcH93X0ku1HasRVFnA==", "d6a1359a-5dd9-4185-a928-315085c61e4f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c260303a-7e67-4642-8551-0cb1780fe5dc", "AQAAAAEAACcQAAAAEJrg0CPmf/QFwUju/psEHfnfegj7Q8v/DqWWYb5lFWbtQPy9SbRNBm3hQxmXcqtvlw==", "0f8a0930-b0af-41c3-aac4-d4f711666b75" });

            migrationBuilder.AddForeignKey(
                name: "FK_PharamcyProduct_Pharmacies_PharmacyId",
                table: "PharamcyProduct",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PharamcyProduct_Products_ProductId",
                table: "PharamcyProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
