﻿using System.ComponentModel.DataAnnotations;

namespace Gex.NetCore.Models;
public partial class Respuesta
{
    [Key]
    public long Id { get; set; }
    public virtual Pregunta Pregunta { get; set; }
    public string Valor { get; set; }
    public bool? Correcto { get; set; }
}
