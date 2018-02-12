using HyperMapper.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HyperMapper.Tests
{
    public class EnumTest
    {
        [Theory]
        [InlineData(MyIntEnum.A, MyIntEnum.A)]
        [InlineData(MyLongEnum.B, MyLongEnum.B)]
        [InlineData(MyShortEnum.C, MyShortEnum.C)]
        [InlineData(MyByteEnum.D, MyByteEnum.D)]
        [InlineData(MyUIntEnum.A, MyUIntEnum.A)]
        [InlineData(MyULongEnum.B, MyULongEnum.B)]
        [InlineData(MyUShortEnum.C, MyUShortEnum.C)]
        [InlineData(MySByteEnum.D, MySByteEnum.D)]
        [InlineData(MyIntEnum.A, 1)]
        [InlineData(MyLongEnum.B, 2)]
        [InlineData(MyShortEnum.C, 3)]
        [InlineData(MyByteEnum.D, 4)]
        [InlineData(MyUIntEnum.A, 1)]
        [InlineData(MyULongEnum.B, 2)]
        [InlineData(MyUShortEnum.C, 3)]
        [InlineData(MySByteEnum.D, 4)]
        [InlineData(1, MyIntEnum.A)]
        [InlineData(2, MyLongEnum.B)]
        [InlineData(3, MyShortEnum.C)]
        [InlineData(4, MyByteEnum.D)]
        [InlineData(1, MyUIntEnum.A)]
        [InlineData(2, MyULongEnum.B)]
        [InlineData(3, MyUShortEnum.C)]
        [InlineData(4, MySByteEnum.D)]
        [InlineData(MyIntEnum.A, (long)1)]
        [InlineData(MyLongEnum.B, (byte)2)]
        [InlineData(MyShortEnum.C, (ushort)3)]
        [InlineData(MyByteEnum.D, (ulong)4)]
        [InlineData(MyUIntEnum.A, (short)1)]
        [InlineData(MyULongEnum.B, (sbyte)2)]
        [InlineData(MyUShortEnum.C, (uint)3)]
        [InlineData(MySByteEnum.D, (sbyte)4)]
        [InlineData((short)1, MyIntEnum.A)]
        [InlineData((ulong)2, MyLongEnum.B)]
        [InlineData((sbyte)3, MyShortEnum.C)]
        [InlineData((long)4, MyByteEnum.D)]
        [InlineData((uint)1, MyUIntEnum.A)]
        [InlineData((ushort)2, MyULongEnum.B)]
        [InlineData((ushort)3, MyUShortEnum.C)]
        [InlineData((long)4, MySByteEnum.D)]
        public void EnumPatterns<T, U>(T from, U to)
        {
            BuiltinResolver.Instance.GetMapper<T, U>().Map(from, null).Is(to);
        }

        [Theory]
        [InlineData(MyIntEnum.A, "A")]
        [InlineData(MyLongEnum.B, "B")]
        [InlineData(MyShortEnum.C, "C")]
        [InlineData(MyByteEnum.D, "D")]
        [InlineData(MyUIntEnum.A, "A")]
        [InlineData(MyULongEnum.B, "B")]
        [InlineData(MyUShortEnum.C, "C")]
        [InlineData(MySByteEnum.D, "D")]
        [InlineData("A", MyIntEnum.A)]
        [InlineData("B", MyLongEnum.B)]
        [InlineData("C", MyShortEnum.C)]
        [InlineData("D", MyByteEnum.D)]
        [InlineData("A", MyUIntEnum.A)]
        [InlineData("B", MyULongEnum.B)]
        [InlineData("C", MyUShortEnum.C)]
        [InlineData("D", MySByteEnum.D)]
        public void StringPatterns<T, U>(T from, U to)
        {
            BuiltinResolver.Instance.GetMapper<T, U>().Map(from, null).Is(to);
        }

        [Theory]
        [InlineData(MyIntEnum.A, (char)1)]
        [InlineData(MyLongEnum.B, (char)2)]
        [InlineData(MyShortEnum.C, (char)3)]
        [InlineData(MyByteEnum.D, (char)4)]
        [InlineData((char)1, MyIntEnum.A)]
        [InlineData((char)2, MyLongEnum.B)]
        [InlineData((char)3, MyShortEnum.C)]
        [InlineData((char)4, MyByteEnum.D)]
        public void CahrEnum<T, U>(T from, U to)
        {
            BuiltinResolver.Instance.GetMapper<T, U>().Map(from, null).Is(to);
        }
    }
}
