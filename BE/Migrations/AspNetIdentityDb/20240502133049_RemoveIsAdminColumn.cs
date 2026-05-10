using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIPA_BE.Migrations.AspNetIdentityDb
{
    /// <inheritdoc />
    public partial class RemoveIsAdminColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "boolean",
                nullable: true);
        }
    }
}
