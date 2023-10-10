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
                name: "Ecosystems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    NumberOfStars = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecosystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Owner = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Topics = table.Column<List<string>>(type: "text[]", nullable: false),
                    TotalSize = table.Column<int>(type: "integer", nullable: true),
                    ReadMe = table.Column<string>(type: "text", nullable: true),
                    NumberOfStars = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcosystemProject",
                columns: table => new
                {
                    EcosystemsId = table.Column<string>(type: "text", nullable: false),
                    ProjectsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcosystemProject", x => new { x.EcosystemsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_EcosystemProject_Ecosystems_EcosystemsId",
                        column: x => x.EcosystemsId,
                        principalTable: "Ecosystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcosystemProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_EcosystemProject_ProjectsId",
                table: "EcosystemProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ecosystems_Name",
                table: "Ecosystems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgrammingLanguage_ProjectId",
                table: "ProjectProgrammingLanguage",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EcosystemProject");

            migrationBuilder.DropTable(
                name: "ProjectProgrammingLanguage");

            migrationBuilder.DropTable(
                name: "Ecosystems");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
