using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SECODashBackend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannedTopic",
                columns: table => new
                {
                    Term = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedTopic", x => x.Term);
                });

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
                name: "Taxonomy",
                columns: table => new
                {
                    Term = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy", x => x.Term);
                });

            migrationBuilder.CreateTable(
                name: "Technology",
                columns: table => new
                {
                    Term = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technology", x => x.Term);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BannedTopicEcosystem",
                columns: table => new
                {
                    BannedTopicsTerm = table.Column<string>(type: "text", nullable: false),
                    EcosystemsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedTopicEcosystem", x => new { x.BannedTopicsTerm, x.EcosystemsId });
                    table.ForeignKey(
                        name: "FK_BannedTopicEcosystem_BannedTopic_BannedTopicsTerm",
                        column: x => x.BannedTopicsTerm,
                        principalTable: "BannedTopic",
                        principalColumn: "Term",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BannedTopicEcosystem_Ecosystems_EcosystemsId",
                        column: x => x.EcosystemsId,
                        principalTable: "Ecosystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcosystemTaxonomy",
                columns: table => new
                {
                    EcosystemsId = table.Column<string>(type: "text", nullable: false),
                    TaxonomyTerm = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcosystemTaxonomy", x => new { x.EcosystemsId, x.TaxonomyTerm });
                    table.ForeignKey(
                        name: "FK_EcosystemTaxonomy_Ecosystems_EcosystemsId",
                        column: x => x.EcosystemsId,
                        principalTable: "Ecosystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcosystemTaxonomy_Taxonomy_TaxonomyTerm",
                        column: x => x.TaxonomyTerm,
                        principalTable: "Taxonomy",
                        principalColumn: "Term",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcosystemTechnology",
                columns: table => new
                {
                    EcosystemsId = table.Column<string>(type: "text", nullable: false),
                    TechnologiesTerm = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcosystemTechnology", x => new { x.EcosystemsId, x.TechnologiesTerm });
                    table.ForeignKey(
                        name: "FK_EcosystemTechnology_Ecosystems_EcosystemsId",
                        column: x => x.EcosystemsId,
                        principalTable: "Ecosystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcosystemTechnology_Technology_TechnologiesTerm",
                        column: x => x.TechnologiesTerm,
                        principalTable: "Technology",
                        principalColumn: "Term",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EcosystemUser",
                columns: table => new
                {
                    EcosystemsId = table.Column<string>(type: "text", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcosystemUser", x => new { x.EcosystemsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_EcosystemUser_Ecosystems_EcosystemsId",
                        column: x => x.EcosystemsId,
                        principalTable: "Ecosystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EcosystemUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BannedTopicEcosystem_EcosystemsId",
                table: "BannedTopicEcosystem",
                column: "EcosystemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ecosystems_Name",
                table: "Ecosystems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EcosystemTaxonomy_TaxonomyTerm",
                table: "EcosystemTaxonomy",
                column: "TaxonomyTerm");

            migrationBuilder.CreateIndex(
                name: "IX_EcosystemTechnology_TechnologiesTerm",
                table: "EcosystemTechnology",
                column: "TechnologiesTerm");

            migrationBuilder.CreateIndex(
                name: "IX_EcosystemUser_UsersId",
                table: "EcosystemUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxonomy_Term",
                table: "Taxonomy",
                column: "Term",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Technology_Term",
                table: "Technology",
                column: "Term",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannedTopicEcosystem");

            migrationBuilder.DropTable(
                name: "EcosystemTaxonomy");

            migrationBuilder.DropTable(
                name: "EcosystemTechnology");

            migrationBuilder.DropTable(
                name: "EcosystemUser");

            migrationBuilder.DropTable(
                name: "BannedTopic");

            migrationBuilder.DropTable(
                name: "Taxonomy");

            migrationBuilder.DropTable(
                name: "Technology");

            migrationBuilder.DropTable(
                name: "Ecosystems");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
