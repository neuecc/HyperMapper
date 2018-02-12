using HyperMapper.Internal;
using HyperMapper.Internal.Emit;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HyperMapper.Mappers;
using System.Threading;

namespace HyperMapper.Resolvers
{
    /// <summary>
    /// ObjectResolver by dynamic code generation.
    /// </summary>
    public static class DynamicObjectResolver
    {
        public static readonly IObjectMapperResolver Default = DynamicObjectResolverNameMutateOriginal.Instance;
        //public static readonly IObjectMapperResolver CamelCase = DynamicObjectResolverAllowPrivateFalseExcludeNullFalseNameMutateCamelCase.Instance;
        //public static readonly IObjectMapperResolver SnakeCase = DynamicObjectResolverAllowPrivateFalseExcludeNullFalseNameMutateSnakeCase.Instance;
    }


    internal sealed class DynamicObjectResolverNameMutateOriginal : IObjectMapperResolver
    {
        // configuration
        public static readonly IObjectMapperResolver Instance = new DynamicObjectResolverNameMutateOriginal();
        static readonly Func<string, string> nameMutator = StringMutator.Original;

        public IObjectMapper<TFrom, TTo> GetMapper<TFrom, TTo>()
        {
            return FormatterCache<TFrom, TTo>.mapper;
        }

        static class FormatterCache<TFrom, TTo>
        {
            public static readonly IObjectMapper<TFrom, TTo> mapper;

            static FormatterCache()
            {
                mapper = (IObjectMapper<TFrom, TTo>)DynamicObjectTypeBuilder.BuildMapperFromType<TFrom, TTo>(nameMutator);
            }
        }
    }
}
