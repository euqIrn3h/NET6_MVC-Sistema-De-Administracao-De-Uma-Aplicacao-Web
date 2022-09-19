using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admin_MVC.Migrations
{
    public partial class AlterandoTabelaDeTermos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TermosDeUso",
                table: "Termos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TermosDePrivacidade",
                table: "Termos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Sobre",
                table: "Termos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Termos",
                keyColumn: "TermosDeUso",
                keyValue: null,
                column: "TermosDeUso",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TermosDeUso",
                table: "Termos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Termos",
                keyColumn: "TermosDePrivacidade",
                keyValue: null,
                column: "TermosDePrivacidade",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TermosDePrivacidade",
                table: "Termos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Termos",
                keyColumn: "Sobre",
                keyValue: null,
                column: "Sobre",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Sobre",
                table: "Termos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
