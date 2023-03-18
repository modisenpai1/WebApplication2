using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class addedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Countries_CountryId",
                table: "Adresses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51e493f1-6b72-4c81-b40b-6575043b42cf", null, "Admin", "ADMIN" },
                    { "73c8c181-e8b7-40d8-a2e5-fb144d0f7ae1", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Countries_CountryId",
                table: "Adresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Countries_CountryId",
                table: "Adresses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51e493f1-6b72-4c81-b40b-6575043b42cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73c8c181-e8b7-40d8-a2e5-fb144d0f7ae1");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Countries_CountryId",
                table: "Adresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
