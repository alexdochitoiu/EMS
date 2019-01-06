using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddIncidentsAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Incidents",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Incidents",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Incidents",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "ReporterId",
                table: "Incidents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Severity",
                table: "Incidents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Incidents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ReporterId",
                table: "Incidents",
                column: "ReporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Users_ReporterId",
                table: "Incidents",
                column: "ReporterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Users_ReporterId",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_ReporterId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "ReporterId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Severity",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Incidents");
        }
    }
}
