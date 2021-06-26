using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class Respuesta
    {
        public Respuesta()
        {
            RespuestasAlumnos = new HashSet<RespuestasAlumnos>();
        }

        public long Id { get; set; }
        public long? PreguntaId { get; set; }
        public string Valor { get; set; }
        public byte? Correcto { get; set; }

        public virtual Pregunta Pregunta { get; set; }
        public virtual ICollection<RespuestasAlumnos> RespuestasAlumnos { get; set; }
    }
}
