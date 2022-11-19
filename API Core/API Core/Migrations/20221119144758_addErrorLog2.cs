using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Core.Migrations
{
    public partial class addErrorLog2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34ca35ca-d522-49f4-aa7b-eef6755a3b3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfc3c714-0b54-46de-bb9c-0b5ce2837640");

            migrationBuilder.AddColumn<int>(
                name: "statusCode",
                table: "tblErrorLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "09e0c2d1-84b9-4602-9aac-71af72a8615c", "b13180c1-ada9-4895-9605-14cae053538e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aa34650d-71b2-436d-a6f7-b4cfc76affa0", "17151cfa-cc3f-4fc9-ab96-901354a57167", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09e0c2d1-84b9-4602-9aac-71af72a8615c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa34650d-71b2-436d-a6f7-b4cfc76affa0");

            migrationBuilder.DropColumn(
                name: "statusCode",
                table: "tblErrorLogs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bfc3c714-0b54-46de-bb9c-0b5ce2837640", "d8bf8b60-7d72-4329-8db0-7e00472b1076", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34ca35ca-d522-49f4-aa7b-eef6755a3b3f", "4b7a0625-32b3-48a3-bd4e-f60d43f8a2a4", "User", "USER" });
        }
    }
}
