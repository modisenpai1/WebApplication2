using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class ff1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "063b31f2-129a-4e9c-9078-c0048a9f6659", null, "Admin", "ADMIN" },
                    { "dc96f52d-0842-4764-b2e2-f4aaea2f7087", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Countries_CityId",
                table: "Events",
                column: "CityId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
                keyValue: "063b31f2-129a-4e9c-9078-c0048a9f6659");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc96f52d-0842-4764-b2e2-f4aaea2f7087");

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
    }
}
