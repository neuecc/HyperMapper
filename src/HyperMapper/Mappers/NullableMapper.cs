using System;

namespace HyperMapper.Mappers
{
    public class NullableMapperFromNullableStructToClass<TFrom, TTo> : IObjectMapper<TFrom?, TTo>
        where TFrom : struct
        where TTo : class
    {
        public TTo Map(TFrom? from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return resolver.GetMapper<TFrom, TTo>().Map(from.Value, resolver);
        }
    }

    public class NullableMapperFromNullableStructToNullableStruct<TFrom, TTo> : IObjectMapper<TFrom?, TTo?>
        where TFrom : struct
        where TTo : struct
    {
        public TTo? Map(TFrom? from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return resolver.GetMapper<TFrom, TTo>().Map(from.Value, resolver);
        }
    }

    public class NullableMapperFromNullableStructToStruct<TFrom, TTo> : IObjectMapper<TFrom?, TTo>
        where TFrom : struct
        where TTo : struct
    {
        public TTo Map(TFrom? from, IObjectMapperResolver resolver)
        {
            if (from == null) throw new ArgumentNullException("`from` is null but `to` does not allow null.");

            return resolver.GetMapper<TFrom, TTo>().Map(from.Value, resolver);
        }
    }

    public class NullableMapperFromClassToNullableStruct<TFrom, TTo> : IObjectMapper<TFrom, TTo?>
        where TFrom : class
        where TTo : struct
    {
        public TTo? Map(TFrom from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return resolver.GetMapper<TFrom, TTo>().Map(from, resolver);
        }
    }

    public class NullableMapperFromStructToNullableStruct<TFrom, TTo> : IObjectMapper<TFrom, TTo?>
        where TFrom : struct
        where TTo : struct
    {
        public TTo? Map(TFrom from, IObjectMapperResolver resolver)
        {
            return resolver.GetMapper<TFrom, TTo>().Map(from, resolver);
        }
    }
}