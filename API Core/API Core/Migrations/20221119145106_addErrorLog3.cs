using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Core.Migrations
{
    public partial class addErrorLog3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09e0c2d1-84b9-4602-9aac-71af72a8615c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa34650d-71b2-436d-a6f7-b4cfc76affa0");

            migrationBuilder.RenameColumn(
                name: "statusCode",
                table: "tblErrorLogs",
                newName: "StatusCode");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c741059e-7230-43de-98cc-9b3eaaa468ac", "e7b23af6-6b7d-4f10-8030-ffa338e93358", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d21c3b99-d2e3-43a7-b6c8-72a1946ba9bb", "b4d3e1e0-5085-4a2d-b2a8-2f585c0eedb7", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c741059e-7230-43de-98cc-9b3eaaa468ac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d21c3b99-d2e3-43a7-b6c8-72a1946ba9bb");

            migrationBuilder.RenameColumn(
                name: "StatusCode",
                table: "tblErrorLogs",
                newName: "statusCode");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "09e0c2d1-84b9-4602-9aac-71af72a8615c", "b13180c1-ada9-4895-9605-14cae053538e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aa34650d-71b2-436d-a6f7-b4cfc76affa0", "17151cfa-cc3f-4fc9-ab96-901354a57167", "User", "USER" });
        }
    }
}
