using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.MVC.Migrations
{
    public partial class AddRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Restaurants",
               columns: table => new
               {
                   Id = table.Column<string>(nullable: false),
                   Name = table.Column<string>(nullable: false),
                   Address = table.Column<string>(maxLength: 256, nullable: true),
                   Logo = table.Column<byte[]>(nullable: true),
                   ConcurrencyStamp = table.Column<string>(nullable: true)
                   
               }
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
