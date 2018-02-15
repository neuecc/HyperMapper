using HyperMapper.Mappers;
using System.Linq;
using HyperMapper.Internal;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using System.Text;
using System.Collections;
using System.Dynamic;
using System.Collections.ObjectModel;

namespace HyperMapper.Resolvers
{
    public sealed class BuiltinResolver : IObjectMapperResolver
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
                {typeof(Int16[]),  new Int16MemoryCopyMapper()},
                {typeof(Int32[]),  new Int32MemoryCopyMapper()},
                {typeof(Int64[]),  new Int64MemoryCopyMapper()},
                {typeof(UInt16[]), new UInt16MemoryCopyMapper()},
                {typeof(UInt32[]), new UInt32MemoryCopyMapper()},
                {typeof(UInt64[]), new UInt64MemoryCopyMapper()},
                {typeof(Single[]), new SingleMemoryCopyMapper()},
                {typeof(Double[]), new DoubleMemoryCopyMapper()},
                {typeof(bool[]),   new BooleanMemoryCopyMapper()},
                {typeof(byte[]),   new ByteMemoryCopyMapper()},
                {typeof(sbyte[]),  new SByteMemoryCopyMapper()},
                {typeof(char[]),   new CharMemoryCopyMapper()},
                {typeof(decimal[]),new DecimalMemoryCopyMapper()},

                // Primitive Nullable Array
                {typeof(Nullable<Int16>[]),  new ShallowCopyArrayMapper<Int16?>()},
                {typeof(Nullable<Int32>[]),  new ShallowCopyArrayMapper<Int32?>()},
                {typeof(Nullable<Int64>[]),  new ShallowCopyArrayMapper<Int64?>()},
                {typeof(Nullable<UInt16>[]), new ShallowCopyArrayMapper<UInt16?>()},
                {typeof(Nullable<UInt32>[]), new ShallowCopyArrayMapper<UInt32?>()},
                {typeof(Nullable<UInt64>[]), new ShallowCopyArrayMapper<UInt64?>()},
                {typeof(Nullable<Single>[]), new ShallowCopyArrayMapper<Single?>()},
                {typeof(Nullable<Double>[]), new ShallowCopyArrayMapper<Double?>()},
                {typeof(Nullable<bool>[]),   new ShallowCopyArrayMapper<bool?>()},
                {typeof(Nullable<byte>[]),   new ShallowCopyArrayMapper<byte?>()},
                {typeof(Nullable<sbyte>[]),  new ShallowCopyArrayMapper<sbyte?>()},
                {typeof(Nullable<char>[]),   new ShallowCopyArrayMapper<char?>()},
                {typeof(Nullable<decimal>[]),new ShallowCopyArrayMapper<decimal?>()},

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

            static readonly Dictionary<Type, (Type sameType, Type fromEnumerable)> collectionMapperMap = new Dictionary<Type, (Type, Type)>
            {
                { typeof(IEnumerable<>), (typeof(EnumerableMapper<,>), typeof(EnumerableMapper<,,>)) },
                { typeof(ArraySegment<>), (typeof(ArraySegmentMapper<,>), typeof(ArraySegmentMapper<,,>)) },
                { typeof(List<>), (typeof(ListMapper<,>), typeof(ListMapper<,,>)) },
                { typeof(LinkedList<>), (typeof(LinkedListMapper<,>), typeof(LinkedListMapper<,,>)) },
                { typeof(Queue<>), (typeof(QueueMapper<,>), typeof(QueueMapper<,,>)) },
                { typeof(Stack<>), (typeof(StackMapper<,>), typeof(StackMapper<,,>)) },
                { typeof(HashSet<>), (typeof(HashSetMapper<,>), typeof(HashSetMapper<,,>)) },
                { typeof(ReadOnlyCollection<>), (typeof(ReadOnlyCollectionMapper<,>), typeof(ReadOnlyCollectionMapper<,,>)) },
                { typeof(IList<>), (typeof(InterfaceListMapper<,>), typeof(InterfaceListMapper<,,>)) },
                { typeof(ICollection<>), (typeof(InterfaceCollectionMapper<,>), typeof(InterfaceCollectionMapper<,,>)) },
            };

