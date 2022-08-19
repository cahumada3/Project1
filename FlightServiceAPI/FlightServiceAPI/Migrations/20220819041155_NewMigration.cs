using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightServiceAPI.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightPassenger_Flights_FlightId",
                table: "FlightPassenger");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightPassenger_Passengers_PassengerId",
                table: "FlightPassenger");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightPassenger",
                table: "FlightPassenger");

            migrationBuilder.RenameTable(
                name: "FlightPassenger",
                newName: "FlightPassengers");

            migrationBuilder.RenameIndex(
                name: "IX_FlightPassenger_PassengerId",
                table: "FlightPassengers",
                newName: "IX_FlightPassengers_PassengerId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightPassenger_FlightId",
                table: "FlightPassengers",
                newName: "IX_FlightPassengers_FlightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightPassengers",
                table: "FlightPassengers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPassengers_Flights_FlightId",
                table: "FlightPassengers",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPassengers_Passengers_PassengerId",
                table: "FlightPassengers",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightPassengers_Flights_FlightId",
                table: "FlightPassengers");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightPassengers_Passengers_PassengerId",
                table: "FlightPassengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlightPassengers",
                table: "FlightPassengers");

            migrationBuilder.RenameTable(
                name: "FlightPassengers",
                newName: "FlightPassenger");

            migrationBuilder.RenameIndex(
                name: "IX_FlightPassengers_PassengerId",
                table: "FlightPassenger",
                newName: "IX_FlightPassenger_PassengerId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightPassengers_FlightId",
                table: "FlightPassenger",
                newName: "IX_FlightPassenger_FlightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlightPassenger",
                table: "FlightPassenger",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPassenger_Flights_FlightId",
                table: "FlightPassenger",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightPassenger_Passengers_PassengerId",
                table: "FlightPassenger",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
