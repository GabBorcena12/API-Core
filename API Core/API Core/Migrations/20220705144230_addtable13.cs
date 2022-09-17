using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Core.Migrations
{
    public partial class addtable13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblTicketPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblTicketPrice",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
