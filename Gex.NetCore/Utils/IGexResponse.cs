using System.Collections.Generic;
using System.Runtime.InteropServices;
using Gex.Helpers;

namespace Gex.Utils;
public interface IGexResponse<T> where T : class
{
    public GexResponse<T> Error(GexErrorMessage error, [Optional] string message);
    public GexResponse<ICollection<T>> CollectionMessage(GexErrorMessage error, [Optional] string message);
    public GexResponse<T> Success(GexSuccessMessage message);
    public GexResponse<ICollection<T>> OkCollection(ICollection<T> data);
    public GexResponse<T> Data(T data, GexSuccessMessage gexSuccess);
    public GexResponse<T> Data(T data);
}
