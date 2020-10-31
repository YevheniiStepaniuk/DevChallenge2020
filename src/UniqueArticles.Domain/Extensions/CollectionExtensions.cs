using System.Collections.Generic;
using System.Linq;

namespace UniqueArticles.Domain.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<TItem> EmptyIfNull<TItem>(this IEnumerable<TItem> entries)
        {
            return entries ?? Enumerable.Empty<TItem>();
        }
    }
}