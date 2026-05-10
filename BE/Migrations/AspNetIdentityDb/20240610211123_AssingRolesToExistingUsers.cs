using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIPA_BE.Migrations.AspNetIdentityDb
{
    /// <inheritdoc />
    public partial class AssingRolesToExistingUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // add roles to users which are already created
            migrationBuilder.Sql("INSERT INTO \"AspNetUserRoles\" (\"UserId\", \"RoleId\")" +
                                 "SELECT \"Id\", (SELECT \"Id\" FROM \"AspNetRoles\" WHERE \"Name\" = 'Student')" +
                                 "FROM \"AspNetUsers\" WHERE NOT EXISTS (" + "" +
                                 "  SELECT 1 FROM \"AspNetUserRoles\"" +
                                 "  WHERE \"AspNetUserRoles\".\"UserId\" = \"AspNetUsers\".\"Id\"" +
                                 ")");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // This migration is irreversible because we cannot reliably determine which users had the "Student" role added by this migration.
        }
    }
}
