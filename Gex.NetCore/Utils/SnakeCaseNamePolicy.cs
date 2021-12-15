
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Gex.Extensions;

namespace Gex.Utils;
public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return new string(name.ToSnakeCase().ToArray());
    }
}
