using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq;

public static class AsyncEnumerablesExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items)
    {
        var evaluatedItems = new List<T>();
        await foreach (var item in items)
            evaluatedItems.Add(item);
        return evaluatedItems;
    }
}
