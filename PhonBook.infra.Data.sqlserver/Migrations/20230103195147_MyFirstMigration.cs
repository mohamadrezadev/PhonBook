using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhonBook.infra.Data.sqlserver.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POST = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tell_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_number1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_number2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organiztion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tell_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code_posti = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<int>(type: "int", nullable: false),
                    Contact_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactGrop",
                columns: table => new
                {
                    Contact_id = table.Column<int>(type: "int", nullable: false),
                    Group_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactGrop", x => new { x.Contact_id, x.Group_Id });
                    table.ForeignKey(
                        name: "FK_ContactGrop_Contacts_Group_Id",
                        column: x => x.Group_Id,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactGrop_Grops_Contact_id",
                        column: x => x.Contact_id,
                        principalTable: "Grops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactGrop_Group_Id",
                table: "ContactGrop",
                column: "Group_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactGrop");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Grops");
        }
    }
}
