using HyperMapper.Mappers;
using System;
using System.Collections.Generic;

namespace HyperMapper.Resolvers
{
    public class BuiltinResolver : IObjectMapperResolver
    {
        public IObjectMapper<TFrom, TTo> GetMapper<TFrom, TTo>()
        {
            return Cache<TFrom, TTo>.mapper;
        }

        static class Cache<TFrom, TTo>
        {
            public static readonly IObjectMapper<TFrom, TTo> mapper;

            static Cache()
            {
                mapper = (IObjectMapper<TFrom, TTo>)Helper.CreateMapper(typeof(TFrom), typeof(TTo));
            }
        }

        static class Helper
        {
            static readonly Dictionary<Type, object> mapperMap = new Dictionary<Type, object>()
            {
                // Primitive
                {typeof(Int16),  new ReturnSelfMapper<Int16>()},
                {typeof(Int32),  new ReturnSelfMapper<Int32>()},
                {typeof(Int64),  new ReturnSelfMapper<Int64>()},
                {typeof(UInt16), new ReturnSelfMapper<UInt16>()},
                {typeof(UInt32), new ReturnSelfMapper<UInt32>()},
                {typeof(UInt64), new ReturnSelfMapper<UInt64>()},
                {typeof(Single), new ReturnSelfMapper<Single>()},
                {typeof(Double), new ReturnSelfMapper<Double>()},
                {typeof(bool),   new ReturnSelfMapper<bool>()},
                {typeof(byte),   new ReturnSelfMapper<byte>()},
                {typeof(sbyte),  new ReturnSelfMapper<sbyte>()},
                {typeof(char),   new ReturnSelfMapper<char>()},
                {typeof(decimal),new ReturnSelfMapper<decimal>()},

                // Nulllable Primitive
                {typeof(Nullable<Int16>), new ReturnSelfMapper<Int16?>()},
                {typeof(Nullable<Int32>), new ReturnSelfMapper<Int32?>()},
                {typeof(Nullable<Int64>), new ReturnSelfMapper<Int64?>()},
                {typeof(Nullable<UInt16>),new ReturnSelfMapper<UInt16?>()},
                {typeof(Nullable<UInt32>),new ReturnSelfMapper<UInt32?>()},
                {typeof(Nullable<UInt64>),new ReturnSelfMapper<UInt64?>()},
                {typeof(Nullable<Single>),new ReturnSelfMapper<Single?>()},
                {typeof(Nullable<Double>),new ReturnSelfMapper<Double?>()},
                {typeof(Nullable<bool>),  new ReturnSelfMapper<bool?>()},
                {typeof(Nullable<byte>),  new ReturnSelfMapper<byte?>()},
                {typeof(Nullable<sbyte>), new ReturnSelfMapper<sbyte?>()},
                {typeof(Nullable<char>),  new ReturnSelfMapper<char?>()},
                {typeof(decimal?),        new ReturnSelfMapper<decimal?>()},

                // Primitive Array
                {typeof(Int16[]),  new BlockCopyMapper<Int16>(sizeof(Int16))},
                {typeof(Int32[]),  new BlockCopyMapper<Int32>(sizeof(Int32))},
                {typeof(Int64[]),  new BlockCopyMapper<Int64>(sizeof(Int64))},
                {typeof(UInt16[]), new BlockCopyMapper<UInt16>(sizeof(UInt16))},
                {typeof(UInt32[]), new BlockCopyMapper<UInt32>(sizeof(UInt32))},
                {typeof(UInt64[]), new BlockCopyMapper<UInt64>(sizeof(UInt64))},
                {typeof(Single[]), new BlockCopyMapper<Single>(sizeof(Single))},
                {typeof(Double[]), new BlockCopyMapper<Double>(sizeof(Double))},
                {typeof(bool[]),   new BlockCopyMapper<bool>(sizeof(bool))},
                {typeof(byte[]),   new BlockCopyMapper<byte>(sizeof(byte))},
                {typeof(sbyte[]),  new BlockCopyMapper<sbyte>(sizeof(sbyte))},
                {typeof(char[]),   new BlockCopyMapper<char>(sizeof(char))},
                {typeof(decimal[]),new BlockCopyMapper<decimal>(sizeof(decimal))},

                // Primitive Nullable Array
                {typeof(Nullable<Int16>[]), new ShallowCopyArrayMapper<Int16?>()},
                {typeof(Nullable<Int32>[]), new ShallowCopyArrayMapper<Int32?>()},
                {typeof(Nullable<Int64>[]), new ShallowCopyArrayMapper<Int64?>()},
                {typeof(Nullable<UInt16>[]),new ShallowCopyArrayMapper<UInt16?>()},
                {typeof(Nullable<UInt32>[]),new ShallowCopyArrayMapper<UInt32?>()},
                {typeof(Nullable<UInt64>[]),new ShallowCopyArrayMapper<UInt64?>()},
                {typeof(Nullable<Single>[]),new ShallowCopyArrayMapper<Single?>()},
                {typeof(Nullable<Double>[]),new ShallowCopyArrayMapper<Double?>()},
                {typeof(Nullable<bool>[]),  new ShallowCopyArrayMapper<bool?>()},
                {typeof(Nullable<byte>[]),  new ShallowCopyArrayMapper<byte?>()},
                {typeof(Nullable<sbyte>[]), new ShallowCopyArrayMapper<sbyte?>()},
                {typeof(Nullable<char>[]),  new ShallowCopyArrayMapper<char?>()},
                {typeof(decimal?[]),        new ShallowCopyArrayMapper<decimal?>()},

                // String
                {typeof(string), new ReturnSelfMapper<string>()},
                {typeof(string[]), new ShallowCopyArrayMapper<string>()},

                // Standard Structs or immutable classes
                
                {typeof(DateTime), new ReturnSelfMapper<DateTime>()},
                {typeof(DateTime?), new ReturnSelfMapper<DateTime?>()},
                {typeof(DateTime[]), new ShallowCopyArrayMapper<DateTime>()},
                {typeof(TimeSpan), new ReturnSelfMapper<TimeSpan>()},
                {typeof(TimeSpan?), new ReturnSelfMapper<TimeSpan?>()},
                {typeof(TimeSpan[]), new ShallowCopyArrayMapper<TimeSpan>()},
                {typeof(DateTimeOffset), new ReturnSelfMapper<DateTimeOffset>()},
                {typeof(DateTimeOffset?), new ReturnSelfMapper<DateTimeOffset?>()},
                {typeof(DateTimeOffset[]), new ShallowCopyArrayMapper<DateTimeOffset>()},


                

                //{typeof(Guid), GuidFormatter.Default},
                //{typeof(Guid?), new StaticNullableFormatter<Guid>(GuidFormatter.Default)},

                //{typeof(Uri), UriFormatter.Default},
                //{typeof(Version), VersionFormatter.Default},

                //{ typeof(StringBuilder), StringBuilderFormatter.Default},
                //{typeof(BitArray), BitArrayFormatter.Default},
                //{typeof(Type), TypeFormatter.Default},
            
           


                //{ typeof(ArraySegment<byte>), ByteArraySegmentFormatter.Default },
                //{ typeof(ArraySegment<byte>?),new StaticNullableFormatter<ArraySegment<byte>>(ByteArraySegmentFormatter.Default) },

                //{typeof(System.Numerics.BigInteger), BigIntegerFormatter.Default},
                //{typeof(System.Numerics.BigInteger?), new StaticNullableFormatter<System.Numerics.BigInteger>(BigIntegerFormatter.Default)},
                //{typeof(System.Numerics.Complex), ComplexFormatter.Default},
                //{typeof(System.Numerics.Complex?), new StaticNullableFormatter<System.Numerics.Complex>(ComplexFormatter.Default)},
                //{typeof(System.Dynamic.ExpandoObject), ExpandoObjectFormatter.Default },
                //{typeof(System.Threading.Tasks.Task), TaskUnitFormatter.Default},
            };

