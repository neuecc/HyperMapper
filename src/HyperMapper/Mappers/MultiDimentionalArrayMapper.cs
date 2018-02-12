
namespace HyperMapper.Mappers
{
    public class TwoDimentionalArrayMapper<TFrom, TTo> : IObjectMapper<TFrom[,], TTo[,]>
    {
        public TTo[,] Map(TFrom[,] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var mapper = resolver.GetMapperWithVerify<TFrom, TTo>();

            var iLen = from.GetLength(0);
            var jLen = from.GetLength(1);

            var to = new TTo[iLen, jLen];

            for (int i = 0; i < iLen; i++)
            {
                for (int j = 0; j < jLen; j++)
                {
                    to[i, j] = mapper.Map(from[i, j], resolver);
                }
            }

            return to;
        }
    }

    public class ThreeDimentionalArrayMapper<TFrom, TTo> : IObjectMapper<TFrom[,,], TTo[,,]>
    {
        public TTo[,,] Map(TFrom[,,] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var mapper = resolver.GetMapperWithVerify<TFrom, TTo>();

            var iLen = from.GetLength(0);
            var jLen = from.GetLength(1);
            var kLen = from.GetLength(2);

            var to = new TTo[iLen, jLen, kLen];

            for (int i = 0; i < iLen; i++)
            {
                for (int j = 0; j < jLen; j++)
                {
                    for (int k = 0; k < kLen; k++)
                    {
                        to[i, j, k] = mapper.Map(from[i, j, k], resolver);
                    }
                }
            }

            return to;
        }
    }

    public class FourDimentionalArrayMapper<TFrom, TTo> : IObjectMapper<TFrom[,,,], TTo[,,,]>
    {
        public TTo[,,,] Map(TFrom[,,,] from, IObjectMapperResolver resolver)
        {
            if (from == null) return null;

            var mapper = resolver.GetMapperWithVerify<TFrom, TTo>();

            var iLen = from.GetLength(0);
            var jLen = from.GetLength(1);
            var kLen = from.GetLength(2);
            var lLen = from.GetLength(3);

            var to = new TTo[iLen, jLen, kLen, lLen];

            for (int i = 0; i < iLen; i++)
            {
                for (int j = 0; j < jLen; j++)
                {
                    for (int k = 0; k < kLen; k++)
                    {
                        for (int l = 0; l < lLen; l++)
                        {
                            to[i, j, k, l] = mapper.Map(from[i, j, k, l], resolver);
                        }
                    }
                }
            }

            return to;
        }
    }
}
