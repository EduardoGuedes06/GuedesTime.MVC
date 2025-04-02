using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuedesTime.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase_Intituição : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InstituicaoId",
                table: "Turmas",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "InstituicaoId",
                table: "Salas",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "InstituicaoId",
                table: "Professores",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "InstituicaoId",
                table: "PlanejamentoDeAulas",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "InstituicaoId1",
                table: "PlanejamentoDeAulas",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "InstituicaoId",
                table: "Horarios",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "InstituicaoId",
                table: "Disciplinas",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Instituicoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNPJ = table.Column<string>(type: "varchar(100)", maxLength: 18, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Endereco = table.Column<string>(type: "varchar(100)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicoes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Feriados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Recorrente = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InstituicaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    InstituicaoId1 = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feriados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feriados_Instituicoes_InstituicaoId",
                        column: x => x.InstituicaoId,
                        principalTable: "Instituicoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feriados_Instituicoes_InstituicaoId1",
                        column: x => x.InstituicaoId1,
                        principalTable: "Instituicoes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_InstituicaoId",
                table: "Turmas",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Salas_InstituicaoId",
                table: "Salas",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Professores_InstituicaoId",
                table: "Professores",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoDeAulas_InstituicaoId",
                table: "PlanejamentoDeAulas",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanejamentoDeAulas_InstituicaoId1",
                table: "PlanejamentoDeAulas",
                column: "InstituicaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_InstituicaoId",
                table: "Horarios",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_InstituicaoId",
                table: "Disciplinas",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Feriados_InstituicaoId",
                table: "Feriados",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Feriados_InstituicaoId1",
                table: "Feriados",
                column: "InstituicaoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplinas_Instituicoes_InstituicaoId",
                table: "Disciplinas",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Horarios_Instituicoes_InstituicaoId",
                table: "Horarios",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentoDeAulas_Instituicoes_InstituicaoId",
                table: "PlanejamentoDeAulas",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanejamentoDeAulas_Instituicoes_InstituicaoId1",
                table: "PlanejamentoDeAulas",
                column: "InstituicaoId1",
                principalTable: "Instituicoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_Instituicoes_InstituicaoId",
                table: "Professores",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salas_Instituicoes_InstituicaoId",
                table: "Salas",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Instituicoes_InstituicaoId",
                table: "Turmas",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplinas_Instituicoes_InstituicaoId",
                table: "Disciplinas");

            migrationBuilder.DropForeignKey(
                name: "FK_Horarios_Instituicoes_InstituicaoId",
                table: "Horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentoDeAulas_Instituicoes_InstituicaoId",
                table: "PlanejamentoDeAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanejamentoDeAulas_Instituicoes_InstituicaoId1",
                table: "PlanejamentoDeAulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Professores_Instituicoes_InstituicaoId",
                table: "Professores");

            migrationBuilder.DropForeignKey(
                name: "FK_Salas_Instituicoes_InstituicaoId",
                table: "Salas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Instituicoes_InstituicaoId",
                table: "Turmas");

            migrationBuilder.DropTable(
                name: "Feriados");

            migrationBuilder.DropTable(
                name: "Instituicoes");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_InstituicaoId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Salas_InstituicaoId",
                table: "Salas");

            migrationBuilder.DropIndex(
                name: "IX_Professores_InstituicaoId",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_PlanejamentoDeAulas_InstituicaoId",
                table: "PlanejamentoDeAulas");

            migrationBuilder.DropIndex(
                name: "IX_PlanejamentoDeAulas_InstituicaoId1",
                table: "PlanejamentoDeAulas");

            migrationBuilder.DropIndex(
                name: "IX_Horarios_InstituicaoId",
                table: "Horarios");

            migrationBuilder.DropIndex(
                name: "IX_Disciplinas_InstituicaoId",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "InstituicaoId",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "InstituicaoId",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "InstituicaoId",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "InstituicaoId",
                table: "PlanejamentoDeAulas");

            migrationBuilder.DropColumn(
                name: "InstituicaoId1",
                table: "PlanejamentoDeAulas");

            migrationBuilder.DropColumn(
                name: "InstituicaoId",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "InstituicaoId",
                table: "Disciplinas");
        }
    }
}
