using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyperMapper.Internal;

namespace HyperMapper.Mappers
{
    public abstract class DictionaryMapperrBase<TFromDictionary, TFromKey, TFromValue, TFromEnumerator, TToIntermediate, TToDictionary, TToKey, TToValue>
        where TFromDictionary : class, IDictionary<TFromKey, TFromValue>
        where TFromEnumerator : IEnumerator<KeyValuePair<TFromKey, TFromValue>>
        where TToDictionary : class, IEnumerable<KeyValuePair<TToKey, TToValue>>
    {
        protected abstract TFromEnumerator GetSourceEnumerator(TFromDictionary source);
        protected abstract TToIntermediate Create(TFromDictionary from);
        protected abstract void Add(ref TToIntermediate collection, int index, TToKey key, TToValue value);
        protected abstract TToDictionary Complete(ref TToIntermediate intermediateCollection);

        public TToDictionary Map(TFromDictionary from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var keyMapper = resolver.GetMapperWithVerify<TFromKey, TToKey>();
            var valueMapper = resolver.GetMapperWithVerify<TFromValue, TToValue>();

            var builder = Create(from);

            using (var e = GetSourceEnumerator(from))
            {
                var index = 0;
                while (e.MoveNext())
                {
                    var key = keyMapper.Map(e.Current.Key, resolver);
                    var value = valueMapper.Map(e.Current.Value, resolver);
                    Add(ref builder, index, key, value);
                    index++;
                }
            }

            return Complete(ref builder);
        }
    }

    public class DictionaryMapper<TFromKey, TFromValue, TToKey, TToValue> : DictionaryMapperrBase<Dictionary<TFromKey, TFromValue>, TFromKey, TFromValue, Dictionary<TFromKey, TFromValue>.Enumerator, Dictionary<TToKey, TToValue>, Dictionary<TToKey, TToValue>, TToKey, TToValue>
    {
        protected IEqualityComparer<TToKey> EqualityComparer { get; }

        protected override void Add(ref Dictionary<TToKey, TToValue> collection, int index, TToKey key, TToValue value)
        {
            collection.Add(key, value);
        }

        protected override Dictionary<TToKey, TToValue> Complete(ref Dictionary<TToKey, TToValue> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override Dictionary<TToKey, TToValue> Create(Dictionary<TFromKey, TFromValue> from)
        {
            return new Dictionary<TToKey, TToValue>(from.Count, EqualityComparer);
        }

        protected override Dictionary<TFromKey, TFromValue>.Enumerator GetSourceEnumerator(Dictionary<TFromKey, TFromValue> source)
        {
            return source.GetEnumerator();
        }
    }

    public class DictionaryMapper<TKey, TValue> : DictionaryMapper<TKey, TValue, TKey, TValue>
    {
        protected override Dictionary<TKey, TValue> Create(Dictionary<TKey, TValue> from)
        {
            return new Dictionary<TKey, TValue>(from.Count, from.Comparer);
        }
    }




}
