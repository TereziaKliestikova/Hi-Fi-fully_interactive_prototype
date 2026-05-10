using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HIPA_BE.Migrations
{
    /// <inheritdoc />
    public partial class DirectoryAndConverter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectoryId",
                table: "PdfFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Directories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StudyCategory = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    NestingLevel = table.Column<int>(type: "integer", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    KeyWords = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directories_Directories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectorySampleImage",
                columns: table => new
                {
                    ParentDirectoriesId = table.Column<int>(type: "integer", nullable: false),
                    SampleImagesID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorySampleImage", x => new { x.ParentDirectoriesId, x.SampleImagesID });
                    table.ForeignKey(
                        name: "FK_DirectorySampleImage_Directories_ParentDirectoriesId",
                        column: x => x.ParentDirectoriesId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectorySampleImage_SampleImages_SampleImagesID",
                        column: x => x.SampleImagesID,
                        principalTable: "SampleImages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "PdfFiles",
                keyColumn: "ID",
                keyValue: 1,
                column: "DirectoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PdfFiles",
                keyColumn: "ID",
                keyValue: 2,
                column: "DirectoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PdfFiles",
                keyColumn: "ID",
                keyValue: 3,
                column: "DirectoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PdfFiles",
                keyColumn: "ID",
                keyValue: 4,
                column: "DirectoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PdfFiles",
                keyColumn: "ID",
                keyValue: 5,
                column: "DirectoryId",
                value: null);

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

            migrationBuilder.CreateIndex(
                name: "IX_PdfFiles_DirectoryId",
                table: "PdfFiles",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Directories_ParentId",
                table: "Directories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorySampleImage_SampleImagesID",
                table: "DirectorySampleImage",
                column: "SampleImagesID");

            migrationBuilder.AddForeignKey(
                name: "FK_PdfFiles_Directories_DirectoryId",
                table: "PdfFiles",
                column: "DirectoryId",
                principalTable: "Directories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PdfFiles_Directories_DirectoryId",
                table: "PdfFiles");

            migrationBuilder.DropTable(
                name: "DirectorySampleImage");

            migrationBuilder.DropTable(
                name: "Directories");

            migrationBuilder.DropIndex(
                name: "IX_PdfFiles_DirectoryId",
                table: "PdfFiles");

            migrationBuilder.DropColumn(
                name: "DirectoryId",
                table: "PdfFiles");

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5148));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 2,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5151));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 3,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5152));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 4,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5154));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 5,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5155));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 6,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5156));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 7,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5157));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 8,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5158));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 9,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5159));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 10,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5160));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 11,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5160));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 12,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5161));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 13,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5162));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 14,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5163));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 15,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5164));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 16,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5165));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 17,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5166));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 18,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5167));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 19,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 20,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5242));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 21,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5244));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 22,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5245));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 23,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5246));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 24,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5246));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 25,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5247));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 26,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5248));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 27,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5253));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 28,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5254));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 29,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5255));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 30,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5256));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 31,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5257));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 32,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5258));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 33,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5259));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 34,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5259));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 35,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5260));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 36,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5261));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 37,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5261));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 38,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5262));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 39,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5263));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 40,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5263));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 41,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5264));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 42,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5265));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 43,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5266));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 44,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5266));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 45,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5267));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 46,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5313));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 47,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5313));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 48,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5315));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 49,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5317));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 50,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5318));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 51,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5319));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 52,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5319));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 53,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5320));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 54,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5321));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 55,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5322));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 56,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5322));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 57,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5323));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 58,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5324));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 59,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5325));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 60,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 61,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 62,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5327));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 63,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5328));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 64,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5329));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 65,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5329));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 66,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 67,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5331));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 68,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5333));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 69,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5333));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 70,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5378));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 71,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5379));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 72,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5380));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 73,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5381));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 74,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5382));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 75,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5383));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 76,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5384));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 77,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5385));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 78,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5386));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 79,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5387));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 80,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5388));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 81,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5388));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 82,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5389));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 83,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5390));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 84,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5390));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 85,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5391));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 86,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5392));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 87,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5393));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 88,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5393));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 89,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 90,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5395));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 91,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5396));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 92,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5396));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 93,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5397));

            migrationBuilder.UpdateData(
                table: "SampleImages",
                keyColumn: "ID",
                keyValue: 94,
                column: "LastModified",
                value: new DateTime(2025, 4, 20, 19, 19, 18, 682, DateTimeKind.Utc).AddTicks(5398));
        }
    }
}
