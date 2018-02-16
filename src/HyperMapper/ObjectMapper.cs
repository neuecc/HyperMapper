using System;
using System.Runtime.CompilerServices;

namespace HyperMapper
{
    public static class ObjectMapper
    {
        static IObjectMapperResolver defaultResolver;

        /// <summary>
        /// ObjectMapperResolver that used resolver less overloads. If does not set it, used StandardResolver.Default.
        /// </summary>
        public static IObjectMapperResolver DefaultResolver
        {
            get
            {
                if (defaultResolver == null)
                {
                    defaultResolver = HyperMapper.Resolvers.StandardResolver.Default;
                }

                return defaultResolver;
            }
        }

        /// <summary>
        /// Is resolver decided?
        /// </summary>
        public static bool IsInitialized
        {
            get
            {
                return defaultResolver != null;
            }
        }

        /// <summary>
        /// Set default resolver of HyperMapper APIs.
        /// </summary>
        /// <param name="resolver"></param>
        public static void SetDefaultResolver(IObjectMapperResolver resolver)
        {
            defaultResolver = resolver;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DeepCopy<T>(T cloneFrom)
        {
            var resolver = DefaultResolver;
            return resolver.GetMapperWithVerify<T, T>().Map(cloneFrom, resolver);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DeepCopy<T>(T cloneFrom, IObjectMapperResolver resolver)
        {
            return resolver.GetMapperWithVerify<T, T>().Map(cloneFrom, resolver);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TTo Map<TFrom, TTo>(TFrom from)
        {
            var resolver = DefaultResolver;
            return resolver.GetMapperWithVerify<TFrom, TTo>().Map(from, resolver);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TTo Map<TFrom, TTo>(TFrom from, IObjectMapperResolver resolver)
        {
            return resolver.GetMapperWithVerify<TFrom, TTo>().Map(from, resolver);
        }

        public static Chain<TFrom> From<TFrom>(TFrom from)
        {
            return new Chain<TFrom>(from, DefaultResolver);
        }

        public static Chain<TFrom> From<TFrom>(TFrom from, IObjectMapperResolver resolver)
        {
            return new Chain<TFrom>(from, resolver);
        }

        public struct Chain<T>
        {
            readonly T from;
            readonly IObjectMapperResolver resolver;

            public Chain(T t, IObjectMapperResolver resolver)
            {
                this.from = t;
                this.resolver = resolver;
            }

            public TTo To<TTo>()
            {
                return ObjectMapper.Map<T, TTo>(from, resolver);
            }
        }
    }

}