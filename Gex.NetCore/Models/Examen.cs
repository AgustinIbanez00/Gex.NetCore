using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class Examen
    {
        public Examen()
        {
            Mesas = new HashSet<MesaExamen>();
            Preguntas = new HashSet<Pregunta>();
        }

        public long Id { get; set; }

        [ForeignKey(nameof(Curso))]
        public long? CursoId { get; set; }
        [ForeignKey(nameof(Materia))]
        public long? MateriaId { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? MinTardanza { get; set; }
        public int? NotaRegular { get; set; }
        public int? NotaPromo { get; set; }
        public byte Estado { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Materia Materia { get; set; }
        public virtual ICollection<MesaExamen> Mesas { get; set; }
        public virtual ICollection<Pregunta> Preguntas { get; set; }
    }
}
