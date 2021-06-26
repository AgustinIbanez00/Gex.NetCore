using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class RespuestasAlumnos
    {
        public long Id { get; set; }
        public long? AlumnoId { get; set; }
        public long? RespuestaId { get; set; }
        public string Valor { get; set; }
        public byte Estado { get; set; }

        public virtual User Alumno { get; set; }
        public virtual Respuesta Respuesta { get; set; }
    }
}
