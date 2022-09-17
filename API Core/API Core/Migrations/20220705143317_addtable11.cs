using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Core.Migrations
{
    public partial class addtable11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "tblTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "tblTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblTransaction_PriceId",
                table: "tblTransaction",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTransaction_SeatId",
                table: "tblTransaction",
                column: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblTransaction_tblTicketPrice_PriceId",
                table: "tblTransaction",
                column: "PriceId",
                principalTable: "tblTicketPrice",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tblTransaction_tblTicketSeats_SeatId",
                table: "tblTransaction",
                column: "SeatId",
                principalTable: "tblTicketSeats",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblTransaction_tblTicketPrice_PriceId",
                table: "tblTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_tblTransaction_tblTicketSeats_SeatId",
                table: "tblTransaction");

            migrationBuilder.DropIndex(
                name: "IX_tblTransaction_PriceId",
                table: "tblTransaction");

            migrationBuilder.DropIndex(
                name: "IX_tblTransaction_SeatId",
                table: "tblTransaction");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "tblTransaction");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "tblTransaction");
        }
    }
}
