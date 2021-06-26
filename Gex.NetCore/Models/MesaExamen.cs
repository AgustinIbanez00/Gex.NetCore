using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class MesaExamen
    {
        public MesaExamen()
        {
            MesasAlumnos = new HashSet<MesasAlumnos>();
        }

        public long Id { get; set; }
        public long? ProfesorId { get; set; }
        public long? ExamenId { get; set; }
        public DateTime? Fecha { get; set; }
        public byte? MostrarRespuestas { get; set; }

        public virtual Examen Examen { get; set; }
        public virtual User Profesor { get; set; }
        public virtual ICollection<MesasAlumnos> MesasAlumnos { get; set; }
    }
}