            static readonly Dictionary<Type, object> parseMapperMap = new Dictionary<Type, object>
            {
                { typeof(sbyte), new SByteParseMapper() },
                { typeof(Int16), new Int16ParseMapper() },
                { typeof(Int32), new Int32ParseMapper() },
                { typeof(Int64), new Int64ParseMapper() },
                { typeof(byte), new ByteParseMapper() },
                { typeof(UInt16), new UInt16ParseMapper() },
                { typeof(UInt32), new UInt32ParseMapper() },
                { typeof(UInt64), new UInt64ParseMapper() },
                { typeof(Single), new SingleParseMapper() },
                { typeof(Double), new DoubleParseMapper() },
                { typeof(bool), new BooleanParseMapper() },
                { typeof(char), new CharParseMapper() },
                { typeof(decimal), new DecimalParseMapper() },
                { typeof(DateTime), new DateTimeParseMapper() },
                { typeof(DateTimeOffset), new DateTimeOffsetParseMapper() },
                { typeof(TimeSpan), new TimeSpanParseMapper() }
            };

            static readonly Dictionary<Type, object> nullableParseMapperMap = new Dictionary<Type, object>
            {
                { typeof(sbyte?), new NullableSByteParseMapper() },
                { typeof(Int16?), new NullableInt16ParseMapper() },
                { typeof(Int32?), new NullableInt32ParseMapper() },
                { typeof(Int64?), new NullableInt64ParseMapper() },
                { typeof(byte?),  new NullableByteParseMapper() },
                { typeof(UInt16?), new NullableUInt16ParseMapper() },
                { typeof(UInt32?), new NullableUInt32ParseMapper() },
                { typeof(UInt64?), new NullableUInt64ParseMapper() },
                { typeof(Single?), new NullableSingleParseMapper() },
                { typeof(Double?), new NullableDoubleParseMapper() },
                { typeof(bool?), new NullableBooleanParseMapper() },
                { typeof(char?), new NullableCharParseMapper() },
                { typeof(decimal?),  new NullableDecimalParseMapper() },
                { typeof(DateTime?), new NullableDateTimeParseMapper() },
                { typeof(DateTimeOffset?), new NullableDateTimeOffsetParseMapper() },
                { typeof(TimeSpan?), new NullableTimeSpanParseMapper() }
            };

            public static object CreateMapper(Type from, Type to)
            {
                object mapper = null;

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
                }

                // must be before TryCreateNullableMapper
                mapper = TryCreateParseMapper(from, to);
                if (mapper != null) return mapper;

                mapper = TryCreateNullableMapper(from, to);
                if (mapper != null) return mapper;

                mapper = TryCreateEnumMapper(from, to);
                if (mapper != null) return mapper;

                // must be after TryCreateEnumMapper and standard mappers.
                mapper = TryCreatePrimitiveToPrimitiveMapper(from, to);
                if (mapper != null) return mapper;

                mapper = TryCreateWellKnownGenericTypeMapper(from, to);
                if (mapper != null) return mapper;

                mapper = TryCreateTupleMapper(from, to);
                if (mapper != null) return mapper;

                mapper = TryCreateCollectionMapper(from, to);
                if (mapper != null) return mapper;

                mapper = TryCreateExceptionMapper(from, to);
                if (mapper != null) return mapper;

                // TODO:object mapper?

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

            static object TryCreatePrimitiveToPrimitiveMapper(Type from, Type to)
            {
                if (PrimitiveConvertMapper.IsPrimitive(from))
                {
                    if (PrimitiveConvertMapper.IsPrimitive(to))
                    {
                        return PrimitiveConvertMapper.Build(from, to);
                    }
                    else
                    {
                        throw new NotSupportedException("Primitive to X mapping does not supported.");
                    }
                }
                else if (PrimitiveConvertMapper.IsPrimitive(to))
                {
                    throw new NotSupportedException("X to Primitive mapping does not supported.");
                }

                return null;
            }

            static object TryCreateParseMapper(Type from, Type to)
            {
                if (from != typeof(string)) return null;

                if (parseMapperMap.TryGetValue(to, out var formatter))
                {
                    return formatter;
                }
                else if (nullableParseMapperMap.TryGetValue(to, out formatter))
                {
                    return formatter;
                }

                return null;
            }

