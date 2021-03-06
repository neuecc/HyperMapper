﻿using HyperMapper.Internal;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;

namespace HyperMapper.Mappers
{
    public sealed class ShallowCopyArrayMapper<T> : IObjectMapper<T[], T[]>
    {
        public T[] Map(T[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new T[from.Length];
            Array.Copy(from, newArray, from.Length);
            return newArray;
        }
    }

    public sealed class BlockCopyMapper<T> : IObjectMapper<T[], T[]>
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

    public sealed class ArrayMapper<TFrom, TTo> : IObjectMapper<TFrom[], TTo[]>
    {
        public TTo[] Map(TFrom[] obj, IObjectMapperResolver resolver)
        {
            if (obj == null) return null;

            var mapper = resolver.GetMapperWithVerify<TFrom, TTo>();

            var newArray = new TTo[obj.Length];
            for (int i = 0; i < obj.Length; i++)
            {
                newArray[i] = mapper.Map(obj[i], resolver);
            }

            return newArray;
        }
    }

    public sealed class ArrayMapper<TFromCollection, TFromElement, TTo> : IObjectMapper<TFromCollection, TTo[]>
        where TFromCollection : IEnumerable<TFromElement>
    {
        public TTo[] Map(TFromCollection from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var mapper = resolver.GetMapperWithVerify<TFromElement, TTo>();
            var count = from.FastCount();
            if (count != null)
            {
                var newArray = new TTo[count.Value];
                var i = 0;
                foreach (var item in from)
                {
                    newArray[i] = mapper.Map(item, resolver);
                    i++;
                }

                return newArray;
            }
            else
            {
                var newArray = new ArrayBuffer<TTo>(4);
                foreach (var item in from)
                {

                    newArray.Add(mapper.Map(item, resolver));
                }

                return newArray.ToArray();
            }
        }
    }

    public sealed class ArraySegmentMapper<TFrom, TTo> : IObjectMapper<ArraySegment<TFrom>, ArraySegment<TTo>>
    {
        public ArraySegment<TTo> Map(ArraySegment<TFrom> from, IObjectMapperResolver resolver)
        {
            if (from.Array == null) return new ArraySegment<TTo>(null, 0, 0);

            var to = new TTo[from.Count];
            var array = from.Array;
            var max = from.Offset + from.Count;
            var index = 0;
            var mapper = resolver.GetMapperWithVerify<TFrom, TTo>();
            for (int i = from.Offset; i < max; i++)
            {
                to[index++] = mapper.Map(array[i], resolver);
            }

            return new ArraySegment<TTo>(to);
        }
    }

    public sealed class ArraySegmentMapper<TFromCollection, TFromElement, TToElement> : IObjectMapper<TFromCollection, ArraySegment<TToElement>>
        where TFromCollection : IEnumerable<TFromElement>
    {
        public ArraySegment<TToElement> Map(TFromCollection from, IObjectMapperResolver resolver)
        {
            if (from == null) return new ArraySegment<TToElement>(null, 0, 0);

            var mapper = resolver.GetMapperWithVerify<TFromElement, TToElement>();
            var count = from.FastCount() ?? 4;
            var to = new ArrayBuffer<TToElement>(count);

            foreach (var item in from)
            {
                to.Add(mapper.Map(item, resolver));
            }

            return new ArraySegment<TToElement>(to.ToArray());
        }
    }

    public sealed class EnumerableMapper<TFrom, TTo> : IObjectMapper<IEnumerable<TFrom>, IEnumerable<TTo>>
    {
        public IEnumerable<TTo> Map(IEnumerable<TFrom> obj, IObjectMapperResolver resolver)
        {
            return resolver.GetMapperWithVerify<IEnumerable<TFrom>, TTo[]>().Map(obj, resolver);
        }
    }


