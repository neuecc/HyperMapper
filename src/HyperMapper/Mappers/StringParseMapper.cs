using System;
using HyperMapper;
using System.Globalization;

namespace HyperMapper.Mappers
{
    public sealed class SByteParseMapper : IObjectMapper<string, SByte>
    {
        public SByte Map(string from, IObjectMapperResolver resolver)
        {
            return SByte.Parse(from);
        }
    }

    public sealed class Int16ParseMapper : IObjectMapper<string, Int16>
    {
        public Int16 Map(string from, IObjectMapperResolver resolver)
        {
            return Int16.Parse(from);
        }
    }

    public sealed class Int32ParseMapper : IObjectMapper<string, Int32>
    {
        public Int32 Map(string from, IObjectMapperResolver resolver)
        {
            return Int32.Parse(from);
        }
    }

    public sealed class Int64ParseMapper : IObjectMapper<string, Int64>
    {
        public Int64 Map(string from, IObjectMapperResolver resolver)
        {
            return Int64.Parse(from);
        }
    }

    public sealed class ByteParseMapper : IObjectMapper<string, Byte>
    {
        public Byte Map(string from, IObjectMapperResolver resolver)
        {
            return Byte.Parse(from);
        }
    }

    public sealed class UInt16ParseMapper : IObjectMapper<string, UInt16>
    {
        public UInt16 Map(string from, IObjectMapperResolver resolver)
        {
            return UInt16.Parse(from);
        }
    }

    public sealed class UInt32ParseMapper : IObjectMapper<string, UInt32>
    {
        public UInt32 Map(string from, IObjectMapperResolver resolver)
        {
            return UInt32.Parse(from);
        }
    }

    public sealed class UInt64ParseMapper : IObjectMapper<string, UInt64>
    {
        public UInt64 Map(string from, IObjectMapperResolver resolver)
        {
            return UInt64.Parse(from);
        }
    }

    public sealed class SingleParseMapper : IObjectMapper<string, Single>
    {
        public Single Map(string from, IObjectMapperResolver resolver)
        {
            return Single.Parse(from);
        }
    }

    public sealed class DoubleParseMapper : IObjectMapper<string, Double>
    {
        public Double Map(string from, IObjectMapperResolver resolver)
        {
            return Double.Parse(from);
        }
    }

    public sealed class BooleanParseMapper : IObjectMapper<string, Boolean>
    {
        public Boolean Map(string from, IObjectMapperResolver resolver)
        {
            return Boolean.Parse(from);
        }
    }

    public sealed class CharParseMapper : IObjectMapper<string, Char>
    {
        public Char Map(string from, IObjectMapperResolver resolver)
        {
            return Char.Parse(from);
        }
    }

    public sealed class DecimalParseMapper : IObjectMapper<string, Decimal>
    {
        public Decimal Map(string from, IObjectMapperResolver resolver)
        {
            return Decimal.Parse(from);
        }
    }

    public sealed class DateTimeParseMapper : IObjectMapper<string, DateTime>
    {
        public DateTime Map(string from, IObjectMapperResolver resolver)
        {
            return DateTime.Parse(from);
        }
    }

    public sealed class DateTimeOffsetParseMapper : IObjectMapper<string, DateTimeOffset>
    {
        public DateTimeOffset Map(string from, IObjectMapperResolver resolver)
        {
            return DateTimeOffset.Parse(from);
        }
    }

    public sealed class TimeSpanParseMapper : IObjectMapper<string, TimeSpan>
    {
        public TimeSpan Map(string from, IObjectMapperResolver resolver)
        {
            return TimeSpan.Parse(from);
        }
    }


    public sealed class NullableSByteParseMapper : IObjectMapper<string, SByte?>
    {
        public SByte? Map(string from, IObjectMapperResolver resolver)
        {
            return SByte.TryParse(from, out var value)
                ? value
                : default(SByte?);
        }
    }

    public sealed class NullableInt16ParseMapper : IObjectMapper<string, Int16?>
    {
        public Int16? Map(string from, IObjectMapperResolver resolver)
        {
            return Int16.TryParse(from, out var value)
                ? value
                : default(Int16?);
        }
    }

