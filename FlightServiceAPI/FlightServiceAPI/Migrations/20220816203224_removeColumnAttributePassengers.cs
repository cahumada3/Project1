using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightServiceAPI.Migrations
{
    public partial class removeColumnAttributePassengers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Passengers",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Passengers",
                newName: "Title");
        }
    }
}
