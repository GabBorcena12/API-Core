using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Core.Migrations
{
    public partial class ADDTABLE4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "tblTicketPrice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblTicketPrice_TicketId",
                table: "tblTicketPrice",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblTicketPrice_tblTicket_TicketId",
                table: "tblTicketPrice",
                column: "TicketId",
                principalTable: "tblTicket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblTicketPrice_tblTicket_TicketId",
                table: "tblTicketPrice");

            migrationBuilder.DropIndex(
                name: "IX_tblTicketPrice_TicketId",
                table: "tblTicketPrice");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "tblTicketPrice");
        }
    }
}
