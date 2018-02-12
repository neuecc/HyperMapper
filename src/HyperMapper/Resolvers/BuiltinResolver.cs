using HyperMapper.Mappers;
using HyperMapper.Internal;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using System.Text;
using System.Collections;
using System.Dynamic;

namespace HyperMapper.Resolvers
{
    public class BuiltinResolver : IObjectMapperResolver
    {
        public static IObjectMapperResolver Instance = new BuiltinResolver();

        BuiltinResolver()
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
                {typeof(Nullable<decimal>[]),        new ShallowCopyArrayMapper<decimal?>()},

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
                {typeof(Guid), new ReturnSelfMapper<Guid>()},
                {typeof(Guid?), new ReturnSelfMapper<Guid?>()},
                {typeof(Guid[]), new ShallowCopyArrayMapper<Guid>()},
                {typeof(Uri), new ReturnSelfMapper<Uri>()},
                {typeof(Uri[]), new ShallowCopyArrayMapper<Uri>()},
                {typeof(Version), new ReturnSelfMapper<Version>()},
                {typeof(Version[]), new ShallowCopyArrayMapper<Version>()},
                {typeof(Type), new ReturnSelfMapper<Type>()},
                {typeof(Type[]), new ShallowCopyArrayMapper<Type>()},
                {typeof(BigInteger), new ReturnSelfMapper<BigInteger>()},
                {typeof(BigInteger?), new ReturnSelfMapper<BigInteger?>()},
                {typeof(BigInteger[]), new ShallowCopyArrayMapper<BigInteger>()},
                {typeof(Complex), new ReturnSelfMapper<Complex>()},
                {typeof(Complex?), new ReturnSelfMapper<Complex?>()},
                {typeof(Complex[]), new ShallowCopyArrayMapper<Complex>()},
                {typeof(Task), new ReturnSelfMapper<Task>()},
                {typeof(Task[]), new ShallowCopyArrayMapper<Task>()},

                // others...
                {typeof(StringBuilder), new StringBuilderMapper() },
                {typeof(BitArray), new BitArrayMapper() },
                {typeof(ExpandoObject), new ExpandoObjectMapper() },
            };

            public static object CreateMapper(Type from, Type to)
            {
                var mapper = TryCreateNullableMapper(from, to);
                if (mapper != null) return mapper;

                mapper = TryCreateEnumMapper(from, to);
                if (mapper != null) return mapper;

                if (from == to)
                {
                    if (mapperMap.TryGetValue(from, out mapper))
                    {
                        return mapper;
                    }

                    if (typeof(Delegate).IsAssignableFrom(from))
                    {
                        return CreateGenericReturnSelfMapper(from);
                    }

                    mapper = TryCreateWellKnownGenericTypeMapper(from, to);
                    if (mapper != null) return mapper;

                    mapper = TryCreateTupleMapper(from, to);
                    if (mapper != null) return mapper;

                    mapper = TryCreateCollectionMapper(from, to);
                    if (mapper != null) return mapper;
                }

                // allow not same...
                mapper = TryCreateExceptionMapper(from, to);
                if (mapper != null) return mapper;

                return null;
            }

            static object CreateGenericReturnSelfMapper(Type type) => Activator.CreateInstance(typeof(ReturnSelfMapper<>).MakeGenericType(type));

            static object TryCreateNullableMapper(Type from, Type to)
            {
                var isFromNullable = from.IsNullable();
                var isToNullable = to.IsNullable();

                if (isFromNullable && isToNullable)
                {
                    // unwrap nullable type
                    return Activator.CreateInstance(typeof(NullableMapperFromNullableStructToNullableStruct<,>).MakeGenericType(from.GenericTypeArguments[0], to.GenericTypeArguments[0]));
                }
                else if (isFromNullable)
                {
                    if (to.IsValueType)
                    {
                        return Activator.CreateInstance(typeof(NullableMapperFromNullableStructToStruct<,>).MakeGenericType(from.GenericTypeArguments[0], to));
                    }
                    else
                    {
                        return Activator.CreateInstance(typeof(NullableMapperFromNullableStructToClass<,>).MakeGenericType(from.GenericTypeArguments[0], to));
                    }
                }
                else if (isToNullable)
                {
                    if (from.IsValueType)
                    {
                        return Activator.CreateInstance(typeof(NullableMapperFromStructToNullableStruct<,>).MakeGenericType(from, to.GenericTypeArguments[0]));
                    }
                    else
                    {
                        return Activator.CreateInstance(typeof(NullableMapperFromClassToNullableStruct<,>).MakeGenericType(from, to.GenericTypeArguments[0]));
                    }
                }

                return null;
            }

            static object TryCreateEnumMapper(Type from, Type to)
            {
                if (from == to)
                {
                    if (from.IsEnum)
                    {
                        return CreateGenericReturnSelfMapper(from);
                    }
                }
                else if (from.IsEnum && to.IsEnum)
                {
                    // convert type
                    return EnumToEnumMapperBuilder.Build(from, to);
                }
                else if (from.IsEnum)
                {
                    if (to == typeof(string))
                    {
                        return Activator.CreateInstance(typeof(EnumToStringMapper<>).MakeGenericType(from));
                    }
                    else
                    {
                        // enum to primitive(or enum)
                        return EnumToEnumMapperBuilder.Build(from, to);
                    }
                }
                else if (to.IsEnum)
                {
                    if (from == typeof(string))
                    {
                        return Activator.CreateInstance(typeof(StringToEnumMapper<>).MakeGenericType(to));
                    }
                    else
                    {
                        // primitive(or enum) to enum
                        return EnumToEnumMapperBuilder.Build(from, to);
                    }
                }

                return null;
            }

            static object TryCreateWellKnownGenericTypeMapper(Type from, Type to)
            {
                // KVP<TKey,TValue>, Lazy<T>, Task<T>, ValueTask<T>

                throw new NotImplementedException();
            }

            static object TryCreateTupleMapper(Type from, Type to)
            {
                // Tuple or ValueTuple
                throw new NotImplementedException();
            }

            static object TryCreateCollectionMapper(Type from, Type to)
            {
                throw new NotImplementedException();
            }

            static object TryCreateExceptionMapper(Type from, Type to)
            {
                throw new NotImplementedException();
            }
        }
    }
}