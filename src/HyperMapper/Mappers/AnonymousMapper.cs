using System;

namespace HyperMapper.Mappers
{
    public sealed class AnonymousMapper<TFrom, TTo> : IObjectMapper<TFrom, TTo>
    {
        readonly Func<TFrom, IObjectMapperResolver, TTo> map;

        public AnonymousMapper(Func<TFrom, IObjectMapperResolver, TTo> map)
        {
            this.map = map;
        }

        public TTo Map(TFrom from, IObjectMapperResolver resolver)
        {
            return map(from, resolver);
        }
    }
}
