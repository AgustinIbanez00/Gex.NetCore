using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Gex.NetCore.Migrations
{
    public partial class InitialSnapShot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Comision = table.Column<string>(type: "text", nullable: true),
                    Cuatrimestre = table.Column<int>(type: "int", nullable: false),
                    CicloLectivo = table.Column<int>(type: "int", nullable: true),
                    CantAlumnos = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FailedJobs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Uuid = table.Column<string>(type: "text", nullable: true),
                    Connection = table.Column<string>(type: "text", nullable: true),
                    Queue = table.Column<string>(type: "text", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    Exception = table.Column<string>(type: "text", nullable: true),
                    FailedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailedJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalAccessTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TokenableType = table.Column<string>(type: "text", nullable: true),
                    TokenableId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    Abilities = table.Column<string>(type: "text", nullable: true),
                    LastUsedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAccessTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(767)", nullable: false),
                    EmailVerifiedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    RememberToken = table.Column<string>(type: "text", nullable: true),
                    ProfilePhotoPath = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    Dni = table.Column<long>(type: "bigint", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Observation = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Examenes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CursoId = table.Column<long>(type: "bigint", nullable: true),
                    MateriaId = table.Column<long>(type: "bigint", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    MinTardanza = table.Column<int>(type: "int", nullable: true),
                    NotaRegular = table.Column<int>(type: "int", nullable: true),
                    NotaPromo = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examenes_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examenes_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MateriasCursos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CursoId = table.Column<long>(type: "bigint", nullable: true),
                    MateriaId = table.Column<long>(type: "bigint", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasCursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriasCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MateriasCursos_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ProfesorId = table.Column<long>(type: "bigint", nullable: true),
                    ExamenId = table.Column<long>(type: "bigint", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    MostrarRespuestas = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    ProfesorId1 = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mesas_Examenes_ExamenId",
                        column: x => x.ExamenId,
                        principalTable: "Examenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mesas_Users_ProfesorId1",
                        column: x => x.ProfesorId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ExamenId = table.Column<long>(type: "bigint", nullable: true),
                    Valor = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: true),
                    Puntos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preguntas_Examenes_ExamenId",
                        column: x => x.ExamenId,
                        principalTable: "Examenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesasAlumnos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AlumnoId = table.Column<long>(type: "bigint", nullable: true),
                    MesaId = table.Column<long>(type: "bigint", nullable: true),
                    Nota = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    AlumnoId1 = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesasAlumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesasAlumnos_Mesas_MesaId",
                        column: x => x.MesaId,
                        principalTable: "Mesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesasAlumnos_Users_AlumnoId1",
                        column: x => x.AlumnoId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PreguntaId = table.Column<long>(type: "bigint", nullable: true),
                    Valor = table.Column<string>(type: "text", nullable: true),
                    Correcto = table.Column<byte>(type: "tinyint unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respuestas_Preguntas_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Preguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasAlumnos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AlumnoId = table.Column<long>(type: "bigint", nullable: true),
                    RespuestaId = table.Column<long>(type: "bigint", nullable: true),
                    Valor = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    AlumnoId1 = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasAlumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespuestasAlumnos_Respuestas_RespuestaId",
                        column: x => x.RespuestaId,
                        principalTable: "Respuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespuestasAlumnos_Users_AlumnoId1",
                        column: x => x.AlumnoId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examenes_CursoId",
                table: "Examenes",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Examenes_MateriaId",
                table: "Examenes",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasCursos_CursoId",
                table: "MateriasCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasCursos_MateriaId",
                table: "MateriasCursos",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_ExamenId",
                table: "Mesas",
                column: "ExamenId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_ProfesorId1",
                table: "Mesas",
                column: "ProfesorId1");

            migrationBuilder.CreateIndex(
                name: "IX_MesasAlumnos_AlumnoId1",
                table: "MesasAlumnos",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_MesasAlumnos_MesaId",
                table: "MesasAlumnos",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_ExamenId",
                table: "Preguntas",
                column: "ExamenId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_PreguntaId",
                table: "Respuestas",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasAlumnos_AlumnoId1",
                table: "RespuestasAlumnos",
                column: "AlumnoId1");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasAlumnos_RespuestaId",
                table: "RespuestasAlumnos",
                column: "RespuestaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FailedJobs");

            migrationBuilder.DropTable(
                name: "MateriasCursos");

            migrationBuilder.DropTable(
                name: "MesasAlumnos");

            migrationBuilder.DropTable(
                name: "PersonalAccessTokens");

            migrationBuilder.DropTable(
                name: "RespuestasAlumnos");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "Examenes");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Materias");
        }
    }
}
