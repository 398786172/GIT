using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeXiecheng.API.Migrations
{
    public partial class ShoppingCartMigrationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "308660dc-ae51-480f-824d-7dca6714c3e2",
                column: "ConcurrencyStamp",
                value: "7febae01-edde-4a50-a609-6433c56cac3f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "baeeeec0-05d4-4559-9135-3f0a59a6e5ee", "AQAAAAEAACcQAAAAEIaa6L1GzuEdCmB2r94tNG8ixTkbFLO4O94PHzV92wDiWWeNy25Ct76/IkLwjMu/JQ==", "45601bb1-2118-4a02-b96b-1021621954d4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "308660dc-ae51-480f-824d-7dca6714c3e2",
                column: "ConcurrencyStamp",
                value: "1a8fb824-6631-4c8c-bdb5-496191788b46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85ac86a6-122e-412d-ba14-6c028c7b19a3", "AQAAAAEAACcQAAAAEHmSCxF/ZKuky8zFqKJYGM8rC9J+VRWIv+xxFD5vQo1uG8vBEiuiWuV7T2E2UMvdPQ==", "b7a210bb-2af3-4713-a19b-0a904b1e62e9" });
        }
    }
}
