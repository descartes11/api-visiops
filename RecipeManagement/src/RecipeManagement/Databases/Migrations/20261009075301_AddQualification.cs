using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeManagement.Databases.Migrations
{
    /// <inheritdoc />
    public partial class AddQualification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "minimum_qualifications",
                table: "kcr_role");

            migrationBuilder.CreateTable(
                name: "kcr_qualification",
                columns: table => new
                {
                    kcr_qualification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    kcr_role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    minimum_recruitment_requirements = table.Column<string>(type: "text", nullable: true),
                    recruitment_requirements_verification = table.Column<string>(type: "text", nullable: true),
                    required_performance_thresholds = table.Column<string>(type: "text", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true),
                    specific_skills_and_knowledge = table.Column<string>(type: "text", nullable: true),
                    training_modules = table.Column<string>(type: "text", nullable: true),
                    training_mode = table.Column<string>(type: "text", nullable: true),
                    authorized_training_personnel = table.Column<string>(type: "text", nullable: true),
                    minimum_competency_verification = table.Column<string>(type: "text", nullable: true),
                    initial_training_performance_threshold = table.Column<string>(type: "text", nullable: true),
                    recurrent_check_performance_threshold = table.Column<string>(type: "text", nullable: true),
                    training_framework = table.Column<string>(type: "text", nullable: true),
                    RoleId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kcr_qualification", x => x.kcr_qualification_id);
                    table.ForeignKey(
                        name: "FK_kcr_qualification_kcr_role_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "kcr_role",
                        principalColumn: "kcr_role_id");
                    table.ForeignKey(
                        name: "FK_kcr_qualification_kcr_role_kcr_role_id",
                        column: x => x.kcr_role_id,
                        principalTable: "kcr_role",
                        principalColumn: "kcr_role_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kcr_qualification_kcr_role_id",
                table: "kcr_qualification",
                column: "kcr_role_id",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_kcr_qualification_RoleId1",
                table: "kcr_qualification",
                column: "RoleId1",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kcr_qualification");

            migrationBuilder.AddColumn<string>(
                name: "minimum_qualifications",
                table: "kcr_role",
                type: "text",
                nullable: true);
        }
    }
}
