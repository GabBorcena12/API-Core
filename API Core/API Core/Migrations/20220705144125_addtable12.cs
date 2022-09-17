using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Core.Migrations
{
    public partial class addtable12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketDescription",
                table: "tblTransaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TicketDescription",
                table: "tblTransaction",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
