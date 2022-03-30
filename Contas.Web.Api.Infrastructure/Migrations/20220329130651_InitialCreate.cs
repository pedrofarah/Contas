using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contas.Web.Api.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_PLANOCONTAS",
                columns: table => new
                {
                    CODIGO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TIPO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ACEITA_LANCAMENTOS = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PLANOCONTAS", x => x.CODIGO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PLANOCONTAS");
        }
    }
}
