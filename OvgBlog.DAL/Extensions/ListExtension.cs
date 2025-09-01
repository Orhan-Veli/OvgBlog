using System.Collections.Generic;
using System.Linq;

namespace OvgBlog.DAL.Extensions;

public static class ListExtension
{
    public static IEnumerable<T> IfNullOrEmpty<T>(this IEnumerable<T>? source)
    {
        if (source == null || !source.Any())
        {
            return [];
        }
        
        return source;
    }
}