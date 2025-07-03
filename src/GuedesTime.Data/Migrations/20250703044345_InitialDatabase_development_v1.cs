using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuedesTime.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase_development_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Turmas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Turmas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Turmas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Tarefas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Tarefas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Tarefas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Series",
                type: "varchar(100)",
                maxLength: 10,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Series",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Series",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Salas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Salas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Salas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Restricoes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Restricoes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Restricoes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "ProfessoresDisciplinasTurmas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "ProfessoresDisciplinasTurmas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "ProfessoresDisciplinasTurmas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Professores",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Professores",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "PlanejamentosDeAula",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "PlanejamentosDeAula",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PlanejamentosDeAula",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "PlanejamentoDeAulaItens",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "PlanejamentoDeAulaItens",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PlanejamentoDeAulaItens",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Logs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Logs",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Logs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Instituicoes",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Instituicoes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Horarios",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Horarios",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Horarios",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "HistoricoExportacao",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "HistoricoExportacao",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "HistoricoExportacao",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Feriados",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Feriados",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Feriados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Enderecos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Enderecos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Enderecos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "DisciplinasSeries",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "DisciplinasSeries",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "DisciplinasSeries",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "DisciplinasProfessores",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "DisciplinasProfessores",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "DisciplinasProfessores",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Disciplinas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Disciplinas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "ConfiguracoesGenericas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "ConfiguracoesGenericas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "ConfiguracoesGenericas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Atividades",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Atividades",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Atividades",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Restricoes");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "ProfessoresDisciplinasTurmas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "ProfessoresDisciplinasTurmas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "ProfessoresDisciplinasTurmas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "PlanejamentosDeAula");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "PlanejamentosDeAula");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PlanejamentosDeAula");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "PlanejamentoDeAulaItens");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "PlanejamentoDeAulaItens");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PlanejamentoDeAulaItens");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Instituicoes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Instituicoes");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Horarios");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "HistoricoExportacao");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "HistoricoExportacao");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "HistoricoExportacao");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Feriados");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Feriados");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Feriados");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "DisciplinasSeries");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "DisciplinasSeries");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "DisciplinasSeries");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "DisciplinasProfessores");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "DisciplinasProfessores");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "DisciplinasProfessores");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Disciplinas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "ConfiguracoesGenericas");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "ConfiguracoesGenericas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "ConfiguracoesGenericas");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Atividades");
        }
    }
}
