namespace HyperMapper.Mappers
{
    /// <summary>
    /// Return self for struct or immutable object.
    /// </summary>
    public sealed class ReturnSelfMapper<T> : IObjectMapper<T, T>
    {
        public T Map(T obj, IObjectMapperResolver resolver)
        {
            return obj; // return self(if struct, copied).
        }
    }

    /// <summary>
    /// Return self for immutable object and TTo : TFrom cast.
    /// </summary>
    public sealed class ReturnSelfMapper<TFrom, TTo> : IObjectMapper<TFrom, TTo>
        where TTo : TFrom
    {
        public TTo Map(TFrom obj, IObjectMapperResolver resolver)
        {
            return (TTo)obj; // return self(cast).
        }
    }
}