            static object CreateGenericReturnSelfMapper(Type type) => Activator.CreateInstance(typeof(ReturnSelfMapper<>).MakeGenericType(type));

            public static object CreateMapper(Type from, Type to)
            {
                if (from.IsEnum || to.IsEnum)
                {
                    return CreateEnumMapper(from, to);
                }
                else if (from == to)
                {
                    if (mapperMap.TryGetValue(from, out var mapper))
                    {
                        return mapper;
                    }

                    if (typeof(Delegate).IsAssignableFrom(from))
                    {
                        return CreateGenericReturnSelfMapper(from);
                    }
                }
                else
                {
                    var mapper = TryCreateCollectionMapper(from, to);
                    if (mapper != null) return mapper;

                    mapper = TryCreateWellKnownGenericTypeMapper(from, to);
                    if (mapper != null) return mapper;
                }

                return null;
            }

            static object CreateEnumMapper(Type from, Type to)
            {
                if (from == to)
                {
                    if (from.IsEnum)
                    {
                        return CreateGenericReturnSelfMapper(from);
                    }

                    // TODO:enum to primitive

                    // TODO:primitive to enum
                }

                throw new NotImplementedException();
            }

            static object TryCreateCollectionMapper(Type from, Type to)
            {
                throw new NotImplementedException();
            }

            static object TryCreateWellKnownGenericTypeMapper(Type from, Type to)
            {
                throw new NotImplementedException();
            }
        }
    }
}