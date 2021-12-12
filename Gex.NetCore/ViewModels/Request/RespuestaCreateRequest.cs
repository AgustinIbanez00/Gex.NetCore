﻿using System.ComponentModel.DataAnnotations;
using Gex.Models.Enums;
using Gex.Validation.Attributes;

namespace Gex.ViewModels.Request;
public class RespuestaCreateRequest
{
    public long Id { get; set; }
    /// <summary>
    /// Valor de la respuesta.
    /// </summary>
    [Required, Display(Name = "valor")]
    public string Valor { get; set; }

    /// <summary>
    /// Indica si la respuesta es verdadera.
    /// </summary>
    [Required, Display(Name = "correcto")]
    public bool? Correcto { get; set; }
    /// <summary>
    /// Pregunta a la que estará relacionada la respuesta.
    /// </summary>
    [Required, Display(Name = "pregunta")]
    public long PreguntaId { get; set; }

    [Required, Display(Name = "estado")]
    public int Borrar { get; set; }
}


/*
    public PreguntaTipo Tipo { get; set; }
    public string[] Respuestas { get; set; }
    public int[] RespuestasCorrectas { get; set; }

    [GreaterThanZero]
    public int RespuestaCorrecta { get; set; }

    
    public long PreguntaId { get; set; }
    */