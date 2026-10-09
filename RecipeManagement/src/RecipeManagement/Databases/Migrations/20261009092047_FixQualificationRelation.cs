using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeManagement.Databases.Migrations
{
    /// <inheritdoc />
    public partial class FixQualificationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_kcr_qualification_kcr_role_RoleId1",
                table: "kcr_qualification");

            migrationBuilder.DropIndex(
                name: "IX_kcr_qualification_RoleId1",
                table: "kcr_qualification");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "kcr_qualification");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleId1",
                table: "kcr_qualification",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_kcr_qualification_RoleId1",
                table: "kcr_qualification",
                column: "RoleId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_kcr_qualification_kcr_role_RoleId1",
                table: "kcr_qualification",
                column: "RoleId1",
                principalTable: "kcr_role",
                principalColumn: "kcr_role_id");
        }
    }
}
