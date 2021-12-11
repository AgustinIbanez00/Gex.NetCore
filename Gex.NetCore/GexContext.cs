using Gex.Fakers;
using Gex.Models;
using Microsoft.EntityFrameworkCore;

namespace Gex;

public partial class GexContext : DbContext
{
    public GexContext(DbContextOptions<GexContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Comision> Comisiones { get; set; }
    public virtual DbSet<Cursada> Cursadas { get; set; }
    public virtual DbSet<Examen> Examenes { get; set; }
    public virtual DbSet<InscripcionMesas> InscripcionesMesas { get; set; }
    public virtual DbSet<InscripcionComision> InscripcionesComisiones { get; set; }
    public virtual DbSet<Materia> Materias { get; set; }
    public virtual DbSet<MesaExamen> MesasExamen { get; set; }
    public virtual DbSet<Pregunta> Preguntas { get; set; }
    public virtual DbSet<Respuesta> Respuestas { get; set; }
    public virtual DbSet<RespuestaAlumno> RespuestasAlumnos { get; set; }
    public virtual DbSet<MesaExamenPregunta> PreguntasExamen { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //************ COMISION **********/

        modelBuilder.Entity<Comision>()
        .HasIndex(p => new { p.Nombre, p.CicloLectivo }).IsUnique();

        //modelBuilder.Entity<Materia>()
        //    .HasData(MateriaFaker.Get(300));
            
        base.OnModelCreating(modelBuilder);
    }

}
