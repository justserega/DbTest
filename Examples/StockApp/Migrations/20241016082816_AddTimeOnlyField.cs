using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeOnlyField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeOnly",
                schema: "stock",
                table: "TestEntities",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeOnly",
                schema: "stock",
                table: "TestEntities");
        }
    }
}
