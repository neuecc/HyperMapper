
using System;
using HyperMapper.Internal;

namespace HyperMapper.Mappers
{
    public sealed class TupleMapper<T1, U1> : IObjectMapper<Tuple<T1>, Tuple<U1>>
    {
        public Tuple<U1> Map(Tuple<T1> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<U1>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver));
        }
    }

    public sealed class TupleMapper<T1, T2, U1, U2> : IObjectMapper<Tuple<T1, T2>, Tuple<U1, U2>>
    {
        public Tuple<U1, U2> Map(Tuple<T1, T2> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<U1, U2>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver));
        }
    }

    public sealed class TupleMapper<T1, T2, T3, U1, U2, U3> : IObjectMapper<Tuple<T1, T2, T3>, Tuple<U1, U2, U3>>
    {
        public Tuple<U1, U2, U3> Map(Tuple<T1, T2, T3> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<U1, U2, U3>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver));
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4, U1, U2, U3, U4> : IObjectMapper<Tuple<T1, T2, T3, T4>, Tuple<U1, U2, U3, U4>>
    {
        public Tuple<U1, U2, U3, U4> Map(Tuple<T1, T2, T3, T4> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<U1, U2, U3, U4>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver));
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4, T5, U1, U2, U3, U4, U5> : IObjectMapper<Tuple<T1, T2, T3, T4, T5>, Tuple<U1, U2, U3, U4, U5>>
    {
        public Tuple<U1, U2, U3, U4, U5> Map(Tuple<T1, T2, T3, T4, T5> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<U1, U2, U3, U4, U5>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver),
                resolver.GetMapperWithVerify<T5, U5>().Map(from.Item5, resolver));
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4, T5, T6, U1, U2, U3, U4, U5, U6> : IObjectMapper<Tuple<T1, T2, T3, T4, T5, T6>, Tuple<U1, U2, U3, U4, U5, U6>>
    {
        public Tuple<U1, U2, U3, U4, U5, U6> Map(Tuple<T1, T2, T3, T4, T5, T6> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<U1, U2, U3, U4, U5, U6>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver),
                resolver.GetMapperWithVerify<T5, U5>().Map(from.Item5, resolver),
                resolver.GetMapperWithVerify<T6, U6>().Map(from.Item6, resolver));
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4, T5, T6, T7, U1, U2, U3, U4, U5, U6, U7> : IObjectMapper<Tuple<T1, T2, T3, T4, T5, T6, T7>, Tuple<U1, U2, U3, U4, U5, U6, U7>>
    {
        public Tuple<U1, U2, U3, U4, U5, U6, U7> Map(Tuple<T1, T2, T3, T4, T5, T6, T7> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<U1, U2, U3, U4, U5, U6, U7>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver),
                resolver.GetMapperWithVerify<T5, U5>().Map(from.Item5, resolver),
                resolver.GetMapperWithVerify<T6, U6>().Map(from.Item6, resolver),
                resolver.GetMapperWithVerify<T7, U7>().Map(from.Item7, resolver));
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4, T5, T6, T7, TRest, U1, U2, U3, U4, U5, U6, U7, URest> : IObjectMapper<Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>, Tuple<U1, U2, U3, U4, U5, U6, U7, URest>>
    {
        public Tuple<U1, U2, U3, U4, U5, U6, U7, URest> Map(Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<U1, U2, U3, U4, U5, U6, U7, URest>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver),
                resolver.GetMapperWithVerify<T5, U5>().Map(from.Item5, resolver),
                resolver.GetMapperWithVerify<T6, U6>().Map(from.Item6, resolver),
                resolver.GetMapperWithVerify<T7, U7>().Map(from.Item7, resolver),
                resolver.GetMapperWithVerify<TRest, URest>().Map(from.Rest, resolver));
        }
    }


    public sealed class ValueTupleMapper<T1, U1> : IObjectMapper<ValueTuple<T1>, ValueTuple<U1>>
    {
        public ValueTuple<U1> Map(ValueTuple<T1> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<U1>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver));
        }
    }

    public sealed class ValueTupleMapper<T1, T2, U1, U2> : IObjectMapper<ValueTuple<T1, T2>, ValueTuple<U1, U2>>
    {
        public ValueTuple<U1, U2> Map(ValueTuple<T1, T2> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<U1, U2>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver));
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, U1, U2, U3> : IObjectMapper<ValueTuple<T1, T2, T3>, ValueTuple<U1, U2, U3>>
    {
        public ValueTuple<U1, U2, U3> Map(ValueTuple<T1, T2, T3> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<U1, U2, U3>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver));
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4, U1, U2, U3, U4> : IObjectMapper<ValueTuple<T1, T2, T3, T4>, ValueTuple<U1, U2, U3, U4>>
    {
        public ValueTuple<U1, U2, U3, U4> Map(ValueTuple<T1, T2, T3, T4> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<U1, U2, U3, U4>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver));
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4, T5, U1, U2, U3, U4, U5> : IObjectMapper<ValueTuple<T1, T2, T3, T4, T5>, ValueTuple<U1, U2, U3, U4, U5>>
    {
        public ValueTuple<U1, U2, U3, U4, U5> Map(ValueTuple<T1, T2, T3, T4, T5> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<U1, U2, U3, U4, U5>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver),
                resolver.GetMapperWithVerify<T5, U5>().Map(from.Item5, resolver));
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4, T5, T6, U1, U2, U3, U4, U5, U6> : IObjectMapper<ValueTuple<T1, T2, T3, T4, T5, T6>, ValueTuple<U1, U2, U3, U4, U5, U6>>
    {
        public ValueTuple<U1, U2, U3, U4, U5, U6> Map(ValueTuple<T1, T2, T3, T4, T5, T6> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<U1, U2, U3, U4, U5, U6>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver),
                resolver.GetMapperWithVerify<T5, U5>().Map(from.Item5, resolver),
                resolver.GetMapperWithVerify<T6, U6>().Map(from.Item6, resolver));
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4, T5, T6, T7, U1, U2, U3, U4, U5, U6, U7> : IObjectMapper<ValueTuple<T1, T2, T3, T4, T5, T6, T7>, ValueTuple<U1, U2, U3, U4, U5, U6, U7>>
    {
        public ValueTuple<U1, U2, U3, U4, U5, U6, U7> Map(ValueTuple<T1, T2, T3, T4, T5, T6, T7> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<U1, U2, U3, U4, U5, U6, U7>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver),
                resolver.GetMapperWithVerify<T5, U5>().Map(from.Item5, resolver),
                resolver.GetMapperWithVerify<T6, U6>().Map(from.Item6, resolver),
                resolver.GetMapperWithVerify<T7, U7>().Map(from.Item7, resolver));
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4, T5, T6, T7, TRest, U1, U2, U3, U4, U5, U6, U7, URest> : IObjectMapper<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>, ValueTuple<U1, U2, U3, U4, U5, U6, U7, URest>> where TRest : struct where URest : struct
    {
        public ValueTuple<U1, U2, U3, U4, U5, U6, U7, URest> Map(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<U1, U2, U3, U4, U5, U6, U7, URest>(
                resolver.GetMapperWithVerify<T1, U1>().Map(from.Item1, resolver),
                resolver.GetMapperWithVerify<T2, U2>().Map(from.Item2, resolver),
                resolver.GetMapperWithVerify<T3, U3>().Map(from.Item3, resolver),
                resolver.GetMapperWithVerify<T4, U4>().Map(from.Item4, resolver),
                resolver.GetMapperWithVerify<T5, U5>().Map(from.Item5, resolver),
                resolver.GetMapperWithVerify<T6, U6>().Map(from.Item6, resolver),
                resolver.GetMapperWithVerify<T7, U7>().Map(from.Item7, resolver),
                resolver.GetMapperWithVerify<TRest, URest>().Map(from.Rest, resolver));
        }
    }

}