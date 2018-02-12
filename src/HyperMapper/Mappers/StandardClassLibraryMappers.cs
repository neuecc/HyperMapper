using System;
using HyperMapper.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMapper.Mappers
{
    public sealed class StringBuilderMapper : IObjectMapper<StringBuilder, StringBuilder>
    {
        public StringBuilder Map(StringBuilder from, IObjectMapperResolver resolver)
        {
            return new StringBuilder(from.ToString());
        }
    }

    public sealed class BitArrayMapper : IObjectMapper<BitArray, BitArray>
    {
        public BitArray Map(BitArray from, IObjectMapperResolver resolver)
        {
            return new BitArray(from);
        }
    }

    public sealed class ExpandoObjectMapper : IObjectMapper<ExpandoObject, ExpandoObject>
    {
        public ExpandoObject Map(ExpandoObject from, IObjectMapperResolver resolver)
        {
            var objectMapper = resolver.GetMapperWithVerify<object, object>();

            var to = (IDictionary<string, object>)new ExpandoObject();
            foreach (var item in from)
            {
                to.Add(item.Key, objectMapper.Map(item.Value, resolver));
            }
            return (ExpandoObject)to;
        }
    }

    // generic types...

    public sealed class KeyValuePairMapper<TKey, TValue, UKey, UValue> : IObjectMapper<KeyValuePair<TKey, TValue>, KeyValuePair<UKey, UValue>>
    {
        public KeyValuePair<UKey, UValue> Map(KeyValuePair<TKey, TValue> from, IObjectMapperResolver resolver)
        {
            var keyMapper = resolver.GetMapperWithVerify<TKey, UKey>();
            var valueMapper = resolver.GetMapperWithVerify<TValue, UValue>();

            return new KeyValuePair<UKey, UValue>(keyMapper.Map(from.Key, resolver), valueMapper.Map(from.Value, resolver));
        }
    }

    public sealed class LazyMapper<T, U> : IObjectMapper<Lazy<T>, Lazy<U>>
    {
        public Lazy<U> Map(Lazy<T> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var mapper = resolver.GetMapperWithVerify<T, U>();
            var to = mapper.Map(from.Value, resolver);
            return new Lazy<U>(to.AsFunc());
        }
    }

    public sealed class TaskValueMapper<T, U> : IObjectMapper<Task<T>, Task<U>>
    {
        public Task<U> Map(Task<T> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var mapper = resolver.GetMapperWithVerify<T, U>();
            var to = mapper.Map(from.Result, resolver); // task wait...!
            return Task.FromResult(to);
        }
    }

    public sealed class ValueTaskMapper<T, U> : IObjectMapper<ValueTask<T>, ValueTask<U>>
    {
        public ValueTask<U> Map(ValueTask<T> from, IObjectMapperResolver resolver)
        {
            var mapper = resolver.GetMapperWithVerify<T, U>();
            var to = mapper.Map(from.Result, resolver); // task wait...!
            return new ValueTask<U>(to);
        }
    }

    public class InterfaceGroupingMapper<TFromKey, TFromElement, TToKey, TToElement> : IObjectMapper<IGrouping<TFromKey, TFromElement>, IGrouping<TToKey, TToElement>>
    {
        public IGrouping<TToKey, TToElement> Map(IGrouping<TFromKey, TFromElement> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var keyMapper = resolver.GetMapperWithVerify<TFromKey, TToKey>();
            var elementMapper = resolver.GetMapperWithVerify<TFromElement, TToElement>();

            var key = keyMapper.Map(from.Key, resolver);
            var values = new ArrayBuffer<TToElement>(from.FastCount() ?? 4);
            foreach (var item in from)
            {
                values.Add(elementMapper.Map(item, resolver));
            }

            return new Grouping<TToKey, TToElement>(key, values.ToArray());
        }
    }

    public class InterfaceLookupMapper<TFromKey, TFromElement, TToKey, TToElement> : IObjectMapper<ILookup<TFromKey, TFromElement>, ILookup<TToKey, TToElement>>
    {
        public ILookup<TToKey, TToElement> Map(ILookup<TFromKey, TFromElement> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var groupingMapper = resolver.GetMapperWithVerify<IGrouping<TFromKey, TFromElement>, IGrouping<TToKey, TToElement>>();

            var dict = new Dictionary<TToKey, IGrouping<TToKey, TToElement>>();
            foreach (var item in from)
            {
                var v = groupingMapper.Map(item, resolver);
                dict.Add(v.Key, v);
            }

            return new Lookup<TToKey, TToElement>(dict);
        }
    }

    class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        readonly TKey key;
        readonly IEnumerable<TElement> elements;

        public Grouping(TKey key, IEnumerable<TElement> elements)
        {
            this.key = key;
            this.elements = elements;
        }

        public TKey Key
        {
            get
            {
                return key;
            }
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Lookup<TKey, TElement> : ILookup<TKey, TElement>
    {
        readonly Dictionary<TKey, IGrouping<TKey, TElement>> groupings;

        public Lookup(Dictionary<TKey, IGrouping<TKey, TElement>> groupings)
        {
            this.groupings = groupings;
        }

        public IEnumerable<TElement> this[TKey key]
        {
            get
            {
                return groupings[key];
            }
        }

        public int Count
        {
            get
            {
                return groupings.Count;
            }
        }

        public bool Contains(TKey key)
        {
            return groupings.ContainsKey(key);
        }

        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            return groupings.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return groupings.Values.GetEnumerator();
        }
    }
}