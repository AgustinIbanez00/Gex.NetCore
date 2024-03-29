﻿using System.ComponentModel;

namespace Gex.Models.Enums;
public enum ExamenTipo
{
    /// <summary>
    /// Indica que un exámen es final.
    /// </summary>
    [Description("Final")]
    FINAL = 0,
    /// <summary>
    /// Indica que un exámen es parcial.
    /// </summary>
    [Description("Parcial")]
    PARCIAL = 1,
    /// <summary>
    /// Indica que un exámen es global.
    /// </summary>
    [Description("Global")]
    GLOBAL = 2,
    /// <summary>
    /// Este tipo de exámen solo es úsado para un ambiente de prueba.
    /// </summary>
    TEST = 3
}
