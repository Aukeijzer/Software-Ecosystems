using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SECODashBackend.Migrations
{
    /// <inheritdoc />
    public partial class initialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Owner = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Topics = table.Column<List<string>>(type: "text[]", nullable: false),
                    TotalSize = table.Column<int>(type: "integer", nullable: true),
                    ReadMe = table.Column<string>(type: "text", nullable: true),
                    NumberOfStars = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ecosystems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    NumberOfStars = table.Column<int>(type: "integer", nullable: true),
                    ProjectId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecosystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ecosystems_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectProgrammingLanguage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<int>(type: "integer", nullable: false),
                    Percentage = table.Column<float>(type: "real", nullable: false),
                    ProjectId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProgrammingLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectProgrammingLanguage_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ecosystems_Name",
                table: "Ecosystems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ecosystems_ProjectId",
                table: "Ecosystems",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguage_ProjectId",
                table: "ProjectProgrammingLanguage",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ecosystems");

            migrationBuilder.DropTable(
                name: "ProjectProgrammingLanguage");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
