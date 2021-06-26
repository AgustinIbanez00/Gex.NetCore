using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class Curso
    {
        public Curso()
        {
            Examenes = new HashSet<Examen>();
            MateriasCursos = new HashSet<MateriasCursos>();
        }

        public long Id { get; set; }
        public string Comision { get; set; }
        public int Cuatrimestre { get; set; }
        public int? CicloLectivo { get; set; }
        public int CantAlumnos { get; set; }
        public int Estado { get; set; }

        public virtual ICollection<Examen> Examenes { get; set; }
        public virtual ICollection<MateriasCursos> MateriasCursos { get; set; }
    }
}
