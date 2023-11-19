using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LockedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LockedDate",
                table: "Tickets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LockedUserId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_LockedUserId",
                table: "Tickets",
                column: "LockedUserId",
                unique: true,
                filter: "[LockedUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_LockedUserId",
                table: "Tickets",
                column: "LockedUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_LockedUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_LockedUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "LockedDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "LockedUserId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "AssignedToId",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_AssignedToId",
                table: "Tickets",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
