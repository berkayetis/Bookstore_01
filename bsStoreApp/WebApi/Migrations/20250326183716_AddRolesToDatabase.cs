using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddRolesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f86bd8c-31ca-4cf5-a2ed-1ca73a3effbc", "bc4567e3-78d9-425c-86fb-5580293b5b23", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "790c86af-a81d-4a2e-8337-0630efed5b6b", "fe9c683a-e77e-48b6-a3eb-eea02082ad7f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e679839c-9122-4d09-afaf-bbd79ea1ece9", "969db720-9ddd-414f-a45f-d15d83fcff5f", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f86bd8c-31ca-4cf5-a2ed-1ca73a3effbc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "790c86af-a81d-4a2e-8337-0630efed5b6b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e679839c-9122-4d09-afaf-bbd79ea1ece9");
        }
    }
}
