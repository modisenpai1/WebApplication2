using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class updateInvitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b207963-a8b5-467a-b935-fbb7c5bbbb3e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "752d2d43-c522-4a22-af49-01944e2f0fd7");

            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "Invitations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9fbb5070-1ce7-458d-bf01-547f6fdc4ef1", null, "User", "USER" },
                    { "d4d78451-c839-4d79-8d06-cc9de3c1c3cc", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fbb5070-1ce7-458d-bf01-547f6fdc4ef1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4d78451-c839-4d79-8d06-cc9de3c1c3cc");

            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "Invitations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b207963-a8b5-467a-b935-fbb7c5bbbb3e", null, "User", "USER" },
                    { "752d2d43-c522-4a22-af49-01944e2f0fd7", null, "Admin", "ADMIN" }
                });
        }
    }
}
