using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabricaDeSorrisos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarRespostaAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataResposta",
                table: "Comentarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resposta",
                table: "Comentarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataResposta",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "Resposta",
                table: "Comentarios");
        }
    }
}
