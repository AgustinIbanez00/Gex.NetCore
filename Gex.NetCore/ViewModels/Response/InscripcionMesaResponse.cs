using System.ComponentModel.DataAnnotations;
using Gex.Models.Enums;

namespace Gex.ViewModels.Response
{
    public class InscripcionMesaResponse
    {
        public long Id { get; set; }
        public int Nota { get; set; }
        public InscripcionCondicion Condicion { get; set; }
        public UsuarioResponse Alumno { get; set; }
        public MesaExamenResponse Examen { get; set; }
    }
}
