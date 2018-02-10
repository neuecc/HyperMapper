using System;

namespace HyperMapper.Internal
{
    // some mappers use this so accessibility have to be public.
    public struct ArrayBuffer<T>
    {
        public T[] Buffer;
        public int Size;

        public ArrayBuffer(int initialSize)
        {
            this.Buffer = new T[initialSize];
            this.Size = 0;
        }

        public void Add(T value)
        {
            if (Size >= this.Buffer.Length)
            {
                Array.Resize(ref Buffer, Size * 2);
            }

            Buffer[Size++] = value;
        }

        public T[] ToArray()
        {
            if (Buffer.Length == Size)
            {
                return Buffer;
            }

            var result = new T[Size];
            Array.Copy(Buffer, result, Size);
            return result;
        }
    }
}