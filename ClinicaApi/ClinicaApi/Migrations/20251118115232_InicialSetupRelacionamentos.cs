using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaApi.Migrations
{
    /// <inheritdoc />
    public partial class InicialSetupRelacionamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClinicaId",
                table: "Medicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_ClinicaId",
                table: "Medicos",
                column: "ClinicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Clinicas_ClinicaId",
                table: "Medicos",
                column: "ClinicaId",
                principalTable: "Clinicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Clinicas_ClinicaId",
                table: "Medicos");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_ClinicaId",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "ClinicaId",
                table: "Medicos");
        }
    }
}
