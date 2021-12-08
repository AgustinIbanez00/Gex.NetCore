﻿// <auto-generated />
using System;
using Gex;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gex.Migrations
{
    [DbContext(typeof(GexContext))]
    [Migration("20211205005608_ExamenAuditory")]
    partial class ExamenAuditory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Gex.Models.Comision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("CicloLectivo")
                        .HasColumnType("int")
                        .HasColumnName("ciclo_lectivo");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<int>("Estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_comisiones");

                    b.HasIndex("Nombre", "CicloLectivo")
                        .IsUnique()
                        .HasDatabaseName("ix_comisiones_nombre_ciclo_lectivo");

                    b.ToTable("comisiones", (string)null);
                });

            modelBuilder.Entity("Gex.Models.Cursada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("ComisionId")
                        .HasColumnType("int")
                        .HasColumnName("comision_id");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha");

                    b.Property<long?>("MateriaId")
                        .HasColumnType("bigint")
                        .HasColumnName("materia_id");

                    b.HasKey("Id")
                        .HasName("pk_cursadas");

                    b.HasIndex("ComisionId")
                        .HasDatabaseName("ix_cursadas_comision_id");

                    b.HasIndex("MateriaId")
                        .HasDatabaseName("ix_cursadas_materia_id");

                    b.ToTable("cursadas", (string)null);
                });

            modelBuilder.Entity("Gex.Models.Examen", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<int>("Estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<long?>("MateriaId")
                        .HasColumnType("bigint")
                        .HasColumnName("materia_id");

                    b.Property<int>("NotaPromo")
                        .HasColumnType("int")
                        .HasColumnName("nota_promo");

                    b.Property<int>("NotaRegular")
                        .HasColumnType("int")
                        .HasColumnName("nota_regular");

                    b.Property<bool>("Recuperatorio")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("recuperatorio");

                    b.Property<int>("Tipo")
                        .HasColumnType("int")
                        .HasColumnName("tipo");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_examenes");

                    b.HasIndex("MateriaId")
                        .HasDatabaseName("ix_examenes_materia_id");

                    b.ToTable("examenes", (string)null);
                });

            modelBuilder.Entity("Gex.Models.InscripcionComision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("AlumnoId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("alumno_id");

                    b.Property<int?>("ComisionId")
                        .HasColumnType("int")
                        .HasColumnName("comision_id");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha");

                    b.HasKey("Id")
                        .HasName("pk_inscripciones_comisiones");

                    b.HasIndex("AlumnoId")
                        .HasDatabaseName("ix_inscripciones_comisiones_alumno_id");

                    b.HasIndex("ComisionId")
                        .HasDatabaseName("ix_inscripciones_comisiones_comision_id");

                    b.ToTable("inscripciones_comisiones", (string)null);
                });

            modelBuilder.Entity("Gex.Models.InscripcionMesas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("AlumnoId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("alumno_id");

                    b.Property<int>("Condicion")
                        .HasColumnType("int")
                        .HasColumnName("condicion");

                    b.Property<long?>("MesaExamenId")
                        .HasColumnType("bigint")
                        .HasColumnName("mesa_examen_id");

                    b.Property<int>("Nota")
                        .HasColumnType("int")
                        .HasColumnName("nota");

                    b.HasKey("Id")
                        .HasName("pk_inscripciones_mesas");

                    b.HasIndex("AlumnoId")
                        .HasDatabaseName("ix_inscripciones_mesas_alumno_id");

                    b.HasIndex("MesaExamenId")
                        .HasDatabaseName("ix_inscripciones_mesas_mesa_examen_id");

                    b.ToTable("inscripciones_mesas", (string)null);
                });

            modelBuilder.Entity("Gex.Models.Materia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext")
                        .HasColumnName("nombre");

                    b.Property<int>("Tipo")
                        .HasColumnType("int")
                        .HasColumnName("tipo");

                    b.HasKey("Id")
                        .HasName("pk_materias");

                    b.ToTable("materias", (string)null);
                });

            modelBuilder.Entity("Gex.Models.MesaExamen", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<int>("Duracion")
                        .HasColumnType("int")
                        .HasColumnName("duracion");

                    b.Property<long?>("ExamenId")
                        .HasColumnType("bigint")
                        .HasColumnName("examen_id");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha");

                    b.Property<bool>("MostrarRespuestas")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("mostrar_respuestas");

                    b.Property<string>("ProfesorId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("profesor_id");

                    b.HasKey("Id")
                        .HasName("pk_mesas_examen");

                    b.HasIndex("ExamenId")
                        .HasDatabaseName("ix_mesas_examen_examen_id");

                    b.HasIndex("ProfesorId")
                        .HasDatabaseName("ix_mesas_examen_profesor_id");

                    b.ToTable("mesas_examen", (string)null);
                });

            modelBuilder.Entity("Gex.Models.MesaExamenPregunta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("MesaExamenId")
                        .HasColumnType("bigint")
                        .HasColumnName("mesa_examen_id");

                    b.Property<long?>("PreguntaId")
                        .HasColumnType("bigint")
                        .HasColumnName("pregunta_id");

                    b.Property<int>("Puntos")
                        .HasColumnType("int")
                        .HasColumnName("puntos");

                    b.HasKey("Id")
                        .HasName("pk_preguntas_examen");

                    b.HasIndex("MesaExamenId")
                        .HasDatabaseName("ix_preguntas_examen_mesa_examen_id");

                    b.HasIndex("PreguntaId")
                        .HasDatabaseName("ix_preguntas_examen_pregunta_id");

                    b.ToTable("preguntas_examen", (string)null);
                });

            modelBuilder.Entity("Gex.Models.Pregunta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext")
                        .HasColumnName("descripcion");

                    b.Property<int?>("TemaId")
                        .HasColumnType("int")
                        .HasColumnName("tema_id");

                    b.Property<int>("Tipo")
                        .HasColumnType("int")
                        .HasColumnName("tipo");

                    b.HasKey("Id")
                        .HasName("pk_preguntas");

                    b.HasIndex("TemaId")
                        .HasDatabaseName("ix_preguntas_tema_id");

                    b.ToTable("preguntas", (string)null);
                });

            modelBuilder.Entity("Gex.Models.Respuesta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<bool?>("Correcto")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("correcto");

                    b.Property<long?>("PreguntaId")
                        .HasColumnType("bigint")
                        .HasColumnName("pregunta_id");

                    b.Property<string>("Valor")
                        .HasColumnType("longtext")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("pk_respuestas");

                    b.HasIndex("PreguntaId")
                        .HasDatabaseName("ix_respuestas_pregunta_id");

                    b.ToTable("respuestas", (string)null);
                });

            modelBuilder.Entity("Gex.Models.RespuestaAlumno", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("AlumnoId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("alumno_id");

                    b.Property<bool?>("Correcto")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("correcto");

                    b.Property<long?>("ExamenId")
                        .HasColumnType("bigint")
                        .HasColumnName("examen_id");

                    b.Property<long?>("RespuestaId")
                        .HasColumnType("bigint")
                        .HasColumnName("respuesta_id");

                    b.Property<string>("Valor")
                        .HasColumnType("longtext")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("pk_respuestas_alumnos");

                    b.HasIndex("AlumnoId")
                        .HasDatabaseName("ix_respuestas_alumnos_alumno_id");

                    b.HasIndex("ExamenId")
                        .HasDatabaseName("ix_respuestas_alumnos_examen_id");

                    b.HasIndex("RespuestaId")
                        .HasDatabaseName("ix_respuestas_alumnos_respuesta_id");

                    b.ToTable("respuestas_alumnos", (string)null);
                });

            modelBuilder.Entity("Gex.Models.Tema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext")
                        .HasColumnName("descripcion");

                    b.Property<long?>("MateriaId")
                        .HasColumnType("bigint")
                        .HasColumnName("materia_id");

                    b.HasKey("Id")
                        .HasName("pk_temas");

                    b.HasIndex("MateriaId")
                        .HasDatabaseName("ix_temas_materia_id");

                    b.ToTable("temas", (string)null);
                });

            modelBuilder.Entity("Gex.Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext")
                        .HasColumnName("concurrency_stamp");

                    b.Property<long?>("Dni")
                        .HasColumnType("bigint")
                        .HasColumnName("dni");

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("email_confirmed");

                    b.Property<DateTimeOffset?>("EmailVerifiedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("email_verified_at");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("Password")
                        .HasColumnType("longtext")
                        .HasColumnName("password");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("ProfilePhotoPath")
                        .HasColumnType("longtext")
                        .HasColumnName("profile_photo_path");

                    b.Property<string>("RememberToken")
                        .HasColumnType("longtext")
                        .HasColumnName("remember_token");

                    b.Property<string>("Salt")
                        .HasColumnType("longtext")
                        .HasColumnName("salt");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext")
                        .HasColumnName("security_stamp");

                    b.Property<int>("Tipo")
                        .HasColumnType("int")
                        .HasColumnName("tipo");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_usuarios");

                    b.ToTable("usuarios", (string)null);
                });

            modelBuilder.Entity("Gex.Models.Cursada", b =>
                {
                    b.HasOne("Gex.Models.Comision", "Comision")
                        .WithMany()
                        .HasForeignKey("ComisionId")
                        .HasConstraintName("fk_cursadas_comisiones_comision_id");

                    b.HasOne("Gex.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId")
                        .HasConstraintName("fk_cursadas_materias_materia_id");

                    b.Navigation("Comision");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("Gex.Models.Examen", b =>
                {
                    b.HasOne("Gex.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId")
                        .HasConstraintName("fk_examenes_materias_materia_id");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("Gex.Models.InscripcionComision", b =>
                {
                    b.HasOne("Gex.Models.Usuario", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId")
                        .HasConstraintName("fk_inscripciones_comisiones_usuarios_alumno_id");

                    b.HasOne("Gex.Models.Comision", "Comision")
                        .WithMany()
                        .HasForeignKey("ComisionId")
                        .HasConstraintName("fk_inscripciones_comisiones_comisiones_comision_id");

                    b.Navigation("Alumno");

                    b.Navigation("Comision");
                });

            modelBuilder.Entity("Gex.Models.InscripcionMesas", b =>
                {
                    b.HasOne("Gex.Models.Usuario", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId")
                        .HasConstraintName("fk_inscripciones_mesas_usuarios_alumno_id");

                    b.HasOne("Gex.Models.MesaExamen", "MesaExamen")
                        .WithMany()
                        .HasForeignKey("MesaExamenId")
                        .HasConstraintName("fk_inscripciones_mesas_mesas_examen_mesa_examen_id");

                    b.Navigation("Alumno");

                    b.Navigation("MesaExamen");
                });

            modelBuilder.Entity("Gex.Models.MesaExamen", b =>
                {
                    b.HasOne("Gex.Models.Examen", "Examen")
                        .WithMany()
                        .HasForeignKey("ExamenId")
                        .HasConstraintName("fk_mesas_examen_examenes_examen_id");

                    b.HasOne("Gex.Models.Usuario", "Profesor")
                        .WithMany()
                        .HasForeignKey("ProfesorId")
                        .HasConstraintName("fk_mesas_examen_usuarios_profesor_id");

                    b.Navigation("Examen");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("Gex.Models.MesaExamenPregunta", b =>
                {
                    b.HasOne("Gex.Models.MesaExamen", "MesaExamen")
                        .WithMany()
                        .HasForeignKey("MesaExamenId")
                        .HasConstraintName("fk_preguntas_examen_mesas_examen_mesa_examen_id");

                    b.HasOne("Gex.Models.Pregunta", "Pregunta")
                        .WithMany()
                        .HasForeignKey("PreguntaId")
                        .HasConstraintName("fk_preguntas_examen_preguntas_pregunta_id");

                    b.Navigation("MesaExamen");

                    b.Navigation("Pregunta");
                });

            modelBuilder.Entity("Gex.Models.Pregunta", b =>
                {
                    b.HasOne("Gex.Models.Tema", "Tema")
                        .WithMany()
                        .HasForeignKey("TemaId")
                        .HasConstraintName("fk_preguntas_temas_tema_id");

                    b.Navigation("Tema");
                });

            modelBuilder.Entity("Gex.Models.Respuesta", b =>
                {
                    b.HasOne("Gex.Models.Pregunta", "Pregunta")
                        .WithMany()
                        .HasForeignKey("PreguntaId")
                        .HasConstraintName("fk_respuestas_preguntas_pregunta_id");

                    b.Navigation("Pregunta");
                });

            modelBuilder.Entity("Gex.Models.RespuestaAlumno", b =>
                {
                    b.HasOne("Gex.Models.Usuario", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId")
                        .HasConstraintName("fk_respuestas_alumnos_usuarios_alumno_id");

                    b.HasOne("Gex.Models.Examen", "Examen")
                        .WithMany()
                        .HasForeignKey("ExamenId")
                        .HasConstraintName("fk_respuestas_alumnos_examenes_examen_id");

                    b.HasOne("Gex.Models.Respuesta", "Respuesta")
                        .WithMany()
                        .HasForeignKey("RespuestaId")
                        .HasConstraintName("fk_respuestas_alumnos_respuestas_respuesta_id");

                    b.Navigation("Alumno");

                    b.Navigation("Examen");

                    b.Navigation("Respuesta");
                });

            modelBuilder.Entity("Gex.Models.Tema", b =>
                {
                    b.HasOne("Gex.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId")
                        .HasConstraintName("fk_temas_materias_materia_id");

                    b.Navigation("Materia");
                });
#pragma warning restore 612, 618
        }
    }
}
