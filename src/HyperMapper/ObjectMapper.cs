using System;

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

        public static T DeepCopy<T>(T cloneFrom)
        {
            var resolver = DefaultResolver;
            return resolver.GetMapperWithVerify<T, T>().Map(cloneFrom, resolver);
        }

        public static TTo Map<TFrom, TTo>(TFrom from)
        {
            var resolver = DefaultResolver;
            return resolver.GetMapperWithVerify<TFrom, TTo>().Map(from, resolver);
        }
    }
}
