using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GistCopy.Application.Migrations
{
    public partial class PrivateGists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Private",
                table: "Gists",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Private",
                table: "Gists");
        }
    }
}
