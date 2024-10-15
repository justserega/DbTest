using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTagsToGood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "Tags",
                schema: "stock",
                table: "Goods",
                type: "jsonb",
                nullable: false,
                defaultValue: new string[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                schema: "stock",
                table: "Goods");
        }
    }
}
