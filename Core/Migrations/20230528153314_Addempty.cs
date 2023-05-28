using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class Addempty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql($"INSERT INTO AspNetRoles VALUES(NewId(), 'SuperAdmin', 'SuperAdmin', NEWID())" +
								 $"INSERT INTO AspNetRoles VALUES(NewId(),'CompanyAdmin','CompanyAdmin',NEWID())" +
								 $"INSERT INTO AspNetRoles VALUES(NewId(),'CompanyStaff','CompanyStaff',NEWID())" +

								 $"INSERT INTO [dbo].[CommonDropDowns] VALUES(3 ,'Male',1,0,'20120618 10:34:09 AM')" +
								 $"INSERT INTO[dbo].[CommonDropDowns] VALUES(3 ,'Female',1,0,'20120618 10:34:09 AM')");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
