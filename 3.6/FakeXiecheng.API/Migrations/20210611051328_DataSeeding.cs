using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeXiecheng.API.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureTime", "Description", "DiscountPresent", "Features", "Fees", "Notes", "OriginalPrice", "Title", "UpdateTime" },
                values: new object[] { new Guid("f9d43c0c-e7bc-4930-b25c-45e9d591bcac"), new DateTime(2021, 6, 11, 5, 13, 28, 437, DateTimeKind.Utc).AddTicks(1324), null, "shuoming", null, null, null, null, 0m, "ceshititle", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("f9d43c0c-e7bc-4930-b25c-45e9d591bcac"));
        }
    }
}
