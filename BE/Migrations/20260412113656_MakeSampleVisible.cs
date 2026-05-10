using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIPA_BE.Migrations
{
    /// <inheritdoc />
    public partial class MakeSampleVisible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
            table: "SampleImages",
            keyColumn: "ID",
            keyValue: 1,
            columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 4, "Snímka 1" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 4, "Snímka 2" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 4, "Snímka 3" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 4, "Snímka obličiek" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 4, "Obličky so zápalom" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 4, "Oblička" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 4, "Snímka obličiek 2" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 4, "Obličky" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 4, "Snímka obličky" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 3, "Snímka 1" }); 

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 3, "Snímka 2" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 3, "Snímka 3" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 3, "Snímka Pľúc" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 14,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 10, "Priedušky" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 15,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 11, "Snímka Hrtanu" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 16,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 16, "Hltan" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 17,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 10, "Snímka Priedušky" });
                
            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 18,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 10, "Prieduška" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 19,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 11, "Snímka Hrtanu" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 20,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 11, "Snímka Hrtan" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 21,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 11, "Snímka Hrtanu" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 22,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 16, "Hltan" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 23,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 16, "Snímka Hltanu" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 24,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 16, "Hltan" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 25,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 16, "Hltan" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 26,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 3, "Snímka Pľúc" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 27,
                columns: new[] { "IsVisible", "OrganID", "Name" },
            values: new object[] { true, 3, "Pľúca" });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 28,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 29,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 30,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 31,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 32,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 33,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 34,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 35,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 36,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 37,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 38,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 39,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 40,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 41,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 42,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 43,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 44,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 45,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 46,
                column: "IsVisible",
                value: true);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 47,
                column: "IsVisible",
                value: true);
        }

            

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 1,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 2,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 3,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 4,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 5,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 6,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 7,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 8,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 9,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 10,
                column: "IsVisible",
                value: false); 

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 11,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 12,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 13,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 14,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 15,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 16,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 17,
                column: "IsVisible",
                value: false);
                
            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 18,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 19,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 20,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 21,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 22,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 23,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 24,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 25,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 26,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 27,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 28,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 29,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 30,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 31,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 32,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 33,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 34,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 35,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 36,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 37,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 38,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 39,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 40,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 41,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 42,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 43,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 44,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 45,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 46,
                column: "IsVisible",
                value: false);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 47,
                column: "IsVisible",
                value: false);
        }
    }
}
