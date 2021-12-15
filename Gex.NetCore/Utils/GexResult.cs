using System.Collections.Generic;
using Gex.Extensions.Response;

namespace Gex.Utils;
public class GexResult<T> where T : class
{
    public T Data { get; set; }
    public bool Success { get; set; } = true;
    public string Message { get; set; } = null;
    public IDictionary<string, string[]> ErrorMessages { get; set; } = null;
    public GexErrorMessage ErrorCode { get; set; }
}
