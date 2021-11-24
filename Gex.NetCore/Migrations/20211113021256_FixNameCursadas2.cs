using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gex.NetCore.Migrations
{
    public partial class FixNameCursadas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crusadas_Comisiones_ComisionId",
                table: "Crusadas");

            migrationBuilder.DropForeignKey(
                name: "FK_Crusadas_Materias_MateriaId",
                table: "Crusadas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Crusadas",
                table: "Crusadas");

            migrationBuilder.RenameTable(
                name: "Crusadas",
                newName: "Cursadas");

            migrationBuilder.RenameIndex(
                name: "IX_Crusadas_MateriaId",
                table: "Cursadas",
                newName: "IX_Cursadas_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Crusadas_ComisionId",
                table: "Cursadas",
                newName: "IX_Cursadas_ComisionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cursadas",
                table: "Cursadas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursadas_Comisiones_ComisionId",
                table: "Cursadas",
                column: "ComisionId",
                principalTable: "Comisiones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursadas_Materias_MateriaId",
                table: "Cursadas",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursadas_Comisiones_ComisionId",
                table: "Cursadas");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursadas_Materias_MateriaId",
                table: "Cursadas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cursadas",
                table: "Cursadas");

            migrationBuilder.RenameTable(
                name: "Cursadas",
                newName: "Crusadas");

            migrationBuilder.RenameIndex(
                name: "IX_Cursadas_MateriaId",
                table: "Crusadas",
                newName: "IX_Crusadas_MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Cursadas_ComisionId",
                table: "Crusadas",
                newName: "IX_Crusadas_ComisionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Crusadas",
                table: "Crusadas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Crusadas_Comisiones_ComisionId",
                table: "Crusadas",
                column: "ComisionId",
                principalTable: "Comisiones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Crusadas_Materias_MateriaId",
                table: "Crusadas",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id");
        }
    }
}
