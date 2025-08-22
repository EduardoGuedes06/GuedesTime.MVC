using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuedesTime.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase_AAA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Turmas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Professores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Disciplinas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Disciplinas");
        }
    }
}
