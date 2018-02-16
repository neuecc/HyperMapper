using System;

namespace HyperMapper.Internal
{
    internal static class FuncExtensions
    {
        // hack of avoid closure allocation(() => value).
        public static Func<T> AsFunc<T>(this T value)
        {
            return new Func<T>(value.ReturnBox<T>);
        }

        public static Func<TIgnore, T> AsFunc2<TIgnore, T>(this T value)
        {
            return new Func<TIgnore, T>(value.ReturnBox2<TIgnore, T>);
        }

        public static Func<T> AsFuncFast<T>(this T value) where T : class
        {
            return new Func<T>(value.Return<T>);
        }

        static T Return<T>(this T value)
        {
            return value;
        }

        static T ReturnBox<T>(this object value)
        {
            return (T)value;
        }

        static T ReturnBox2<TIgnore, T>(this object value, TIgnore _)
        {
            return (T)value;
        }
    }
}