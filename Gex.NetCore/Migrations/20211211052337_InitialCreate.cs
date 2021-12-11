using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gex.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comisiones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ciclo_lectivo = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comisiones", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "materias",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_materias", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email_verified_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salt = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    remember_token = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    profile_photo_path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dni = table.Column<long>(type: "bigint", nullable: true),
                    last_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    user_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    normalized_user_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    normalized_email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email_confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    password_hash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    security_stamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number_confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    access_failed_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cursadas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    comision_id = table.Column<int>(type: "int", nullable: true),
                    materia_id = table.Column<long>(type: "bigint", nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cursadas", x => x.id);
                    table.ForeignKey(
                        name: "fk_cursadas_comisiones_comision_id",
                        column: x => x.comision_id,
                        principalTable: "comisiones",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cursadas_materias_materia_id",
                        column: x => x.materia_id,
                        principalTable: "materias",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "examenes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    materia_id = table.Column<long>(type: "bigint", nullable: true),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    nota_regular = table.Column<int>(type: "int", nullable: false),
                    nota_promo = table.Column<int>(type: "int", nullable: false),
                    recuperatorio = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_examenes", x => x.id);
                    table.ForeignKey(
                        name: "fk_examenes_materias_materia_id",
                        column: x => x.materia_id,
                        principalTable: "materias",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inscripciones_comisiones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    alumno_id = table.Column<int>(type: "int", nullable: true),
                    comision_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inscripciones_comisiones", x => x.id);
                    table.ForeignKey(
                        name: "fk_inscripciones_comisiones_comisiones_comision_id",
                        column: x => x.comision_id,
                        principalTable: "comisiones",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_inscripciones_comisiones_usuarios_alumno_id",
                        column: x => x.alumno_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mesas_examen",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    mostrar_respuestas = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    duracion = table.Column<int>(type: "int", nullable: false),
                    examen_id = table.Column<long>(type: "bigint", nullable: true),
                    profesor_id = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mesas_examen", x => x.id);
                    table.ForeignKey(
                        name: "fk_mesas_examen_examenes_examen_id",
                        column: x => x.examen_id,
                        principalTable: "examenes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_mesas_examen_usuarios_profesor_id",
                        column: x => x.profesor_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "preguntas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tema = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    examen_id = table.Column<long>(type: "bigint", nullable: true),
                    materia_id = table.Column<long>(type: "bigint", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preguntas", x => x.id);
                    table.ForeignKey(
                        name: "fk_preguntas_examenes_examen_id",
                        column: x => x.examen_id,
                        principalTable: "examenes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_preguntas_materias_materia_id",
                        column: x => x.materia_id,
                        principalTable: "materias",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inscripciones_mesas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nota = table.Column<int>(type: "int", nullable: false),
                    condicion = table.Column<int>(type: "int", nullable: false),
                    alumno_id = table.Column<int>(type: "int", nullable: true),
                    mesa_examen_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inscripciones_mesas", x => x.id);
                    table.ForeignKey(
                        name: "fk_inscripciones_mesas_mesas_examen_mesa_examen_id",
                        column: x => x.mesa_examen_id,
                        principalTable: "mesas_examen",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_inscripciones_mesas_usuarios_alumno_id",
                        column: x => x.alumno_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "preguntas_examen",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    mesa_examen_id = table.Column<long>(type: "bigint", nullable: true),
                    pregunta_id = table.Column<long>(type: "bigint", nullable: true),
                    puntos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preguntas_examen", x => x.id);
                    table.ForeignKey(
                        name: "fk_preguntas_examen_mesas_examen_mesa_examen_id",
                        column: x => x.mesa_examen_id,
                        principalTable: "mesas_examen",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_preguntas_examen_preguntas_pregunta_id",
                        column: x => x.pregunta_id,
                        principalTable: "preguntas",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "respuestas",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    valor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correcto = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    pregunta_id = table.Column<long>(type: "bigint", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_respuestas", x => x.id);
                    table.ForeignKey(
                        name: "fk_respuestas_preguntas_pregunta_id",
                        column: x => x.pregunta_id,
                        principalTable: "preguntas",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "respuestas_alumnos",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    alumno_id = table.Column<int>(type: "int", nullable: true),
                    examen_id = table.Column<long>(type: "bigint", nullable: true),
                    respuesta_id = table.Column<long>(type: "bigint", nullable: true),
                    valor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correcto = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_respuestas_alumnos", x => x.id);
                    table.ForeignKey(
                        name: "fk_respuestas_alumnos_mesas_examen_examen_id",
                        column: x => x.examen_id,
                        principalTable: "mesas_examen",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_respuestas_alumnos_respuestas_respuesta_id",
                        column: x => x.respuesta_id,
                        principalTable: "respuestas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_respuestas_alumnos_usuarios_alumno_id",
                        column: x => x.alumno_id,
                        principalTable: "usuarios",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_comisiones_nombre_ciclo_lectivo",
                table: "comisiones",
                columns: new[] { "nombre", "ciclo_lectivo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cursadas_comision_id",
                table: "cursadas",
                column: "comision_id");

            migrationBuilder.CreateIndex(
                name: "ix_cursadas_materia_id",
                table: "cursadas",
                column: "materia_id");

            migrationBuilder.CreateIndex(
                name: "ix_examenes_materia_id",
                table: "examenes",
                column: "materia_id");

            migrationBuilder.CreateIndex(
                name: "ix_inscripciones_comisiones_alumno_id",
                table: "inscripciones_comisiones",
                column: "alumno_id");

            migrationBuilder.CreateIndex(
                name: "ix_inscripciones_comisiones_comision_id",
                table: "inscripciones_comisiones",
                column: "comision_id");

            migrationBuilder.CreateIndex(
                name: "ix_inscripciones_mesas_alumno_id",
                table: "inscripciones_mesas",
                column: "alumno_id");

            migrationBuilder.CreateIndex(
                name: "ix_inscripciones_mesas_mesa_examen_id",
                table: "inscripciones_mesas",
                column: "mesa_examen_id");

            migrationBuilder.CreateIndex(
                name: "ix_materias_nombre",
                table: "materias",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_mesas_examen_examen_id",
                table: "mesas_examen",
                column: "examen_id");

            migrationBuilder.CreateIndex(
                name: "ix_mesas_examen_profesor_id",
                table: "mesas_examen",
                column: "profesor_id");

            migrationBuilder.CreateIndex(
                name: "ix_preguntas_examen_id",
                table: "preguntas",
                column: "examen_id");

            migrationBuilder.CreateIndex(
                name: "ix_preguntas_materia_id",
                table: "preguntas",
                column: "materia_id");

            migrationBuilder.CreateIndex(
                name: "ix_preguntas_examen_mesa_examen_id",
                table: "preguntas_examen",
                column: "mesa_examen_id");

            migrationBuilder.CreateIndex(
                name: "ix_preguntas_examen_pregunta_id",
                table: "preguntas_examen",
                column: "pregunta_id");

            migrationBuilder.CreateIndex(
                name: "ix_respuestas_pregunta_id",
                table: "respuestas",
                column: "pregunta_id");

            migrationBuilder.CreateIndex(
                name: "ix_respuestas_alumnos_alumno_id",
                table: "respuestas_alumnos",
                column: "alumno_id");

            migrationBuilder.CreateIndex(
                name: "ix_respuestas_alumnos_examen_id",
                table: "respuestas_alumnos",
                column: "examen_id");

            migrationBuilder.CreateIndex(
                name: "ix_respuestas_alumnos_respuesta_id",
                table: "respuestas_alumnos",
                column: "respuesta_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cursadas");

            migrationBuilder.DropTable(
                name: "inscripciones_comisiones");

            migrationBuilder.DropTable(
                name: "inscripciones_mesas");

            migrationBuilder.DropTable(
                name: "preguntas_examen");

            migrationBuilder.DropTable(
                name: "respuestas_alumnos");

            migrationBuilder.DropTable(
                name: "comisiones");

            migrationBuilder.DropTable(
                name: "mesas_examen");

            migrationBuilder.DropTable(
                name: "respuestas");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "preguntas");

            migrationBuilder.DropTable(
                name: "examenes");

            migrationBuilder.DropTable(
                name: "materias");
        }
    }
}
