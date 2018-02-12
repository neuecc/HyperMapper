using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMapper.Internal
{
    internal static class CollectionHelper
    {
        public static int? FastCount<T>(this IEnumerable<T> source)
        {
            var c = source as ICollection<T>;
            if (c != null) return c.Count;
            var c2 = source as IReadOnlyCollection<T>;
            if (c2 != null) return c2.Count;
            return null;
        }

        public static Type GetEnumerableElement(Type type)
        {
            if (type.IsArray)
            {
                var rank = type.GetArrayRank();
                if (rank == 1)
                {
                    return type.GetElementType();
                }
                else
                {
                    return null; // not supported
                }
            }

            // concat self(if type is IEnumerable<>...)
            foreach (var item in new[] { type }.Concat(type.GetInterfaces()))
            {
                if (item.IsGenericType)
                {
                    if (item.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        return item.GetGenericArguments()[0];
                    }
                }
            }

            return null;
        }
    }
}
