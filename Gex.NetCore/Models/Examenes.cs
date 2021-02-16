using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class Examenes
    {
        public Examenes()
        {
            Mesas = new HashSet<Mesas>();
            Preguntas = new HashSet<Preguntas>();
        }

        public long Id { get; set; }
        public long? CursoId { get; set; }
        public long? MateriaId { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? MinTardanza { get; set; }
        public int? NotaRegular { get; set; }
        public int? NotaPromo { get; set; }
        public byte Estado { get; set; }

        public virtual Cursos Curso { get; set; }
        public virtual Materias Materia { get; set; }
        public virtual ICollection<Mesas> Mesas { get; set; }
        public virtual ICollection<Preguntas> Preguntas { get; set; }
    }
}
