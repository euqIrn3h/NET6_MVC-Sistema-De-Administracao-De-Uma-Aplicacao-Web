using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_MVC.Migrations
{
    public partial class AdicionandoCampoFotoPerfilTabelaUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathFotoPerfil",
                table: "Usuarios",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
