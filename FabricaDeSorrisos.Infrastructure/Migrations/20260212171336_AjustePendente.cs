using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FabricaDeSorrisos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjustePendente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "UsuariosDoSistema");

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "UsuariosDoSistema",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
