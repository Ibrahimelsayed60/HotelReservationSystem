using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.Data.Migrations
{
    /// <inheritdoc />
    public partial class amendReservationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomCapacity",
                schema: "Reservations",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoomDescription",
                schema: "Reservations",
                table: "Reservations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                schema: "Reservations",
                table: "Reservations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "RoomPrice",
                schema: "Reservations",
                table: "Reservations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                schema: "Reservations",
                table: "Reservations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomCapacity",
                schema: "Reservations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomDescription",
                schema: "Reservations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomName",
                schema: "Reservations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomPrice",
                schema: "Reservations",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomType",
                schema: "Reservations",
                table: "Reservations");
        }
    }
}
