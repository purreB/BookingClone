using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingClone.Migrations
{
    /// <inheritdoc />
    public partial class projectRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "StaffUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Guests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "StaffUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Guests",
                type: "text",
                nullable: true);
        }
    }
}
