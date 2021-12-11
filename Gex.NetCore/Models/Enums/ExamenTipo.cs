using System.ComponentModel;

namespace Gex.Models.Enums;
public enum ExamenTipo
{
    [Description("Final")]
    FINAL,
    [Description("Parcial")]
    PARCIAL,
    [Description("Global")]
    GLOBAL,
    TEST
}
