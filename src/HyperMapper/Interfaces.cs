
using System;

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
        public static IObjectMapper<TFrom, TTo> GetMapperWithVerify<TFrom, TTo>(this IObjectMapperResolver resolver)
        {
            IObjectMapper<TFrom, TTo> mapper;
            try
            {
                mapper = resolver.GetMapper<TFrom, TTo>();
            }
            catch (TypeInitializationException ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                {
                    inner = inner.InnerException;
                }

                throw inner;
            }

            if (mapper == null)
            {
                throw new MapperNotRegisteredException(typeof(TFrom), typeof(TTo), resolver);
            }

            return mapper;
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