            static object TryCreateWellKnownGenericTypeMapper(Type from, Type to)
            {
                // KVP<TKey,TValue>, Lazy<T>, Task<T>, ValueTask<T>
                // IGrouping, ILookup
                if (from.IsGenericType && to.IsGenericType)
                {
                    var fromGenericType = from.GetGenericTypeDefinition();
                    var toGenericType = to.GetGenericTypeDefinition();

                    if (fromGenericType == typeof(KeyValuePair<,>) && toGenericType == typeof(KeyValuePair<,>))
                    {
                        return Activator.CreateInstance(typeof(KeyValuePairMapper<,,,>).MakeGenericType(from.GenericTypeArguments.Concat(to.GenericTypeArguments).ToArray()));
                    }
                    else if (fromGenericType == typeof(Lazy<>) && toGenericType == typeof(Lazy<>))
                    {
                        return Activator.CreateInstance(typeof(LazyMapper<,>).MakeGenericType(from.GenericTypeArguments.Concat(to.GenericTypeArguments).ToArray()));
                    }
                    else if (fromGenericType == typeof(Task<>) && toGenericType == typeof(Task<>))
                    {
                        return Activator.CreateInstance(typeof(TaskValueMapper<,>).MakeGenericType(from.GenericTypeArguments.Concat(to.GenericTypeArguments).ToArray()));
                    }
                    else if (fromGenericType == typeof(ValueTask<>) && toGenericType == typeof(ValueTask<>))
                    {
                        return Activator.CreateInstance(typeof(ValueTaskMapper<,>).MakeGenericType(from.GenericTypeArguments.Concat(to.GenericTypeArguments).ToArray()));
                    }
                    else if (fromGenericType == typeof(IGrouping<,>) && toGenericType == typeof(IGrouping<,>))
                    {
                        return Activator.CreateInstance(typeof(InterfaceGroupingMapper<,,,>).MakeGenericType(from.GenericTypeArguments.Concat(to.GenericTypeArguments).ToArray()));
                    }
                    else if (fromGenericType == typeof(ILookup<,>) && toGenericType == typeof(ILookup<,>))
                    {
                        return Activator.CreateInstance(typeof(InterfaceLookupMapper<,,,>).MakeGenericType(from.GenericTypeArguments.Concat(to.GenericTypeArguments).ToArray()));
                    }
                }

                return null;
            }

            static object TryCreateTupleMapper(Type from, Type to)
            {
                // Tuple or ValueTuple
                if (from.FullName.StartsWith("System.Tuple", StringComparison.Ordinal))
                {
                    if (!to.FullName.StartsWith("System.Tuple", StringComparison.Ordinal)) throw new NotSupportedException($"Tuple to {to.Name} does not supported.");

                    var len = from.GenericTypeArguments.Length;
                    if (len != to.GenericTypeArguments.Length) throw new NotSupportedException("Different tuple items count does not supported.");

                    Type tupleMapperType = null;
                    switch (len)
                    {
                        case 1: tupleMapperType = typeof(TupleMapper<,>); break;
                        case 2: tupleMapperType = typeof(TupleMapper<,,,>); break;
                        case 3: tupleMapperType = typeof(TupleMapper<,,,,,>); break;
                        case 4: tupleMapperType = typeof(TupleMapper<,,,,,,,>); break;
                        case 5: tupleMapperType = typeof(TupleMapper<,,,,,,,,,>); break;
                        case 6: tupleMapperType = typeof(TupleMapper<,,,,,,,,,,,>); break;
                        case 7: tupleMapperType = typeof(TupleMapper<,,,,,,,,,,,,,>); break;
                        case 8: tupleMapperType = typeof(TupleMapper<,,,,,,,,,,,,,,,>); break;
                        default:
                            break;
                    }

                    return Activator.CreateInstance(tupleMapperType.MakeGenericType(from.GenericTypeArguments.Concat(to.GenericTypeArguments).ToArray()));
                }
                else if (from.FullName.StartsWith("System.ValueTuple", StringComparison.Ordinal))
                {
                    if (!to.FullName.StartsWith("System.ValueTuple", StringComparison.Ordinal)) throw new NotSupportedException($"ValueTuple to {to.Name} does not supported.");

                    var len = from.GenericTypeArguments.Length;
                    if (len != to.GenericTypeArguments.Length) throw new NotSupportedException("Different tuple items count does not supported.");

                    Type tupleMapperType = null;
                    switch (len)
                    {
                        case 1: tupleMapperType = typeof(ValueTupleMapper<,>); break;
                        case 2: tupleMapperType = typeof(ValueTupleMapper<,,,>); break;
                        case 3: tupleMapperType = typeof(ValueTupleMapper<,,,,,>); break;
                        case 4: tupleMapperType = typeof(ValueTupleMapper<,,,,,,,>); break;
                        case 5: tupleMapperType = typeof(ValueTupleMapper<,,,,,,,,,>); break;
                        case 6: tupleMapperType = typeof(ValueTupleMapper<,,,,,,,,,,,>); break;
                        case 7: tupleMapperType = typeof(ValueTupleMapper<,,,,,,,,,,,,,>); break;
                        case 8: tupleMapperType = typeof(ValueTupleMapper<,,,,,,,,,,,,,,,>); break;
                        default:
                            break;
                    }

                    return Activator.CreateInstance(tupleMapperType.MakeGenericType(from.GenericTypeArguments.Concat(to.GenericTypeArguments).ToArray()));
                }

