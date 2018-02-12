using HyperMapper.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMapper.Mappers
{
    public class ShallowCopyArrayMapper<T> : IObjectMapper<T[], T[]>
    {
        public T[] Map(T[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new T[from.Length];
            Array.Copy(from, newArray, from.Length);
            return newArray;
        }
    }

    public class BlockCopyMapper<T> : IObjectMapper<T[], T[]>
    {
        readonly int elementMemorySize;

        public BlockCopyMapper(int elementMemorySize)
        {
            this.elementMemorySize = elementMemorySize;
        }

        public T[] Map(T[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new T[from.Length];
            Buffer.BlockCopy(from, 0, newArray, 0, elementMemorySize * from.Length);
            return newArray;
        }
    }

    // TODO:ArraySegment


    public class ArrayMapper<TFrom, TTo> : IObjectMapper<TFrom[], TTo[]>
    {
        public TTo[] Map(TFrom[] obj, IObjectMapperResolver resolver)
        {
            if (obj == null) return null;

            var mapper = resolver.GetMapper<TFrom, TTo>();

            var newArray = new TTo[obj.Length];
            for (int i = 0; i < obj.Length; i++)
            {
                newArray[i] = mapper.Map(obj[i], resolver);
            }

            return newArray;
        }
    }

    public class EnumerableArrayMapper<TFrom, TTo> : IObjectMapper<IEnumerable<TFrom>, TTo[]>
    {
        public TTo[] Map(IEnumerable<TFrom> obj, IObjectMapperResolver resolver)
        {
            if (obj == null) return null;

            var mapper = resolver.GetMapper<TFrom, TTo>();
            var count = obj.FastCount();
            if (count != null)
            {
                var newArray = new TTo[count.Value];
                var i = 0;
                foreach (var item in obj)
                {
                    newArray[i] = mapper.Map(item, resolver);
                    i++;
                }

                return newArray;
            }
            else
            {
                var newArray = new ArrayBuffer<TTo>(4);
                foreach (var item in obj)
                {

                    newArray.Add(mapper.Map(item, resolver));
                }

                return newArray.ToArray();
            }
        }
    }


    public class EnumerableMapper<TFrom, TTo> : IObjectMapper<IEnumerable<TFrom>, IEnumerable<TTo>>
    {
        public IEnumerable<TTo> Map(IEnumerable<TFrom> obj, IObjectMapperResolver resolver)
        {
            return resolver.GetMapper<IEnumerable<TFrom>, TTo[]>().Map(obj, resolver);
        }
    }


    public abstract class CollectionMapperBase<TFromCollection, TFromElement, TFromEnumerator, TToElement, TToIntermediate, TToCollection> : IObjectMapper<TFromCollection, TToCollection>
        where TFromCollection : class, IEnumerable<TFromElement>
        where TFromEnumerator : IEnumerator<TFromElement>
        where TToCollection : class, IEnumerable<TToElement>
    {
        protected abstract TFromEnumerator GetSourceEnumerator(TFromCollection source);
        protected abstract TToIntermediate Create(int? count);
        protected abstract void Add(ref TToIntermediate collection, int index, TToElement value);
        protected abstract TToCollection Complete(ref TToIntermediate intermediateCollection);

        public TToCollection Map(TFromCollection from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var mapper = resolver.GetMapper<TFromElement, TToElement>();

            var count = from.FastCount();
            var builder = Create(count);

            using (var e = GetSourceEnumerator(from))
            {
                var index = 0;
                while (e.MoveNext())
                {
                    var value = mapper.Map(e.Current, resolver);
                    Add(ref builder, index, value);
                    index++;
                }
            }

            return Complete(ref builder);
        }
    }

    public class LinkedListMapper<TFrom, TTo> : CollectionMapperBase<LinkedList<TFrom>, TFrom, LinkedList<TFrom>.Enumerator, TTo, LinkedList<TTo>, LinkedList<TTo>>
    {
        protected override void Add(ref LinkedList<TTo> collection, int index, TTo value)
        {
            collection.AddLast(value);
        }

        protected override LinkedList<TTo> Complete(ref LinkedList<TTo> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override LinkedList<TTo> Create(int? count)
        {
            return new LinkedList<TTo>();
        }

        protected override LinkedList<TFrom>.Enumerator GetSourceEnumerator(LinkedList<TFrom> source)
        {
            return source.GetEnumerator();
        }
    }



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
    }


    public class DictionaryMapper<TKey, TValue> : IObjectMapper<Dictionary<TKey, TValue>, Dictionary<TKey, TValue>>
    {
        public Dictionary<TKey, TValue> Map(Dictionary<TKey, TValue> obj, IObjectMapperResolver resolver)
        {
            if (obj == null) return null;

            var dict = new Dictionary<TKey, TValue>(obj.Count, obj.Comparer);

            var keyMapper = resolver.GetMapper<TKey, TKey>();
            var valueMapper = resolver.GetMapper<TValue, TValue>();

            foreach (var item in obj)
            {
                var key = keyMapper.Map(item.Key, resolver);
                var value = valueMapper.Map(item.Value, resolver);
                dict.Add(key, value);
            }

            return dict;
        }
    }

    public class DictionaryMapper<TFromTKey, TFromTValue, TToKey, TToValue> : IObjectMapper<IDictionary<TFromTKey, TFromTValue>, Dictionary<TToKey, TToValue>>
    {
        public Dictionary<TToKey, TToValue> Map(IDictionary<TFromTKey, TFromTValue> obj, IObjectMapperResolver resolver)
        {
            if (obj == null) return null;

            var dict = new Dictionary<TToKey, TToValue>(obj.Count); // equality comparer?

            var keyMapper = resolver.GetMapper<TFromTKey, TToKey>();
            var valueMapper = resolver.GetMapper<TFromTValue, TToValue>();

            foreach (var item in obj)
            {
                var key = keyMapper.Map(item.Key, resolver);
                var value = valueMapper.Map(item.Value, resolver);
                dict.Add(key, value);
            }

            return dict;
        }
    }

    public class StackMapper<TFrom, TTo> : CollectionMapperBase<Stack<TFrom>, TFrom, Stack<TFrom>.Enumerator, TTo, ArrayBuffer<TTo>, Stack<TTo>>
    {
        protected override void Add(ref ArrayBuffer<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override Stack<TTo> Complete(ref ArrayBuffer<TTo> intermediateCollection)
        {
            var bufArray = intermediateCollection.Buffer;
            var stack = new Stack<TTo>(intermediateCollection.Size);
            for (int i = intermediateCollection.Size - 1; i >= 0; i--)
            {
                stack.Push(bufArray[i]);
            }
            return stack;
        }

        protected override ArrayBuffer<TTo> Create(int? count)
        {
            return new ArrayBuffer<TTo>(count ?? 4);
        }

        protected override Stack<TFrom>.Enumerator GetSourceEnumerator(Stack<TFrom> source)
        {
            return source.GetEnumerator();
        }
    }

}