    public sealed class NullableInt32ParseMapper : IObjectMapper<string, Int32?>
    {
        public Int32? Map(string from, IObjectMapperResolver resolver)
        {
            return Int32.TryParse(from, out var value)
                ? value
                : default(Int32?);
        }
    }

    public sealed class NullableInt64ParseMapper : IObjectMapper<string, Int64?>
    {
        public Int64? Map(string from, IObjectMapperResolver resolver)
        {
            return Int64.TryParse(from, out var value)
                ? value
                : default(Int64?);
        }
    }

    public sealed class NullableByteParseMapper : IObjectMapper<string, Byte?>
    {
        public Byte? Map(string from, IObjectMapperResolver resolver)
        {
            return Byte.TryParse(from, out var value)
                ? value
                : default(Byte?);
        }
    }

    public sealed class NullableUInt16ParseMapper : IObjectMapper<string, UInt16?>
    {
        public UInt16? Map(string from, IObjectMapperResolver resolver)
        {
            return UInt16.TryParse(from, out var value)
                ? value
                : default(UInt16?);
        }
    }

    public sealed class NullableUInt32ParseMapper : IObjectMapper<string, UInt32?>
    {
        public UInt32? Map(string from, IObjectMapperResolver resolver)
        {
            return UInt32.TryParse(from, out var value)
                ? value
                : default(UInt32?);
        }
    }

    public sealed class NullableUInt64ParseMapper : IObjectMapper<string, UInt64?>
    {
        public UInt64? Map(string from, IObjectMapperResolver resolver)
        {
            return UInt64.TryParse(from, out var value)
                ? value
                : default(UInt64?);
        }
    }

    public sealed class NullableSingleParseMapper : IObjectMapper<string, Single?>
    {
        public Single? Map(string from, IObjectMapperResolver resolver)
        {
            return Single.TryParse(from, out var value)
                ? value
                : default(Single?);
        }
    }

    public sealed class NullableDoubleParseMapper : IObjectMapper<string, Double?>
    {
        public Double? Map(string from, IObjectMapperResolver resolver)
        {
            return Double.TryParse(from, out var value)
                ? value
                : default(Double?);
        }
    }

    public sealed class NullableBooleanParseMapper : IObjectMapper<string, Boolean?>
    {
        public Boolean? Map(string from, IObjectMapperResolver resolver)
        {
            return Boolean.TryParse(from, out var value)
                ? value
                : default(Boolean?);
        }
    }

    public sealed class NullableCharParseMapper : IObjectMapper<string, Char?>
    {
        public Char? Map(string from, IObjectMapperResolver resolver)
        {
            return Char.TryParse(from, out var value)
                ? value
                : default(Char?);
        }
    }

    public sealed class NullableDecimalParseMapper : IObjectMapper<string, Decimal?>
    {
        public Decimal? Map(string from, IObjectMapperResolver resolver)
        {
            return Decimal.TryParse(from, out var value)
                ? value
                : default(Decimal?);
        }
    }

    public sealed class NullableDateTimeParseMapper : IObjectMapper<string, DateTime?>
    {
        public DateTime? Map(string from, IObjectMapperResolver resolver)
        {
            return DateTime.TryParse(from, out var value)
                ? value
                : default(DateTime?);
        }
    }

    public sealed class NullableDateTimeOffsetParseMapper : IObjectMapper<string, DateTimeOffset?>
    {
        public DateTimeOffset? Map(string from, IObjectMapperResolver resolver)
        {
            return DateTimeOffset.TryParse(from, out var value)
                ? value
                : default(DateTimeOffset?);
        }
    }

    public sealed class NullableTimeSpanParseMapper : IObjectMapper<string, TimeSpan?>
    {
        public TimeSpan? Map(string from, IObjectMapperResolver resolver)
        {
            return TimeSpan.TryParse(from, out var value)
                ? value
                : default(TimeSpan?);
        }
    }

}