    public sealed class EnumerableMapper<TFromCollection, TFrom, TTo> : IObjectMapper<TFromCollection, IEnumerable<TTo>>
        where TFromCollection : IEnumerable<TFrom>
    {
        public IEnumerable<TTo> Map(TFromCollection obj, IObjectMapperResolver resolver)
        {
            return resolver.GetMapperWithVerify<IEnumerable<TFrom>, TTo[]>().Map(obj, resolver);
        }
    }

    public sealed class ListMapper<TFrom, TTo> : IObjectMapper<List<TFrom>, List<TTo>>
    {
        public List<TTo> Map(List<TFrom> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var mapper = resolver.GetMapperWithVerify<TFrom, TTo>();
            var list = new List<TTo>(from.Count);
            foreach (var item in from)
            {
                list.Add(mapper.Map(item, resolver));
            }

            return list;
        }
    }

    public sealed class ListMapper<TFromCollection, TFrom, TTo> : IObjectMapper<TFromCollection, List<TTo>>
        where TFromCollection : IEnumerable<TFrom>
    {
        public List<TTo> Map(TFromCollection from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var mapper = resolver.GetMapperWithVerify<TFrom, TTo>();
            var list = new List<TTo>(from.FastCount() ?? 4);
            foreach (var item in from)
            {
                list.Add(mapper.Map(item, resolver));
            }

            return list;
        }
    }

    public sealed class ShallowCopyListMapper<TFromCollection, T> : IObjectMapper<TFromCollection, List<T>>
        where TFromCollection : IEnumerable<T>
    {
        public List<T> Map(TFromCollection from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new List<T>(from);
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

            var mapper = resolver.GetMapperWithVerify<TFromElement, TToElement>();

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

    public sealed class GenericCollectionMapper<TFromCollection, TFromElement, TToElement, TToCollection>
        : CollectionMapperBase<TFromCollection, TFromElement, IEnumerator<TFromElement>, TToElement, TToCollection, TToCollection>
         where TFromCollection : class, IEnumerable<TFromElement>
         where TToCollection : class, ICollection<TToElement>, new()
    {
        protected override void Add(ref TToCollection collection, int index, TToElement value)
        {
            collection.Add(value);
        }

        protected override TToCollection Complete(ref TToCollection intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override TToCollection Create(int? count)
        {
            return new TToCollection();
        }

        protected override IEnumerator<TFromElement> GetSourceEnumerator(TFromCollection source)
        {
            return source.GetEnumerator();
        }
    }

    public abstract class CollectionMapperBase<TFromCollection, TFromElement, TToElement, TToIntermediate, TToCollection>
        : CollectionMapperBase<TFromCollection, TFromElement, IEnumerator<TFromElement>, TToElement, TToIntermediate, TToCollection>
            where TFromCollection : class, IEnumerable<TFromElement>
            where TToCollection : class, IEnumerable<TToElement>
    {
        protected sealed override IEnumerator<TFromElement> GetSourceEnumerator(TFromCollection source)
        {
            return source.GetEnumerator();
        }
    }

    public abstract class CollectionMapperBase<TFromCollection, TFromElement, TToElement, TToCollection>
        : CollectionMapperBase<TFromCollection, TFromElement, TToElement, TToCollection, TToCollection>
            where TFromCollection : class, IEnumerable<TFromElement>
            where TToCollection : class, IEnumerable<TToElement>
    {
        protected sealed override TToCollection Complete(ref TToCollection intermediateCollection)
        {
            return intermediateCollection;
        }
    }

    public sealed class LinkedListMapper<TFrom, TTo> : CollectionMapperBase<LinkedList<TFrom>, TFrom, LinkedList<TFrom>.Enumerator, TTo, LinkedList<TTo>, LinkedList<TTo>>
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

    public sealed class LinkedListMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, LinkedList<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref LinkedList<TTo> collection, int index, TTo value)
        {
            collection.AddLast(value);
        }

        protected override LinkedList<TTo> Create(int? count)
        {
            return new LinkedList<TTo>();
        }
    }

    public sealed class QueueMapper<TFrom, TTo> : CollectionMapperBase<Queue<TFrom>, TFrom, Queue<TFrom>.Enumerator, TTo, Queue<TTo>, Queue<TTo>>
    {
        protected override void Add(ref Queue<TTo> collection, int index, TTo value)
        {
            collection.Enqueue(value);
        }

        protected override Queue<TTo> Complete(ref Queue<TTo> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override Queue<TTo> Create(int? count)
        {
            return new Queue<TTo>(count ?? 4);
        }

        protected override Queue<TFrom>.Enumerator GetSourceEnumerator(Queue<TFrom> source)
        {
            return source.GetEnumerator();
        }
    }

    public sealed class QueueMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, Queue<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref Queue<TTo> collection, int index, TTo value)
        {
            collection.Enqueue(value);
        }

        protected override Queue<TTo> Create(int? count)
        {
            return new Queue<TTo>(count ?? 4);
        }
    }

