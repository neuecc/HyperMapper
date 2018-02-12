using HyperMapper.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HyperMapper.Tests
{
    public class ValueTupleTest
    {
        public static object[][] valueTupleData = new object[][]
        {
            new object[] { (1, 2) },
            new object[] { (1, 2, 3) },
            new object[] { (1, 2, 3, 4) },
            new object[] { (1, 2, 3, 4, 5) },
            new object[] { (1, 2, 3, 4, 5,6) },
            new object[] { (1, 2, 3, 4, 5,6,7) },
            new object[] { (1, 2, 3, 4, 5,6,7,8) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11,12) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11,12,13) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14,15) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14,15,16) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14,15,16,17) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14,15,16,17,18) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14,15,16,17,18,19) },
            new object[] { (1, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20) },
        };

       public static object[][] standardTupleData = new object[][]
       {
            new object[] { Tuple.Create(1, 2) },
            new object[] { Tuple.Create(1, 2, 3) },
            new object[] { Tuple.Create(1, 2, 3, 4) },
            new object[] { Tuple.Create(1, 2, 3, 4, 5) },
            new object[] { Tuple.Create(1, 2, 3, 4, 5,6) },
            new object[] { Tuple.Create(1, 2, 3, 4, 5,6,7) },
            new object[] { Tuple.Create(1, 2, 3, 4, 5,6,7,8) },
       };

        [Theory]
        [MemberData(nameof(valueTupleData))]
        public void ValueTuple<T>(T x)
        {
            BuiltinResolver.Instance.GetMapper<T, T>().Map(x, BuiltinResolver.Instance).Is(x);
        }

        [Theory]
        [MemberData(nameof(standardTupleData))]
        public void Standarduple<T>(T x)
        {
            BuiltinResolver.Instance.GetMapper<T, T>().Map(x, BuiltinResolver.Instance).Is(x);
        }
    }
}
