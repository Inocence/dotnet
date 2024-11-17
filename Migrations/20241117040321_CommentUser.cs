using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CommentUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32999f05-d9ac-4978-a7d6-e40a0ba36781");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddd6ca86-79a4-421e-bdec-581ab1c35f57");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "740fa93d-cca0-497f-9d2e-78ae31be07c7", null, "Admin", "ADMIN" },
                    { "d6457eff-fdf5-46f7-8ddb-58ea89bcec7c", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "740fa93d-cca0-497f-9d2e-78ae31be07c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6457eff-fdf5-46f7-8ddb-58ea89bcec7c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32999f05-d9ac-4978-a7d6-e40a0ba36781", null, "Admin", "ADMIN" },
                    { "ddd6ca86-79a4-421e-bdec-581ab1c35f57", null, "User", "USER" }
                });
        }
    }
}
