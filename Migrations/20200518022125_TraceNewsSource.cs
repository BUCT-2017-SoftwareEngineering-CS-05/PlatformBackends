using Microsoft.EntityFrameworkCore.Migrations;

namespace Analyzer.Migrations
{
    public partial class TraceNewsSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "News");
        }
    }
}
