using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperMapper.Resolvers
{
    public class StandardResolver : IObjectMapperResolver
    {
        public static IObjectMapperResolver Default = new StandardResolver();

        static readonly IObjectMapperResolver[] resolvers = new IObjectMapperResolver[]
        {
            BuiltinResolver.Instance,
            DynamicObjectResolver.Default,
        };

        StandardResolver()
        {

        }

        public IObjectMapper<TFrom, TTo> GetMapper<TFrom, TTo>()
        {
            return Cache<TFrom, TTo>.mapper;
        }

        static class Cache<TFrom, TTo>
        {
            public static readonly IObjectMapper<TFrom, TTo> mapper;

            static Cache()
            {
                foreach (var item in resolvers)
                {
                    var m = item.GetMapper<TFrom, TTo>();
                    if (m != null)
                    {
                        mapper = m;
                        return;
                    }
                }
            }
        }
    }
}
