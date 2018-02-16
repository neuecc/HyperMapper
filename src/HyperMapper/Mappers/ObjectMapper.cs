using HyperMapper.Internal;
using HyperMapper.Internal.Emit;
using System;
using System.Reflection.Emit;

namespace HyperMapper.Mappers
{
    public class ConcreteTypeMapper<TFrom, TTo> : IObjectMapper<TFrom, TTo>
        where TFrom : TTo
        where TTo : class
    {
        static readonly ThreadsafeTypeKeyHashTable<Func<TFrom, IObjectMapperResolver, TTo>> cache = new ThreadsafeTypeKeyHashTable<Func<TFrom, IObjectMapperResolver, TTo>>();

        public TTo Map(TFrom from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var actualType = from.GetType();
            if (actualType == typeof(object)) return (TTo)new object();

            if (cache.TryGetValue(actualType, out var mapper))
            {
                return mapper(from, resolver);
            }
            else
            {
                return GetOrAddMapper(actualType).Invoke(from, resolver);
            }
        }

        static Func<TFrom, IObjectMapperResolver, TTo> GetOrAddMapper(Type type)
        {
            return cache.GetOrAdd(type, castType =>
            {
                var mapper = new DynamicMethod("Map", typeof(TTo), new[] { typeof(TFrom), typeof(IObjectMapperResolver) }, true);
                var il = mapper.GetILGenerator();

                il.EmitLdarg(1); // resolver
                il.EmitCall(typeof(IObjectMapperResolver).GetMethod("GetMapper").MakeGenericMethod(castType, castType));
                il.EmitLdarg(0);
                if (typeof(TFrom) == castType)
                {
                    // do nothing.
                }
                else
                {
                    il.Emit(OpCodes.Castclass, castType);
                }
                il.EmitLdarg(1);
                il.EmitCall(typeof(IObjectMapper<,>).MakeGenericType(castType, castType).GetMethod("Map"));
                if (castType.IsValueType)
                {
                    il.Emit(OpCodes.Box, castType);
                }
                else
                {
                    il.Emit(OpCodes.Castclass, typeof(TTo));
                }
                il.Emit(OpCodes.Ret);

                return (Func<TFrom, IObjectMapperResolver, TTo>)mapper.CreateDelegate(typeof(Func<TFrom, IObjectMapperResolver, TTo>));
            });
        }
    }
}