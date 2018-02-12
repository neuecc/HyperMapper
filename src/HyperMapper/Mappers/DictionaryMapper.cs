using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMapper.Mappers
{
    // TODO:Any more more...


    public sealed class DictionaryMapper<TKey, TValue> : IObjectMapper<Dictionary<TKey, TValue>, Dictionary<TKey, TValue>>
    {
        public Dictionary<TKey, TValue> Map(Dictionary<TKey, TValue> obj, IObjectMapperResolver resolver)
        {
            if (obj == null) return null;

            var dict = new Dictionary<TKey, TValue>(obj.Count, obj.Comparer);

            var keyMapper = resolver.GetMapperWithVerify<TKey, TKey>();
            var valueMapper = resolver.GetMapperWithVerify<TValue, TValue>();

            foreach (var item in obj)
            {
                var key = keyMapper.Map(item.Key, resolver);
                var value = valueMapper.Map(item.Value, resolver);
                dict.Add(key, value);
            }

            return dict;
        }
    }

    public sealed class DictionaryMapper<TFromTKey, TFromTValue, TToKey, TToValue> : IObjectMapper<IDictionary<TFromTKey, TFromTValue>, Dictionary<TToKey, TToValue>>
    {
        public Dictionary<TToKey, TToValue> Map(IDictionary<TFromTKey, TFromTValue> obj, IObjectMapperResolver resolver)
        {
            if (obj == null) return null;

            var dict = new Dictionary<TToKey, TToValue>(obj.Count); // equality comparer?

            var keyMapper = resolver.GetMapperWithVerify<TFromTKey, TToKey>();
            var valueMapper = resolver.GetMapperWithVerify<TFromTValue, TToValue>();

            foreach (var item in obj)
            {
                var key = keyMapper.Map(item.Key, resolver);
                var value = valueMapper.Map(item.Value, resolver);
                dict.Add(key, value);
            }

            return dict;
        }
    }
}
