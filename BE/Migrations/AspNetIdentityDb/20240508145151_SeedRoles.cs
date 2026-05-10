using HIPA_BE.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIPA_BE.Migrations.AspNetIdentityDb
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                migrationBuilder.Sql($"INSERT INTO \"AspNetRoles\" (\"Id\", \"Name\", \"NormalizedName\") VALUES ('{Guid.NewGuid()}', '{role}', '{role.ToString()?.ToUpper()}')");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                migrationBuilder.Sql($"DELETE FROM \"AspNetRoles\" WHERE \"Name\" = '{role}'");
            }
        }
    }
}
