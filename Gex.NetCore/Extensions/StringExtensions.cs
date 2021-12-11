using System.Collections.Generic;

namespace Gex.Extensions;
public static class StringExtensions
{
    public static IEnumerable<char> ToSnakeCase(this string text)
    {
        CharEnumerator e = text.GetEnumerator();

        if (!e.MoveNext()) yield break;
        yield return char.ToLower(e.Current);
        while (e.MoveNext())
        {
            if (char.IsUpper(e.Current))
            {
                yield return '_';
                yield return char.ToLower(e.Current);
            }
            else
            {
                yield return e.Current;
            }
        }
    }
}
