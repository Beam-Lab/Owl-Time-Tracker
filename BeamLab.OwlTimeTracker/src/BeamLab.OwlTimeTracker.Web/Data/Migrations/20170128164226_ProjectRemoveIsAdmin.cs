using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeamLab.OwlTimeTracker.Web.Data.Migrations
{
    public partial class ProjectRemoveIsAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AdminUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AdminUserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AdminUserId",
                table: "Projects");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "ProjectsUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "ProjectsUsers");

            migrationBuilder.AddColumn<string>(
                name: "AdminUserId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AdminUserId",
                table: "Projects",
                column: "AdminUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AdminUserId",
                table: "Projects",
                column: "AdminUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
