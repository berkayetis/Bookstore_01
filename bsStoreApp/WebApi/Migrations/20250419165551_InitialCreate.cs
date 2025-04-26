using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14038896-e222-4778-9758-11f55546a556");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b7225a1-84c4-4a87-9056-a0e80d6aadc9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3bfbc1c-eaf5-4a78-9712-8d073bb9138f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "17d8e03a-757a-46a9-b0fb-4bc1ef5b77de", "bf1fb339-0618-417a-be01-2d11f5973cf8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "26251e92-c8c2-4311-88de-b9186e903308", "61311a79-557d-4869-a3d4-cc8ad519c003", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b540283d-9bad-405b-ba56-d3a10c78ec30", "62fc076f-e2a5-4bad-99d5-77bd3d5de8c4", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17d8e03a-757a-46a9-b0fb-4bc1ef5b77de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26251e92-c8c2-4311-88de-b9186e903308");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b540283d-9bad-405b-ba56-d3a10c78ec30");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14038896-e222-4778-9758-11f55546a556", "df6c3cd2-2643-4e10-a75f-6b5628883b25", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b7225a1-84c4-4a87-9056-a0e80d6aadc9", "dded3db5-9a84-41cb-ad8c-7f2c24111e46", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e3bfbc1c-eaf5-4a78-9712-8d073bb9138f", "ba5a43e7-937e-4c11-81ef-1f01fe094746", "Editor", "EDITOR" });
        }
    }
}
