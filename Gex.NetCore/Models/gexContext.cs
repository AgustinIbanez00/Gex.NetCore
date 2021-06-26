using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class GexContext : DbContext
    {
        public GexContext()
        {
        }

        public GexContext(DbContextOptions<GexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Curso> Cursos { get; set; }
        public virtual DbSet<Examen> Examenes { get; set; }
        public virtual DbSet<FailedJobs> FailedJobs { get; set; }
        public virtual DbSet<Materia> Materias { get; set; }
        public virtual DbSet<MateriasCursos> MateriasCursos { get; set; }
        public virtual DbSet<MesaExamen> Mesas { get; set; }
        public virtual DbSet<MesasAlumnos> MesasAlumnos { get; set; }
        public virtual DbSet<PersonalAccessTokens> PersonalAccessTokens { get; set; }
        public virtual DbSet<Pregunta> Preguntas { get; set; }
        public virtual DbSet<Respuesta> Respuestas { get; set; }
        public virtual DbSet<RespuestasAlumnos> RespuestasAlumnos { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;user=root;password=;Database=gex;Port=3306;");
            }
        }

    }
}
