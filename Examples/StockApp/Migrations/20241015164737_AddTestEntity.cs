using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestEntities",
                schema: "stock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Decimal = table.Column<decimal>(type: "numeric", nullable: false),
                    Empty = table.Column<string>(type: "text", nullable: false),
                    Double = table.Column<double>(type: "double precision", nullable: false),
                    Tags = table.Column<string[]>(type: "jsonb", nullable: false),
                    EntityType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestEntities",
                schema: "stock");
        }
    }
}
