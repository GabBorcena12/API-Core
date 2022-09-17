using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Core.Migrations
{
    public partial class ADDTABLE3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "tblTicketPrice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblTicketPrice_SeatId",
                table: "tblTicketPrice",
                column: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblTicketPrice_tblTicketSeats_SeatId",
                table: "tblTicketPrice",
                column: "SeatId",
                principalTable: "tblTicketSeats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblTicketPrice_tblTicketSeats_SeatId",
                table: "tblTicketPrice");

            migrationBuilder.DropIndex(
                name: "IX_tblTicketPrice_SeatId",
                table: "tblTicketPrice");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "tblTicketPrice");
        }
    }
}
