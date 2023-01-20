using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement_AspNetCore.Migrations
{
    public partial class AddPosition_EmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Employees");
        }
    }
}
