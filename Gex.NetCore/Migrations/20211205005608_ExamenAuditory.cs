using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gex.Migrations
{
    public partial class ExamenAuditory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_creacion",
                table: "examenes");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "examenes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "estado",
                table: "examenes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "examenes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "examenes");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "examenes");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "examenes");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_creacion",
                table: "examenes",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
