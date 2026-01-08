using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evento.Migrations
{
    /// <inheritdoc />
    public partial class fdseofin34sfe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionRole",
                table: "AspNetRoles",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionRole",
                table: "AspNetRoles");
        }
    }
}