    public sealed class StackMapper<TFrom, TTo> : CollectionMapperBase<Stack<TFrom>, TFrom, Stack<TFrom>.Enumerator, TTo, ArrayBuffer<TTo>, Stack<TTo>>
    {
        protected override void Add(ref ArrayBuffer<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override Stack<TTo> Complete(ref ArrayBuffer<TTo> intermediateCollection)
        {
            // should re-map reverse order.
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

    public sealed class StackMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, ArrayBuffer<TTo>, Stack<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref ArrayBuffer<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override Stack<TTo> Complete(ref ArrayBuffer<TTo> intermediateCollection)
        {
            // should re-map reverse order.
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
    }

    public class HashSetMapper<TFrom, TTo> : IObjectMapper<HashSet<TFrom>, HashSet<TTo>>
    {
        readonly bool useSameComparer;
        protected IEqualityComparer<TTo> EqualityComparer { get; }

        public HashSetMapper()
        {
            if (typeof(TFrom) == typeof(TTo))
            {
                useSameComparer = true;
            }
        }

        public HashSet<TTo> Map(HashSet<TFrom> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var comparer = useSameComparer
                ? (IEqualityComparer<TTo>)(object)from.Comparer
                : EqualityComparer;
            comparer = comparer ?? EqualityComparer<TTo>.Default;

            var mapper = resolver.GetMapperWithVerify<TFrom, TTo>();
            var to = new HashSet<TTo>(comparer);
            foreach (var item in from)
            {
                to.Add(mapper.Map(item, resolver));
            }

            return to;
        }
    }

    public class HashSetMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, HashSet<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected IEqualityComparer<TTo> EqualityComparer { get; }

        protected override void Add(ref HashSet<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override HashSet<TTo> Create(int? count)
        {
            return new HashSet<TTo>(EqualityComparer ?? EqualityComparer<TTo>.Default);
        }
    }

    public sealed class ReadOnlyCollectionMapper<TFrom, TTo> : CollectionMapperBase<ReadOnlyCollection<TFrom>, TFrom, TTo, ArrayBuffer<TTo>, ReadOnlyCollection<TTo>>
    {
        protected override void Add(ref ArrayBuffer<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override ReadOnlyCollection<TTo> Complete(ref ArrayBuffer<TTo> intermediateCollection)
        {
            return new ReadOnlyCollection<TTo>(intermediateCollection.ToArray());
        }

        protected override ArrayBuffer<TTo> Create(int? count)
        {
            return new ArrayBuffer<TTo>(count ?? 4);
        }
    }

    public sealed class ReadOnlyCollectionMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, ArrayBuffer<TTo>, ReadOnlyCollection<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref ArrayBuffer<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override ReadOnlyCollection<TTo> Complete(ref ArrayBuffer<TTo> intermediateCollection)
        {
            return new ReadOnlyCollection<TTo>(intermediateCollection.ToArray());
        }

        protected override ArrayBuffer<TTo> Create(int? count)
        {
            return new ArrayBuffer<TTo>(count ?? 4);
        }
    }

    public class InterfaceListMapper<TFrom, TTo> : InterfaceListMapper<IList<TFrom>, TFrom, TTo>
    {
    }

    public class InterfaceListMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, List<TTo>, IList<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref List<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override IList<TTo> Complete(ref List<TTo> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override List<TTo> Create(int? count)
        {
            return new List<TTo>(count ?? 4);
        }
    }

    public class InterfaceCollectionMapper<TFrom, TTo> : InterfaceCollectionMapper<ICollection<TFrom>, TFrom, TTo>
    {
    }

    public class InterfaceCollectionMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, List<TTo>, ICollection<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref List<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override ICollection<TTo> Complete(ref List<TTo> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override List<TTo> Create(int? count)
        {
            return new List<TTo>(count ?? 4);
        }
    }

    public sealed class ObservableCollectionMapper<TFrom, TTo> : ObservableCollectionMapper<ObservableCollection<TFrom>, TFrom, TTo>
    {
    }

    public class ObservableCollectionMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, ObservableCollection<TTo>, ObservableCollection<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref ObservableCollection<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override ObservableCollection<TTo> Complete(ref ObservableCollection<TTo> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override ObservableCollection<TTo> Create(int? count)
        {
            return new ObservableCollection<TTo>();
        }
    }

    public sealed class ReadOnlyObservableCollectionMapper<TFrom, TTo> : ObservableCollectionMapper<ObservableCollection<TFrom>, TFrom, TTo>
    {
    }

    public class ReadOnlyObservableCollectionMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, ObservableCollection<TTo>, ReadOnlyObservableCollection<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref ObservableCollection<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override ReadOnlyObservableCollection<TTo> Complete(ref ObservableCollection<TTo> intermediateCollection)
        {
            return new ReadOnlyObservableCollection<TTo>(intermediateCollection);
        }

        protected override ObservableCollection<TTo> Create(int? count)
        {
            return new ObservableCollection<TTo>();
        }
    }

    public class InterfaceReadOnlyListMapper<TFrom, TTo> : InterfaceReadOnlyListMapper<IReadOnlyList<TFrom>, TFrom, TTo>
    {
    }

    public class InterfaceReadOnlyListMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, ArrayBuffer<TTo>, IReadOnlyList<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref ArrayBuffer<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override IReadOnlyList<TTo> Complete(ref ArrayBuffer<TTo> intermediateCollection)
        {
            return intermediateCollection.ToArray();
        }

        protected override ArrayBuffer<TTo> Create(int? count)
        {
            return new ArrayBuffer<TTo>(count ?? 4);
        }
    }

