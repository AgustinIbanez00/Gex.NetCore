using System.Collections.Generic;
using Gex.Models.Enums;

namespace Gex.ViewModels.Response;
public class ExamenResponse
{ 
    public long Id { get; set; }
    public MateriaResponse Materia { get; set; }
    public ExamenTipo Tipo { get; set; }    
    public int NotaRegular { get; set; }
    public int NotaPromo { get; set; }
    public bool Recuperatorio { get; set; }
}
