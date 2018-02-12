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

    public sealed class KeyValuePairMapper<TKey, TValue> : IObjectMapper<KeyValuePair<TKey, TValue>, KeyValuePair<TKey, TValue>>
    {
        public KeyValuePair<TKey, TValue> Map(KeyValuePair<TKey, TValue> from, IObjectMapperResolver resolver)
        {
            var keyMapper = resolver.GetMapperWithVerify<TKey, TKey>();
            var valueMapper = resolver.GetMapperWithVerify<TValue, TValue>();

            return new KeyValuePair<TKey, TValue>(keyMapper.Map(from.Key, resolver), valueMapper.Map(from.Value, resolver));
        }
    }

    public sealed class LazyObjectMapper<T> : IObjectMapper<Lazy<T>, Lazy<T>>
    {
        public Lazy<T> Map(Lazy<T> from, IObjectMapperResolver resolver)
        {
            var mapper = resolver.GetMapperWithVerify<T, T>();
            var to = mapper.Map(from.Value, resolver);
            return new Lazy<T>(to.AsFunc());
        }
    }

    public sealed class TaskValueMapper<T> : IObjectMapper<Task<T>, Task<T>>
    {
        public Task<T> Map(Task<T> from, IObjectMapperResolver resolver)
        {
            var mapper = resolver.GetMapperWithVerify<T, T>();
            var to = mapper.Map(from.Result, resolver); // task wait...!
            return Task.FromResult(to);
        }
    }

    public sealed class ValueTaskMapper<T> : IObjectMapper<ValueTask<T>, ValueTask<T>>
    {
        public ValueTask<T> Map(ValueTask<T> from, IObjectMapperResolver resolver)
        {
            var mapper = resolver.GetMapperWithVerify<T, T>();
            var to = mapper.Map(from.Result, resolver); // task wait...!
            return new ValueTask<T>(to);
        }
    }
}