﻿using System.ComponentModel.DataAnnotations;
using Gex.Helpers;

namespace Gex.Models;
public enum MateriaTipo
{
    ANUAL,
    CUATRIMESTRAL,
    TRIMESTRAL
}
public partial class Materia : Auditory
{
    [Key]
    public long Id { get; set; }
    [MaxLength(255)]
    [Required]
    public string Nombre { get; set; }
    public MateriaTipo Tipo { get; set; }

    public static GexResponseOptions Options => new GexResponseOptions()
    {
        Entity = "materia",
        Gender = Gender.FEMALE
    };

}
