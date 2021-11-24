using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gex.NetCore.Migrations
{
    public partial class Auditory_In_Comision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Comisiones",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Comisiones",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModificationById",
                table: "Comisiones",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationDate",
                table: "Comisiones",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Comisiones_CreatedById",
                table: "Comisiones",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Comisiones_LastModificationById",
                table: "Comisiones",
                column: "LastModificationById");

            migrationBuilder.AddForeignKey(
                name: "FK_Comisiones_Usuarios_CreatedById",
                table: "Comisiones",
                column: "CreatedById",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comisiones_Usuarios_LastModificationById",
                table: "Comisiones",
                column: "LastModificationById",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comisiones_Usuarios_CreatedById",
                table: "Comisiones");

            migrationBuilder.DropForeignKey(
                name: "FK_Comisiones_Usuarios_LastModificationById",
                table: "Comisiones");

            migrationBuilder.DropIndex(
                name: "IX_Comisiones_CreatedById",
                table: "Comisiones");

            migrationBuilder.DropIndex(
                name: "IX_Comisiones_LastModificationById",
                table: "Comisiones");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Comisiones");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Comisiones");

            migrationBuilder.DropColumn(
                name: "LastModificationById",
                table: "Comisiones");

            migrationBuilder.DropColumn(
                name: "LastModificationDate",
                table: "Comisiones");
        }
    }
}
