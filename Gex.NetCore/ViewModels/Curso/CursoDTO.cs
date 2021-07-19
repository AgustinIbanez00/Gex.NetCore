using System;
using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.ViewModels
{
    public class CursoDTO
    {
        [Required]
        [Display(Name = "comisión")]
        public string Comision { get; set; }

        [Required]
        [Range(minimum: 1, maximum: 3)]        
        [Display(Name = "cuatrimestre")]
        public int Cuatrimestre { get; set; }

        [Required]
        [Display(Name = "ciclo lectivo")]
        //[Range(typeof(DateTime), DateTime.Now.AddYears(-20).Year.ToString(), DateTime.Now.AddYears(1).Year.ToString())]
        public int? CicloLectivo { get; set; }

        [Display(Name = "cantidad de alumnos")]
        [Range(minimum: 0, maximum: 2000)]
        public int CantAlumnos { get; set; } = 0;
        
        [Range(minimum: 0, maximum: 1)]
        public int Estado { get; set; } = 0;
    }
}