                return null;
            }

            static object TryCreateCollectionMapper(Type from, Type to)
            {
                if (from.IsArray && to.IsArray)
                {
                    var rank = from.GetArrayRank();
                    if (rank != to.GetArrayRank())
                    {
                        throw new NotSupportedException("Diffrent array rank mapping does not supported.");
                    }

                    if (rank == 1)
                    {
                        return Activator.CreateInstance(typeof(ArrayMapper<,>).MakeGenericType(from.GetElementType(), to.GetElementType()));
                    }
                    else if (rank == 2)
                    {
                        return Activator.CreateInstance(typeof(TwoDimentionalArrayMapper<,>).MakeGenericType(from.GetElementType(), to.GetElementType()));
                    }
                    else if (rank == 3)
                    {
                        return Activator.CreateInstance(typeof(ThreeDimentionalArrayMapper<,>).MakeGenericType(from.GetElementType(), to.GetElementType()));
                    }
                    else if (rank == 4)
                    {
                        return Activator.CreateInstance(typeof(FourDimentionalArrayMapper<,>).MakeGenericType(from.GetElementType(), to.GetElementType()));
                    }
                    else
                    {
                        throw new NotSupportedException("Rank > 4 multi dimentional array does not supported.");
                    }
                }
                else if (to.IsArray)
                {
                    var fromElement = CollectionHelper.GetEnumerableElement(from);
                    if (fromElement != null)
                    {
                        return Activator.CreateInstance(typeof(ArrayMapper<,,>).MakeGenericType(from, fromElement, to.GetElementType()));
                    }

                    return null;
                }

                if (to.IsGenericType)
                {
                    if (!from.IsArray && !from.IsGenericType) throw new NotSupportedException("NonGenericCollection to GenericCollection does not supported.");

                    // generic collection
                    var fromDef = (from.IsArray) ? null : from.GetGenericTypeDefinition();
                    var toDef = to.GetGenericTypeDefinition();
                    var fromElement = CollectionHelper.GetEnumerableElement(from);
                    var toElement = CollectionHelper.GetEnumerableElement(to);

                    // special for List<T>
                    if (toDef == typeof(List<>) && fromElement != null && fromElement.IsPrimitive && fromElement == toElement)
                    {
                        return Activator.CreateInstance(typeof(ShallowCopyListMapper<,>).MakeGenericType(from, fromElement));
                    }

                    if (collectionMapperMap.TryGetValue(toDef, out var mapperType))
                    {
                        if (fromDef == toDef)
                        {
                            return Activator.CreateInstance(mapperType.sameType.MakeGenericType(fromElement, toElement));
                        }
                        else
                        {
                            return Activator.CreateInstance(mapperType.fromEnumerable.MakeGenericType(from, fromElement, toElement));
                        }
                    }
                }
                else
                {
                    // nongeneric collection?
                }

                // // TODO:GenericCollectionMapper


                return null;
            }

            static object TryCreateExceptionMapper(Type from, Type to)
            {
                if (typeof(Exception).IsAssignableFrom(from) && typeof(Exception).IsAssignableFrom(to))
                {
                    // Exception is maybe immutable(not guranteed)
                    if (to.IsAssignableFrom(from))
                    {
                        return Activator.CreateInstance(typeof(ReturnSelfMapper<,>).MakeGenericType(from, to));
                    }
                }

                return null;
            }
        }
    }
}