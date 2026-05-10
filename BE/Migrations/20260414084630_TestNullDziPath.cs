using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIPA_BE.Migrations
{
    /// <inheritdoc />
    public partial class TestNullDziPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM ""SampleImageAnnotations""
                WHERE ""SampleImageID"" IN (3, 6, 7, 12, 13, 17, 20, 21, 26, 28);
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // strasne vela dat by som ti musela dat, tie anotacie su v Initial.cs migracii
        }
    }
}
