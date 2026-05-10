using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HIPA_BE.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganKeywords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 1,
                column: "KeyWords",
                value: "obličky;zápal;infekcia");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 2,
                column: "KeyWords",
                value: "obličky;vylučovacia sústava;zápal;diabetes");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 4,
                column: "KeyWords",
                value: "oblička");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 5,
                column: "KeyWords",
                value: "zápal;obličky;vylučovacia sústava");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 7,
                column: "KeyWords",
                value: "obličky;zdravé");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 8,
                column: "KeyWords",
                value: "obličky;zápal;infekcia");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 10,
                column: "KeyWords",
                value: "zápal;dýchacia sústava");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 11,
                column: "KeyWords",
                value: "pľúca;dýchacia sústava;rakovina");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 12,
                column: "KeyWords",
                value: "pľúca");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 14,
                column: "KeyWords",
                value: "priedušky;zápal");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 15,
                column: "KeyWords",
                value: "hrtan");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 17,
                column: "KeyWords",
                value: "priedušky;dýchacia sústava");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 18,
                column: "KeyWords",
                value: "dýchacia sústava");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 20,
                column: "KeyWords",
                value: "zápal;hrtan");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 21,
                column: "KeyWords",
                value: "hrtan;zdravý");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 22,
                column: "KeyWords",
                value: "hltan;dýchacia sústava");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 25,
                column: "KeyWords",
                value: "hltan;zdravý");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 26,
                column: "KeyWords",
                value: "zápal;pľúca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 1,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 2,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 4,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 5,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 7,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 8,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 10,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 11,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 12,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 14,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 15,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 17,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 18,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 20,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 21,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 22,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 25,
                column: "KeyWords",
                value: null);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 26,
                column: "KeyWords",
                value: null);
        }
    }
}
