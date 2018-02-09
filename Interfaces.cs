
namespace HyperMapper
{
    public interface IObjectMapperResolver
    {
        IObjectMapper<TFrom, TTo> GetMapper<TFrom, TTo>();
    }

    public interface IObjectMapper<TFrom, TTo>
    {
        TTo Map(TFrom from, IObjectMapperResolver resolver);
    }
}
