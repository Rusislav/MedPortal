using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedPortal.Infrastructure.Migrations
{
    public partial class SetUserFlagForActiveProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c94f0d51-7e88-4058-af6e-900cc49bb1c3", true, "AQAAAAEAACcQAAAAEKZfD4SDGeFVPOEpRPEOeUSJoM736/HRgUpUEj+7X14v1HPurcH93X0ku1HasRVFnA==", "d6a1359a-5dd9-4185-a928-315085c61e4f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c260303a-7e67-4642-8551-0cb1780fe5dc", true, "AQAAAAEAACcQAAAAEJrg0CPmf/QFwUju/psEHfnfegj7Q8v/DqWWYb5lFWbtQPy9SbRNBm3hQxmXcqtvlw==", "0f8a0930-b0af-41c3-aac4-d4f711666b75" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

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
        }
    }
}
