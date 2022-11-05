using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Core.Migrations
{
    public partial class addrolevalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2dfb7764-1a8e-4835-973d-a403db4023fa", "b1aed336-c2d0-48fc-a75b-85eeaf230212", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b833d4b1-1035-4726-b12a-d77220adaa02", "0166e730-d227-4dfb-b896-52620abf6d59", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dfb7764-1a8e-4835-973d-a403db4023fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b833d4b1-1035-4726-b12a-d77220adaa02");
        }
    }
}
