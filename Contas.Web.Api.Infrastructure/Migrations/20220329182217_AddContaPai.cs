using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Web.Api.Infrastructure.Migrations
{
    public partial class AddContaPai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CONTA_PAI",
                table: "TB_PLANOCONTAS",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CONTA_PAI",
                table: "TB_PLANOCONTAS");
        }
    }
}
