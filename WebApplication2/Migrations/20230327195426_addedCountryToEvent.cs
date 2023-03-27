using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class addedCountryToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51e493f1-6b72-4c81-b40b-6575043b42cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73c8c181-e8b7-40d8-a2e5-fb144d0f7ae1");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "UserOrgs",
                newName: "Role");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f63ac06-0f51-4b2e-833a-a03515b9b629", null, "Admin", "ADMIN" },
                    { "e640ca92-3a4f-4ecc-92fa-add8ab4f60f5", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f63ac06-0f51-4b2e-833a-a03515b9b629");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e640ca92-3a4f-4ecc-92fa-add8ab4f60f5");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "UserOrgs",
                newName: "role");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51e493f1-6b72-4c81-b40b-6575043b42cf", null, "Admin", "ADMIN" },
                    { "73c8c181-e8b7-40d8-a2e5-fb144d0f7ae1", null, "User", "USER" }
                });
        }
    }
}
