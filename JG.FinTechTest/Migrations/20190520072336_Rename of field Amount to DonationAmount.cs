using Microsoft.EntityFrameworkCore.Migrations;

namespace JG.FinTechTest.Migrations
{
    public partial class RenameoffieldAmounttoDonationAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "GiftAidDeclaration",
                newName: "DonationAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DonationAmount",
                table: "GiftAidDeclaration",
                newName: "Amount");
        }
    }
}
