using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhonBook.infra.Data.sqlserver.Migrations
{
    public partial class MyFirstMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Organiztion",
                table: "Grops",
                newName: "Organization");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Organization",
                table: "Grops",
                newName: "Organiztion");
        }
    }
}
