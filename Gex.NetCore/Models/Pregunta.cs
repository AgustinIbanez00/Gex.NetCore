using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class Pregunta
    {
        public Pregunta()
        {
            Respuestas = new HashSet<Respuesta>();
        }

        public long Id { get; set; }
        public long? ExamenId { get; set; }
        public string Valor { get; set; }
        public int? Tipo { get; set; }
        public int Puntos { get; set; }

        public virtual Examen Examen { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; }
    }
}
