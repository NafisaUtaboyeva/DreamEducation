using Microsoft.EntityFrameworkCore.Migrations;

namespace DreamEducation.Data.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Students",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Mentors",
                newName: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Students",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Mentors",
                newName: "Login");
        }
    }
}
