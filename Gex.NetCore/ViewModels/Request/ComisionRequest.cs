﻿using System.ComponentModel.DataAnnotations;
using Gex.Validation.Attributes;

namespace Gex.ViewModels.Request;
public class ComisionRequest
{
    [Required, Display(Name = "identificador")]
    public int Id { get; set; }
    [Required, Display(Name = "comisión"), StringLength(50, MinimumLength = 5)]
    public string Nombre { get; set; }
    [Required, Display(Name = "ciclo lectivo"), Date]
    public int CicloLectivo { get; set; }
}