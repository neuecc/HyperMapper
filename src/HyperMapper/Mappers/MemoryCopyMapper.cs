using System;
using HyperMapper;
using System.Runtime.CompilerServices;

namespace HyperMapper.Mappers
{
    public sealed unsafe class SByteMemoryCopyMapper : IObjectMapper<SByte[], SByte[]>
    {
        const int ElementMemorySize = sizeof(SByte);

        public SByte[] Map(SByte[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new SByte[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte[] Map(SByte[] from)
        {
            if (from == null) return null;

            var newArray = new SByte[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class Int16MemoryCopyMapper : IObjectMapper<Int16[], Int16[]>
    {
        const int ElementMemorySize = sizeof(Int16);

        public Int16[] Map(Int16[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new Int16[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16[] Map(Int16[] from)
        {
            if (from == null) return null;

            var newArray = new Int16[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class Int32MemoryCopyMapper : IObjectMapper<Int32[], Int32[]>
    {
        const int ElementMemorySize = sizeof(Int32);

        public Int32[] Map(Int32[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new Int32[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32[] Map(Int32[] from)
        {
            if (from == null) return null;

            var newArray = new Int32[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class Int64MemoryCopyMapper : IObjectMapper<Int64[], Int64[]>
    {
        const int ElementMemorySize = sizeof(Int64);

        public Int64[] Map(Int64[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new Int64[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64[] Map(Int64[] from)
        {
            if (from == null) return null;

            var newArray = new Int64[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class ByteMemoryCopyMapper : IObjectMapper<Byte[], Byte[]>
    {
        const int ElementMemorySize = sizeof(Byte);

        public Byte[] Map(Byte[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new Byte[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] Map(Byte[] from)
        {
            if (from == null) return null;

            var newArray = new Byte[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class UInt16MemoryCopyMapper : IObjectMapper<UInt16[], UInt16[]>
    {
        const int ElementMemorySize = sizeof(UInt16);

        public UInt16[] Map(UInt16[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new UInt16[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16[] Map(UInt16[] from)
        {
            if (from == null) return null;

            var newArray = new UInt16[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class UInt32MemoryCopyMapper : IObjectMapper<UInt32[], UInt32[]>
    {
        const int ElementMemorySize = sizeof(UInt32);

        public UInt32[] Map(UInt32[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new UInt32[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32[] Map(UInt32[] from)
        {
            if (from == null) return null;

            var newArray = new UInt32[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class UInt64MemoryCopyMapper : IObjectMapper<UInt64[], UInt64[]>
    {
        const int ElementMemorySize = sizeof(UInt64);

        public UInt64[] Map(UInt64[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new UInt64[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64[] Map(UInt64[] from)
        {
            if (from == null) return null;

            var newArray = new UInt64[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class SingleMemoryCopyMapper : IObjectMapper<Single[], Single[]>
    {
        const int ElementMemorySize = sizeof(Single);

        public Single[] Map(Single[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new Single[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single[] Map(Single[] from)
        {
            if (from == null) return null;

            var newArray = new Single[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class DoubleMemoryCopyMapper : IObjectMapper<Double[], Double[]>
    {
        const int ElementMemorySize = sizeof(Double);

        public Double[] Map(Double[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new Double[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double[] Map(Double[] from)
        {
            if (from == null) return null;

            var newArray = new Double[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class BooleanMemoryCopyMapper : IObjectMapper<Boolean[], Boolean[]>
    {
        const int ElementMemorySize = sizeof(Boolean);

        public Boolean[] Map(Boolean[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new Boolean[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean[] Map(Boolean[] from)
        {
            if (from == null) return null;

            var newArray = new Boolean[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class CharMemoryCopyMapper : IObjectMapper<Char[], Char[]>
    {
        const int ElementMemorySize = sizeof(Char);

        public Char[] Map(Char[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new Char[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char[] Map(Char[] from)
        {
            if (from == null) return null;

            var newArray = new Char[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

    public sealed unsafe class DecimalMemoryCopyMapper : IObjectMapper<Decimal[], Decimal[]>
    {
        const int ElementMemorySize = sizeof(Decimal);

        public Decimal[] Map(Decimal[] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var newArray = new Decimal[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal[] Map(Decimal[] from)
        {
            if (from == null) return null;

            var newArray = new Decimal[from.Length];
            ulong len = (ulong)from.Length * ElementMemorySize;
            fixed (void* src = from)
            fixed (void* dest = newArray)
            {
                Buffer.MemoryCopy(src, dest, len, len);
            }

            return newArray;
        }
    }

}