using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HIPA_BE.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFlagSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FlagTypes",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FlagTypes",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FlagTypes",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FlagTypes",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FlagTypes",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 2,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 3,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3660));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 4,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 5,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 6,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 7,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 8,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 9,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 10,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 11,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 12,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 13,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3680));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 14,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3720));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 15,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3760));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 16,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 17,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 18,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 19,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 20,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 21,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 22,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 23,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 24,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 25,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 26,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 27,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 28,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 29,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 30,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 31,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 32,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 33,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 34,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 35,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 36,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 37,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 38,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 39,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 40,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 41,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 42,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 43,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 44,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 45,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 46,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 47,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3810));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 48,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 49,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 50,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 51,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 52,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 53,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 54,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 55,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 56,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3820));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 57,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 58,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 59,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 60,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 61,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 62,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 63,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 64,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 65,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 66,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 67,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 68,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 69,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 70,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 71,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 72,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 73,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 74,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 75,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 76,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 77,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 78,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 79,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 80,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 81,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 82,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 83,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3850));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 84,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 85,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 86,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 87,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 88,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 89,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 90,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 91,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 92,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3870));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 93,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3880));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 94,
                column: "LastModified",
                value: new DateTime(2025, 5, 19, 18, 49, 34, 48, DateTimeKind.Utc).AddTicks(3880));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FlagTypes",
                columns: new[] { "ID", "Color", "Name" },
                values: new object[,]
                {
                    { 1, "#FF0000", "Red" },
                    { 2, "#FFFF00", "Yellow" },
                    { 3, "#00FF00", "Green" },
                    { 4, "#0000FF", "Blue" },
                    { 5, "#800080", "Purple" }
                });

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3910));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 2,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3910));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 3,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3910));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 4,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3910));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 5,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 6,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 7,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 8,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 9,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 10,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 11,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 12,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 13,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 14,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 15,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 16,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 17,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 18,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 19,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 20,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 21,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 22,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3930));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 23,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 24,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 25,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 26,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 27,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 28,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 29,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 30,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3940));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 31,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 32,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 33,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 34,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 35,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 36,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 37,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 38,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 39,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 40,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 41,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3970));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 42,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3970));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 43,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3970));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 44,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3970));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 45,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3970));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 46,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3970));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 47,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3970));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 48,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3970));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 49,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 50,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 51,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 52,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 53,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 54,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 55,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 56,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 57,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 58,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 59,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 60,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 61,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 62,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 63,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 64,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 65,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 66,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 67,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 68,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 69,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 70,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(3990));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 71,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 72,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 73,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 74,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 75,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 76,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 77,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 78,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 79,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 80,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 81,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 82,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4000));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 83,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 84,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 85,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 86,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 87,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 88,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 89,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 90,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 91,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 92,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 93,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 94,
                column: "LastModified",
                value: new DateTime(2025, 5, 5, 18, 47, 30, 809, DateTimeKind.Utc).AddTicks(4030));
        }
    }
}
