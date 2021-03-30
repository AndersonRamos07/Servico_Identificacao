using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identificacao.Infra.Migrations
{
    public partial class criando_banco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESAS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNPJ = table.Column<string>(type: "VARCHAR(18)", maxLength: 18, nullable: true),
                    RAZAO_SOCIAL = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    CEP = table.Column<string>(type: "VARCHAR(8)", maxLength: 8, nullable: true),
                    LOGRADOURO = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    NUMERO = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    COMPLEMENTO = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    BAIRRO = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    COD_CIDADE = table.Column<string>(type: "VARCHAR(7)", maxLength: 7, nullable: true),
                    CIDADE = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    UF = table.Column<string>(type: "VARCHAR(2)", maxLength: 2, nullable: true),
                    INSCRICAO_ESTADUAL = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: true),
                    ACCESS_TOKEN = table.Column<Guid>(type: "CHAR(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESAS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    ULTIMO_NOME = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    SENHA = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: true),
                    CELULAR = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    STATUS = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    SUPERVISOR = table.Column<string>(type: "char(1)", maxLength: 1, nullable: true),
                    ID_EMPRESA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID_USUARIO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPRESAS");

            migrationBuilder.DropTable(
                name: "USUARIOS");
        }
    }
}
