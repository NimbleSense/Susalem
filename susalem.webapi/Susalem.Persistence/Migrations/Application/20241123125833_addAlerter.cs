using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Susalem.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class addAlerter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BoundedAlerter",
                table: "Position",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoundedAlerter",
                table: "Position");
        }
    }
}
