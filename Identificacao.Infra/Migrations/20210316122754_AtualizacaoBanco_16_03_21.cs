using Microsoft.EntityFrameworkCore.Migrations;

namespace Identificacao.Infra.Migrations
{
    public partial class AtualizacaoBanco_16_03_21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CEP",
                table: "EMPRESAS",
                type: "VARCHAR(9)",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(8)",
                oldMaxLength: 8,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CEP",
                table: "EMPRESAS",
                type: "VARCHAR(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(9)",
                oldMaxLength: 9,
                oldNullable: true);
        }
    }
}
