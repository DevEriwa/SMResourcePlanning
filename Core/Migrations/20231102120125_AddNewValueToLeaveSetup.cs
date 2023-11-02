using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddNewValueToLeaveSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxStaff",
                table: "shift",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShiftId",
                table: "LeaveSetups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "LeaveApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_LeaveSetups_ShiftId",
                table: "LeaveSetups",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveSetups_shift_ShiftId",
                table: "LeaveSetups",
                column: "ShiftId",
                principalTable: "shift",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveSetups_shift_ShiftId",
                table: "LeaveSetups");

            migrationBuilder.DropIndex(
                name: "IX_LeaveSetups_ShiftId",
                table: "LeaveSetups");

            migrationBuilder.DropColumn(
                name: "MaxStaff",
                table: "shift");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "LeaveSetups");

            migrationBuilder.DropColumn(
                name: "DateApproved",
                table: "LeaveApplications");
        }
    }
}
