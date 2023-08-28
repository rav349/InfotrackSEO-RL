using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoTrackSEO.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "SearchResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "SearchResults");
        }
    }
}
