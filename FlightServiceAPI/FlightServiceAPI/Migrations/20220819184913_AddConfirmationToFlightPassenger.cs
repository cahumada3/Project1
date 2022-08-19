using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightServiceAPI.Migrations
{
    public partial class AddConfirmationToFlightPassenger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationNumber",
                table: "Flights");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationNumber",
                table: "FlightPassengers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationNumber",
                table: "FlightPassengers");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationNumber",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
