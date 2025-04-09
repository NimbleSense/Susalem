using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Susalem.Persistence.Migrations.Record
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PositionRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Contents = table.Column<string>(type: "TEXT", nullable: true),
                    PositionFunction = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    PositionName = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    AreaName = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionRecord", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PositionRecord_Id_PositionId",
                table: "PositionRecord",
                columns: new[] { "Id", "PositionId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionRecord");
        }
    }
}
