using HyperMapper.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HyperMapper.Tests
{
    public class PrimitiveTest
    {
        [Fact]
        public void PrimitiveCast()
        {
            BuiltinResolver.Instance.GetMapper<int, long>().Map(10, BuiltinResolver.Instance).Is(10);
        }
    }
}
