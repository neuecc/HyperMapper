
using System;
using HyperMapper.Internal;

namespace HyperMapper.Mappers
{
    public sealed class TupleMapper<T1> : IObjectMapper<Tuple<T1>, Tuple<T1>>
    {
        public Tuple<T1> Map(Tuple<T1> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<T1>(from.Item1);
        }
    }

    public sealed class TupleMapper<T1, T2> : IObjectMapper<Tuple<T1, T2>, Tuple<T1, T2>>
    {
        public Tuple<T1, T2> Map(Tuple<T1, T2> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<T1, T2>(from.Item1, from.Item2);
        }
    }

    public sealed class TupleMapper<T1, T2, T3> : IObjectMapper<Tuple<T1, T2, T3>, Tuple<T1, T2, T3>>
    {
        public Tuple<T1, T2, T3> Map(Tuple<T1, T2, T3> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<T1, T2, T3>(from.Item1, from.Item2, from.Item3);
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4> : IObjectMapper<Tuple<T1, T2, T3, T4>, Tuple<T1, T2, T3, T4>>
    {
        public Tuple<T1, T2, T3, T4> Map(Tuple<T1, T2, T3, T4> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<T1, T2, T3, T4>(from.Item1, from.Item2, from.Item3, from.Item4);
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4, T5> : IObjectMapper<Tuple<T1, T2, T3, T4, T5>, Tuple<T1, T2, T3, T4, T5>>
    {
        public Tuple<T1, T2, T3, T4, T5> Map(Tuple<T1, T2, T3, T4, T5> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<T1, T2, T3, T4, T5>(from.Item1, from.Item2, from.Item3, from.Item4, from.Item5);
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4, T5, T6> : IObjectMapper<Tuple<T1, T2, T3, T4, T5, T6>, Tuple<T1, T2, T3, T4, T5, T6>>
    {
        public Tuple<T1, T2, T3, T4, T5, T6> Map(Tuple<T1, T2, T3, T4, T5, T6> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<T1, T2, T3, T4, T5, T6>(from.Item1, from.Item2, from.Item3, from.Item4, from.Item5, from.Item6);
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4, T5, T6, T7> : IObjectMapper<Tuple<T1, T2, T3, T4, T5, T6, T7>, Tuple<T1, T2, T3, T4, T5, T6, T7>>
    {
        public Tuple<T1, T2, T3, T4, T5, T6, T7> Map(Tuple<T1, T2, T3, T4, T5, T6, T7> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<T1, T2, T3, T4, T5, T6, T7>(from.Item1, from.Item2, from.Item3, from.Item4, from.Item5, from.Item6, from.Item7);
        }
    }

    public sealed class TupleMapper<T1, T2, T3, T4, T5, T6, T7, TRest> : IObjectMapper<Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>, Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>>
    {
        public Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> Map(Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(from.Item1, from.Item2, from.Item3, from.Item4, from.Item5, from.Item6, from.Item7, from.Rest);
        }
    }


    public sealed class ValueTupleMapper<T1> : IObjectMapper<ValueTuple<T1>, ValueTuple<T1>>
    {
        public ValueTuple<T1> Map(ValueTuple<T1> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<T1>(from.Item1);
        }
    }

    public sealed class ValueTupleMapper<T1, T2> : IObjectMapper<ValueTuple<T1, T2>, ValueTuple<T1, T2>>
    {
        public ValueTuple<T1, T2> Map(ValueTuple<T1, T2> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<T1, T2>(from.Item1, from.Item2);
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3> : IObjectMapper<ValueTuple<T1, T2, T3>, ValueTuple<T1, T2, T3>>
    {
        public ValueTuple<T1, T2, T3> Map(ValueTuple<T1, T2, T3> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<T1, T2, T3>(from.Item1, from.Item2, from.Item3);
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4> : IObjectMapper<ValueTuple<T1, T2, T3, T4>, ValueTuple<T1, T2, T3, T4>>
    {
        public ValueTuple<T1, T2, T3, T4> Map(ValueTuple<T1, T2, T3, T4> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<T1, T2, T3, T4>(from.Item1, from.Item2, from.Item3, from.Item4);
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4, T5> : IObjectMapper<ValueTuple<T1, T2, T3, T4, T5>, ValueTuple<T1, T2, T3, T4, T5>>
    {
        public ValueTuple<T1, T2, T3, T4, T5> Map(ValueTuple<T1, T2, T3, T4, T5> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<T1, T2, T3, T4, T5>(from.Item1, from.Item2, from.Item3, from.Item4, from.Item5);
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4, T5, T6> : IObjectMapper<ValueTuple<T1, T2, T3, T4, T5, T6>, ValueTuple<T1, T2, T3, T4, T5, T6>>
    {
        public ValueTuple<T1, T2, T3, T4, T5, T6> Map(ValueTuple<T1, T2, T3, T4, T5, T6> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6>(from.Item1, from.Item2, from.Item3, from.Item4, from.Item5, from.Item6);
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4, T5, T6, T7> : IObjectMapper<ValueTuple<T1, T2, T3, T4, T5, T6, T7>, ValueTuple<T1, T2, T3, T4, T5, T6, T7>>
    {
        public ValueTuple<T1, T2, T3, T4, T5, T6, T7> Map(ValueTuple<T1, T2, T3, T4, T5, T6, T7> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(from.Item1, from.Item2, from.Item3, from.Item4, from.Item5, from.Item6, from.Item7);
        }
    }

    public sealed class ValueTupleMapper<T1, T2, T3, T4, T5, T6, T7, TRest> : IObjectMapper<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>, ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>> where TRest : struct
    {
        public ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> Map(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> from, IObjectMapperResolver resolver)
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(from.Item1, from.Item2, from.Item3, from.Item4, from.Item5, from.Item6, from.Item7, from.Rest);
        }
    }

}