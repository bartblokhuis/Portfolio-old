using Portfolio.Domain.Wrapper;
using System.Linq;

namespace System.Collections.Generic;

public static class IEnumerableExtensions
{
    public static ListResult<T> ToListResult<T>(this IEnumerable<T> source)
    {
        return ListResult<T>.Success(source.ToList());
    }
}
