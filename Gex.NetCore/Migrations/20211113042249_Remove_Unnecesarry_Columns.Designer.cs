﻿// <auto-generated />
using System;
using Gex.NetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gex.NetCore.Migrations
{
    [DbContext(typeof(GexContext))]
    [Migration("20211113042249_Remove_Unnecesarry_Columns")]
    partial class Remove_Unnecesarry_Columns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Gex.NetCore.Models.Comision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CicloLectivo")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Comisiones");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Cursada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ComisionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("MateriaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ComisionId");

                    b.HasIndex("MateriaId");

                    b.ToTable("Cursadas");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Examen", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Duracion")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("MateriaId")
                        .HasColumnType("bigint");

                    b.Property<int?>("ModalidadId")
                        .HasColumnType("int");

                    b.Property<int>("NotaPromo")
                        .HasColumnType("int");

                    b.Property<int>("NotaRegular")
                        .HasColumnType("int");

                    b.Property<int?>("TipoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MateriaId");

                    b.HasIndex("ModalidadId");

                    b.HasIndex("TipoId");

                    b.ToTable("Examenes");
                });

            modelBuilder.Entity("Gex.NetCore.Models.InscripcionComision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AlumnoId")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("ComisionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("ComisionId");

                    b.ToTable("InscripcionesComisiones");
                });

            modelBuilder.Entity("Gex.NetCore.Models.InscripcionMesas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AlumnoId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Condicion")
                        .HasColumnType("int");

                    b.Property<long?>("MesaId")
                        .HasColumnType("bigint");

                    b.Property<int>("Nota")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("MesaId");

                    b.ToTable("InscripcionesMesas");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Materia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("Gex.NetCore.Models.MesaExamen", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("ExamenId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("MostrarRespuestas")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProfesorId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ExamenId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("MesasExamen");
                });

            modelBuilder.Entity("Gex.NetCore.Models.ModalidadExamen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ModalidadExamen");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Pregunta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<int>("Puntos")
                        .HasColumnType("int");

                    b.Property<int?>("TemaId")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TemaId");

                    b.ToTable("Preguntas");
                });

            modelBuilder.Entity("Gex.NetCore.Models.PreguntaExamen", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("ExamenId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PreguntaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ExamenId");

                    b.HasIndex("PreguntaId");

                    b.ToTable("PreguntasExamen");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Respuesta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool?>("Correcto")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("PreguntaId")
                        .HasColumnType("bigint");

                    b.Property<string>("Valor")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PreguntaId");

                    b.ToTable("Respuestas");
                });

            modelBuilder.Entity("Gex.NetCore.Models.RespuestaAlumno", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AlumnoId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("Correcto")
                        .HasColumnType("tinyint(1)");

                    b.Property<long?>("ExamenId")
                        .HasColumnType("bigint");

                    b.Property<long?>("RespuestaId")
                        .HasColumnType("bigint");

                    b.Property<string>("Valor")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("ExamenId");

                    b.HasIndex("RespuestaId");

                    b.ToTable("RespuestasAlumnos");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Tema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<long?>("MateriaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MateriaId");

                    b.ToTable("Temas");
                });

            modelBuilder.Entity("Gex.NetCore.Models.TipoExamen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TiposExamen");
                });

            modelBuilder.Entity("Gex.NetCore.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<long?>("Dni")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("EmailVerifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProfilePhotoPath")
                        .HasColumnType("longtext");

                    b.Property<string>("RememberToken")
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Cursada", b =>
                {
                    b.HasOne("Gex.NetCore.Models.Comision", "Comision")
                        .WithMany()
                        .HasForeignKey("ComisionId");

                    b.HasOne("Gex.NetCore.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId");

                    b.Navigation("Comision");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Examen", b =>
                {
                    b.HasOne("Gex.NetCore.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId");

                    b.HasOne("Gex.NetCore.Models.ModalidadExamen", "Modalidad")
                        .WithMany()
                        .HasForeignKey("ModalidadId");

                    b.HasOne("Gex.NetCore.Models.TipoExamen", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId");

                    b.Navigation("Materia");

                    b.Navigation("Modalidad");

                    b.Navigation("Tipo");
                });

            modelBuilder.Entity("Gex.NetCore.Models.InscripcionComision", b =>
                {
                    b.HasOne("Gex.NetCore.Models.User", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId");

                    b.HasOne("Gex.NetCore.Models.Comision", "Comision")
                        .WithMany()
                        .HasForeignKey("ComisionId");

                    b.Navigation("Alumno");

                    b.Navigation("Comision");
                });

            modelBuilder.Entity("Gex.NetCore.Models.InscripcionMesas", b =>
                {
                    b.HasOne("Gex.NetCore.Models.User", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId");

                    b.HasOne("Gex.NetCore.Models.MesaExamen", "Mesa")
                        .WithMany()
                        .HasForeignKey("MesaId");

                    b.Navigation("Alumno");

                    b.Navigation("Mesa");
                });

            modelBuilder.Entity("Gex.NetCore.Models.MesaExamen", b =>
                {
                    b.HasOne("Gex.NetCore.Models.Examen", "Examen")
                        .WithMany()
                        .HasForeignKey("ExamenId");

                    b.HasOne("Gex.NetCore.Models.User", "Profesor")
                        .WithMany()
                        .HasForeignKey("ProfesorId");

                    b.Navigation("Examen");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Pregunta", b =>
                {
                    b.HasOne("Gex.NetCore.Models.Tema", "Tema")
                        .WithMany()
                        .HasForeignKey("TemaId");

                    b.Navigation("Tema");
                });

            modelBuilder.Entity("Gex.NetCore.Models.PreguntaExamen", b =>
                {
                    b.HasOne("Gex.NetCore.Models.Examen", "Examen")
                        .WithMany()
                        .HasForeignKey("ExamenId");

                    b.HasOne("Gex.NetCore.Models.Pregunta", "Pregunta")
                        .WithMany()
                        .HasForeignKey("PreguntaId");

                    b.Navigation("Examen");

                    b.Navigation("Pregunta");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Respuesta", b =>
                {
                    b.HasOne("Gex.NetCore.Models.Pregunta", "Pregunta")
                        .WithMany()
                        .HasForeignKey("PreguntaId");

                    b.Navigation("Pregunta");
                });

            modelBuilder.Entity("Gex.NetCore.Models.RespuestaAlumno", b =>
                {
                    b.HasOne("Gex.NetCore.Models.User", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId");

                    b.HasOne("Gex.NetCore.Models.Examen", "Examen")
                        .WithMany()
                        .HasForeignKey("ExamenId");

                    b.HasOne("Gex.NetCore.Models.Respuesta", "Respuesta")
                        .WithMany()
                        .HasForeignKey("RespuestaId");

                    b.Navigation("Alumno");

                    b.Navigation("Examen");

                    b.Navigation("Respuesta");
                });

            modelBuilder.Entity("Gex.NetCore.Models.Tema", b =>
                {
                    b.HasOne("Gex.NetCore.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId");

                    b.Navigation("Materia");
                });
#pragma warning restore 612, 618
        }
    }
}
