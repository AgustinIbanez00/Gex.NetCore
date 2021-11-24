using Gex.NetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Gex.NetCore;

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
    public virtual DbSet<TipoExamen> TiposExamen { get; set; }
    public virtual DbSet<Materia> Materias { get; set; }
    public virtual DbSet<MesaExamen> MesasExamen { get; set; }
    public virtual DbSet<Pregunta> Preguntas { get; set; }
    public virtual DbSet<Respuesta> Respuestas { get; set; }
    public virtual DbSet<RespuestaAlumno> RespuestasAlumnos { get; set; }
    public virtual DbSet<PreguntaExamen> PreguntasExamen { get; set; }
    public virtual DbSet<Tema> Temas { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
