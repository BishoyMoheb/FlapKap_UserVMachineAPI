using Microsoft.EntityFrameworkCore.Migrations;

namespace FUser.CLDataAccess.Migrations
{
    public partial class Mig_MFUVM_Modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "DbS_Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "DbS_Products");
        }
    }
}
