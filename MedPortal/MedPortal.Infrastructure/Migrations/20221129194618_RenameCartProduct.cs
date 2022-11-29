using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedPortal.Infrastructure.Migrations
{
    public partial class RenameCartProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardProduct");

            migrationBuilder.CreateTable(
                name: "CartProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PharamcyId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartProducts_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProducts_Pharmacies_PharamcyId",
                        column: x => x.PharamcyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bfd958ca-f1bb-40b1-8450-bc318497c924", "AQAAAAEAACcQAAAAECxcYYdaPVj9DBEhdsfbdinQfkjBzA1IV93SU4wRv6qwXtQq6ZbjzK+2sDB2/nP/CQ==", "19e1fb82-5061-4b59-9e84-7517ceaa0865" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0dffee07-df8e-4f0c-932b-7405eed675d8", "AQAAAAEAACcQAAAAEH3MeQz1tDe53muxw7OFtD1/ViL2rVd5xi1kHKCjPEq9rkBxt6/D4Vfr0a1SaXbmQg==", "6a8aa7be-8872-43c1-9c5e-3642a15ec22a" });

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_CartId",
                table: "CartProducts",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_PharamcyId",
                table: "CartProducts",
                column: "PharamcyId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProducts");

            migrationBuilder.CreateTable(
                name: "CardProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    PharamcyId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardProduct_Carts_CardId",
                        column: x => x.CardId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardProduct_Pharmacies_PharamcyId",
                        column: x => x.PharamcyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CardProduct_CardId",
                table: "CardProduct",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardProduct_PharamcyId",
                table: "CardProduct",
                column: "PharamcyId");

            migrationBuilder.CreateIndex(
                name: "IX_CardProduct_ProductId",
                table: "CardProduct",
                column: "ProductId");
        }
    }
}
