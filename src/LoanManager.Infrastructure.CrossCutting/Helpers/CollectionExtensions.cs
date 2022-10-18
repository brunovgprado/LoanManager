using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanManager.Infrastructure.CrossCutting.Helpers
{
    public static class CollectionExtensions
    {
        public static IEnumerable<TObject> DistinctBy<TObject, TKey>(this IEnumerable<TObject> items, Func<TObject, TKey> property)
        {
            return items.GroupBy(property)
                .Select(item => item.First());
        }
    }
}
