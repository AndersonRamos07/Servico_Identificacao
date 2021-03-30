using Microsoft.EntityFrameworkCore.Migrations;

namespace Identificacao.Infra.Migrations
{
    public partial class AtualizacaoBanco_16_03_21_V_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CODIGO_REGIME_TRIBUTAVEL",
                table: "EMPRESAS",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CODIGO_REGIME_TRIBUTAVEL",
                table: "EMPRESAS");
        }
    }
}
