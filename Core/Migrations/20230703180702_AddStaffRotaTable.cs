using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddStaffRotaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffRotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RotaObjectString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffRotas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffRotas_UserId",
                table: "StaffRotas",
                column: "UserId");

			migrationBuilder.Sql($"INSERT INTO AspNetRoles VALUES(NewId(), 'SuperAdmin', 'SuperAdmin', NEWID())" +
					             $"INSERT INTO AspNetRoles VALUES(NewId(), 'CompanyAdmin', 'CompanyAdmin',NEWID())" +
					             $"INSERT INTO AspNetRoles VALUES(NewId(), 'CompanyStaff', 'CompanyStaff',NEWID())");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffRotas");
        }
    }
}