    public class InterfaceReadOnlyCollectionMapper<TFrom, TTo> : InterfaceReadOnlyCollectionMapper<IReadOnlyCollection<TFrom>, TFrom, TTo>
    {
    }

    public class InterfaceReadOnlyCollectionMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, ArrayBuffer<TTo>, IReadOnlyCollection<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref ArrayBuffer<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override IReadOnlyCollection<TTo> Complete(ref ArrayBuffer<TTo> intermediateCollection)
        {
            return intermediateCollection.ToArray();
        }

        protected override ArrayBuffer<TTo> Create(int? count)
        {
            return new ArrayBuffer<TTo>(count ?? 4);
        }
    }

    public class InterfaceSetMapper<TFrom, TTo> : InterfaceSetMapper<IReadOnlyCollection<TFrom>, TFrom, TTo>
    {
    }

    public class InterfaceSetMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, HashSet<TTo>, ISet<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected IEqualityComparer<TTo> EqualityComparer { get; }

        protected override void Add(ref HashSet<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override ISet<TTo> Complete(ref HashSet<TTo> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override HashSet<TTo> Create(int? count)
        {
            return new HashSet<TTo>(EqualityComparer ?? EqualityComparer<TTo>.Default);
        }
    }

    public class ConcurrentBagMapper<TFrom, TTo> : ConcurrentBagMapper<ConcurrentBag<TFrom>, TFrom, TTo>
    {
    }

    public class ConcurrentBagMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, ConcurrentBag<TTo>, ConcurrentBag<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref ConcurrentBag<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override ConcurrentBag<TTo> Complete(ref ConcurrentBag<TTo> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override ConcurrentBag<TTo> Create(int? count)
        {
            return new ConcurrentBag<TTo>();
        }
    }

    public class ConcurrentQueueMapper<TFrom, TTo> : ConcurrentQueueMapper<ConcurrentQueue<TFrom>, TFrom, TTo>
    {
    }

    public class ConcurrentQueueMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, ConcurrentQueue<TTo>, ConcurrentQueue<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref ConcurrentQueue<TTo> collection, int index, TTo value)
        {
            collection.Enqueue(value);
        }

