
using System;
using System.Runtime.CompilerServices;

namespace HyperMapper
{
    public interface IObjectMapperResolver
    {
        IObjectMapper<TFrom, TTo> GetMapper<TFrom, TTo>();
    }

    public interface IObjectMapper<TFrom, TTo>
    {
        TTo Map(TFrom from, IObjectMapperResolver resolver);
    }

    public static class ObjectMapperResolverExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IObjectMapper<TFrom, TTo> GetMapperWithVerify<TFrom, TTo>(this IObjectMapperResolver resolver)
        {
            IObjectMapper<TFrom, TTo> mapper = null;
            try
            {
                mapper = resolver.GetMapper<TFrom, TTo>();
            }
            catch (TypeInitializationException ex)
            {
                UnwrapThrow(ex);
            }

            if (mapper == null)
            {
                ThrowWhenNull<TFrom, TTo>(resolver);
            }

            return mapper;
        }

        static void UnwrapThrow(TypeInitializationException ex)
        {
            Exception inner = ex;
            while (inner.InnerException != null)
            {
                inner = inner.InnerException;
            }

            throw inner;
        }

        static void ThrowWhenNull<TFrom, TTo>(IObjectMapperResolver resolver)
        {
            throw new MapperNotRegisteredException(typeof(TFrom), typeof(TTo), resolver);
        }
    }

    public class MapperNotRegisteredException : Exception
    {
        public Type FromType { get; }
        public Type ToType { get; }
        public IObjectMapperResolver Resolver { get; }

        public MapperNotRegisteredException(Type fromType, Type toType, IObjectMapperResolver resolver)
            : base($"{fromType.FullName} -> {toType.FullName} mapper is not registered in this resolver. Resolver: {resolver.GetType().FullName}")
        {
            this.FromType = fromType;
            this.ToType = toType;
            this.Resolver = resolver;
        }
    }
}
