using System;

using Gex.NetCore.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class gexContext : DbContext
    {
        public gexContext()
        {
        }

        public gexContext(DbContextOptions<gexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cursos> Cursos { get; set; }
        public virtual DbSet<Examenes> Examenes { get; set; }
        public virtual DbSet<FailedJobs> FailedJobs { get; set; }
        public virtual DbSet<Materias> Materias { get; set; }
        public virtual DbSet<MateriasCursos> MateriasCursos { get; set; }
        public virtual DbSet<Mesas> Mesas { get; set; }
        public virtual DbSet<MesasAlumnos> MesasAlumnos { get; set; }
        public virtual DbSet<PersonalAccessTokens> PersonalAccessTokens { get; set; }
        public virtual DbSet<Preguntas> Preguntas { get; set; }
        public virtual DbSet<Respuestas> Respuestas { get; set; }
        public virtual DbSet<RespuestasAlumnos> RespuestasAlumnos { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;user=root;password=;Database=gex;Port=3306;");
            }
        }

    }
}