        protected override ConcurrentQueue<TTo> Complete(ref ConcurrentQueue<TTo> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override ConcurrentQueue<TTo> Create(int? count)
        {
            return new ConcurrentQueue<TTo>();
        }
    }

    public class ConcurrentStackMapper<TFrom, TTo> : ConcurrentStackMapper<ConcurrentStack<TFrom>, TFrom, TTo>
    {
    }

    public class ConcurrentStackMapper<TFromCollection, TFrom, TTo> : CollectionMapperBase<TFromCollection, TFrom, TTo, ArrayBuffer<TTo>, ConcurrentStack<TTo>>
        where TFromCollection : class, IEnumerable<TFrom>
    {
        protected override void Add(ref ArrayBuffer<TTo> collection, int index, TTo value)
        {
            collection.Add(value);
        }

        protected override ConcurrentStack<TTo> Complete(ref ArrayBuffer<TTo> intermediateCollection)
        {
            // should re-map reverse order.
            var bufArray = intermediateCollection.Buffer;
            var stack = new ConcurrentStack<TTo>();
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
    }

    // NonGenerics

    public sealed class NonGenericListMapper<TFromCollection, TToCollection> : IObjectMapper<TFromCollection, TToCollection>
        where TFromCollection : IEnumerable
        where TToCollection : class, IList, new()
    {
        public TToCollection Map(TFromCollection from, IObjectMapperResolver resolver)
        {
            var mapper = resolver.GetMapperWithVerify<object, object>();
            var list = new TToCollection();
            foreach (var item in from)
            {
                list.Add(mapper.Map(item, resolver));
            }
            return list;
        }
    }

    public sealed class NonGenericInterfaceEnumerableMapper<TFromCollection> : IObjectMapper<TFromCollection, IEnumerable>
        where TFromCollection : IEnumerable
    {
        public IEnumerable Map(TFromCollection from, IObjectMapperResolver resolver)
        {
            var mapper = resolver.GetMapperWithVerify<object, object>();
            var list = new List<object>();
            foreach (var item in from)
            {
                list.Add(mapper.Map(item, resolver));
            }
            return list;
        }
    }

    public sealed class NonGenericInterfaceCollectionMapper<TFromCollection> : IObjectMapper<TFromCollection, ICollection>
        where TFromCollection : IEnumerable
    {
        public ICollection Map(TFromCollection from, IObjectMapperResolver resolver)
        {
            var mapper = resolver.GetMapperWithVerify<object, object>();
            var list = new List<object>();
            foreach (var item in from)
            {
                list.Add(mapper.Map(item, resolver));
            }
            return list;
        }
    }

    public sealed class NonGenericInterfaceListMapper<TFromCollection> : IObjectMapper<TFromCollection, IList>
        where TFromCollection : IEnumerable
    {
        public IList Map(TFromCollection from, IObjectMapperResolver resolver)
        {
            var mapper = resolver.GetMapperWithVerify<object, object>();
            var list = new List<object>();
            foreach (var item in from)
            {
                list.Add(mapper.Map(item, resolver));
            }
            return list;
        }
    }
}
