using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddLocationTodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CommonDropdowns_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "CommonDropdowns");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbbreviatedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_locations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_UserId",
                table: "Departments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_locations_UserId",
                table: "locations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CommonDropdowns_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalTable: "CommonDropdowns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_UserId",
                table: "Departments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CommonDropdowns_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_UserId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropIndex(
                name: "IX_Departments_UserId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Departments");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "CommonDropdowns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CommonDropdowns_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalTable: "CommonDropdowns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_CompanyId",
                table: "Departments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
