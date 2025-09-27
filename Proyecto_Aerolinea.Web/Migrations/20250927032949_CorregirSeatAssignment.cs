using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Aerolinea.Web.Migrations
{
    /// <inheritdoc />
    public partial class CorregirSeatAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatAssigments_Aircrafts_AircraftId",
                table: "SeatAssigments");

            migrationBuilder.DropIndex(
                name: "IX_SeatAssigments_AircraftId",
                table: "SeatAssigments");

            migrationBuilder.DropColumn(
                name: "AircraftId",
                table: "SeatAssigments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AircraftId",
                table: "SeatAssigments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeatAssigments_AircraftId",
                table: "SeatAssigments",
                column: "AircraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatAssigments_Aircrafts_AircraftId",
                table: "SeatAssigments",
                column: "AircraftId",
                principalTable: "Aircrafts",
                principalColumn: "AircraftId");
        }
    }
}
