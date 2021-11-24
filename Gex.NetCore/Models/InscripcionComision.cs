using MySql.Data.Types;

namespace Gex.NetCore.Models
{
    public class InscripcionComision
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }
        public Usuario Alumno { get; set; }
        public Comision Comision { get; set; }

    }
}
