using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class MateriasCursos
    {
        public long Id { get; set; }
        public long? CursoId { get; set; }
        public long? MateriaId { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Materia Materia { get; set; }
    }
}
