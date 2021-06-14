using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Gex.NetCore.Migrations
{
    public partial class IdentityUser_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesas_Users_ProfesorId",
                table: "Mesas");

            migrationBuilder.DropForeignKey(
                name: "FK_MesasAlumnos_Users_AlumnoId",
                table: "MesasAlumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasAlumnos_Users_AlumnoId",
                table: "RespuestasAlumnos");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Migrations");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropIndex(
                name: "IX_RespuestasAlumnos_AlumnoId",
                table: "RespuestasAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_MesasAlumnos_AlumnoId",
                table: "MesasAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_Mesas_ProfesorId",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlumnoId1",
                table: "RespuestasAlumnos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlumnoId1",
                table: "MesasAlumnos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfesorId1",
                table: "Mesas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasAlumnos_AlumnoId1",
                table: "RespuestasAlumnos",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_MesasAlumnos_AlumnoId1",
                table: "MesasAlumnos",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_ProfesorId1",
                table: "Mesas",
                column: "ProfesorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Mesas_Users_ProfesorId1",
                table: "Mesas",
                column: "ProfesorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MesasAlumnos_Users_AlumnoId1",
                table: "MesasAlumnos",
                column: "AlumnoId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasAlumnos_Users_AlumnoId1",
                table: "RespuestasAlumnos",
                column: "AlumnoId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesas_Users_ProfesorId1",
                table: "Mesas");

            migrationBuilder.DropForeignKey(
                name: "FK_MesasAlumnos_Users_AlumnoId1",
                table: "MesasAlumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_RespuestasAlumnos_Users_AlumnoId1",
                table: "RespuestasAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_RespuestasAlumnos_AlumnoId1",
                table: "RespuestasAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_MesasAlumnos_AlumnoId1",
                table: "MesasAlumnos");

            migrationBuilder.DropIndex(
                name: "IX_Mesas_ProfesorId1",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AlumnoId1",
                table: "RespuestasAlumnos");

            migrationBuilder.DropColumn(
                name: "AlumnoId1",
                table: "MesasAlumnos");

            migrationBuilder.DropColumn(
                name: "ProfesorId1",
                table: "Mesas");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<short>(type: "bit", nullable: false),
                    FacebookId = table.Column<long>(type: "bigint", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    LockoutEnabled = table.Column<short>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<short>(type: "bit", nullable: false),
                    PictureUrl = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    TwoFactorEnabled = table.Column<short>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Migrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Batch = table.Column<int>(type: "int", nullable: false),
                    Migration = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Migrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    LastActivity = table.Column<int>(type: "int", nullable: false),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    UserAgent = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    IdentityId = table.Column<string>(type: "varchar(767)", nullable: true),
                    Locale = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AppUser_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasAlumnos_AlumnoId",
                table: "RespuestasAlumnos",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesasAlumnos_AlumnoId",
                table: "MesasAlumnos",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_ProfesorId",
                table: "Mesas",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdentityId",
                table: "Customers",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mesas_Users_ProfesorId",
                table: "Mesas",
                column: "ProfesorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MesasAlumnos_Users_AlumnoId",
                table: "MesasAlumnos",
                column: "AlumnoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RespuestasAlumnos_Users_AlumnoId",
                table: "RespuestasAlumnos",
                column: "AlumnoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
