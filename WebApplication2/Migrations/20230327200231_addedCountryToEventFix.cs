using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class addedCountryToEventFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f63ac06-0f51-4b2e-833a-a03515b9b629");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e640ca92-3a4f-4ecc-92fa-add8ab4f60f5");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "164d7dc8-ed48-45af-bd52-837d68f2f19e", null, "Admin", "ADMIN" },
                    { "d0adee82-242d-447e-a4d7-9d50eed4acf9", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Countries_CityId",
                table: "Events",
                column: "CityId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Countries_CityId",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "164d7dc8-ed48-45af-bd52-837d68f2f19e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0adee82-242d-447e-a4d7-9d50eed4acf9");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Events");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f63ac06-0f51-4b2e-833a-a03515b9b629", null, "Admin", "ADMIN" },
                    { "e640ca92-3a4f-4ecc-92fa-add8ab4f60f5", null, "User", "USER" }
                });
        }
    }
}
