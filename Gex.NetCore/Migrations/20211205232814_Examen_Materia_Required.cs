using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gex.Migrations
{
    public partial class Examen_Materia_Required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "materias",
                keyColumn: "nombre",
                keyValue: null,
                column: "nombre",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "materias",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "materia_id",
                table: "examenes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "fk_examenes_materias_materia_id",
                table: "examenes",
                column: "materia_id",
                principalTable: "materias",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_examenes_materias_materia_id",
                table: "examenes");

            migrationBuilder.AlterColumn<string>(
                name: "nombre",
                table: "materias",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "materia_id",
                table: "examenes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_examenes_materias_materia_id",
                table: "examenes",
                column: "materia_id",
                principalTable: "materias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
