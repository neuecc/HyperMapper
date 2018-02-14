using System;
using System.Collections.Generic;
using System.Text;

namespace HyperMapper.Mappers
{
    public class ToStringMapper<T> : IObjectMapper<T, string>
    {
        public string Map(T from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;
            return from.ToString();
        }
    }
}
