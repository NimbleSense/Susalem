using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Susalem.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class AddShowDoor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowDoor",
                table: "Position",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowDoor",
                table: "Position");
        }
    }
}
