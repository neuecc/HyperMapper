using AutoMapper;
using Mapster;
using Benchmark.Flattening;
using BenchmarkDotNet.Attributes;
using HyperMapper.Resolvers;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfBenchmark
{
    [Config(typeof(BenchmarkConfig))]
    public class MapBenchmark
    {
        static Foo _foo;

        static int xxx;

        static int[] testArray = new[] { 1, 10, 100, 1000, 2000, 99999 };
        // static int[] testArray = Enumerable.Range(1, 1000).ToArray();
        static HyperMapper.IObjectMapper<int[], int[]> blockcopyMapper = new HyperMapper.Mappers.BlockCopyMapper<int>(sizeof(int));
        static HyperMapper.IObjectMapper<int[], int[]> arraycopyMapper = new HyperMapper.Mappers.ShallowCopyArrayMapper<int>();
        static HyperMapper.IObjectMapper<int[], int[]> memorycopyMapper = new HyperMapper.Mappers.Int32MemoryCopyMapper();

        static MapBenchmark()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Foo, FooDest>();
                cfg.CreateMap<InnerFoo, InnerFooDest>();
                cfg.CreateMap<Foo, Foo>();
                cfg.CreateMap<InnerFoo, InnerFoo>();
            });
            //config.AssertConfigurationIsValid();

            // config.CreateMapper();
            _foo = Foo.New();

            xxx = int.Parse("100");

            //global::Nelibur.ObjectMapper.TinyMapper.Bind<Foo, FooDest>();
            //global::Nelibur.ObjectMapper.TinyMapper.Bind<Foo, Foo>();
            //global::Nelibur.ObjectMapper.TinyMapper.Bind<InnerFoo, InnerFoo>();
        }



        //[Benchmark]
        //public int[] MemoryCopy()
        //{
        //    return memorycopyMapper.Map(testArray, null);
        //}

        //[Benchmark]
        //public int[] BlockCopy()
        //{
        //    return blockcopyMapper.Map(testArray, null);
        //}

        //[Benchmark]
        //public int[] ArrayCopy()
        //{
        //    return arraycopyMapper.Map(testArray, null);
        //}

        [Benchmark]
        public string DirectToS()
        {
            return xxx.ToString();
        }

        [Benchmark]
        public string GetToString()
        {
            return GetToStringCore(xxx);
        }

        [Benchmark]
        public string BoxedToStringd()
        {
            return ((object)xxx).ToString();
        }

        static string GetToStringCore<T>(T item)
        {
            return item.ToString();
        }

        //[Benchmark(Baseline = true)]
        //public Foo HyperMapper()
        //{
        //    return StandardResolver.Default.GetMapper<Foo, Foo>().Map(_foo, StandardResolver.Default);
        //}

        //[Benchmark()]
        //public Foo AutoMapper()
        //{
        //    return Mapper.Map<Foo, Foo>(_foo);
        //}

        //[Benchmark()]
        //public Foo TinyMapper()
        //{
        //    return global::Nelibur.ObjectMapper.TinyMapper.Map<Foo, Foo>(_foo);
        //}

        //[Benchmark()]
        //public Foo Mapster()
        //{
        //    return _foo.Adapt<Foo>();
        //}

        //[Benchmark()]
        //public Foo ExpressMapper()
        //{
        //    return global::ExpressMapper.Mapper.Map<Foo, Foo>(_foo);
        //}

        //[Benchmark()]
        //public Foo JsonNet()
        //{
        //    return Newtonsoft.Json.JsonConvert.DeserializeObject<Foo>(Newtonsoft.Json.JsonConvert.SerializeObject(_foo));
        //}

        //[Benchmark()]
        //public Foo MsgPack()
        //{
        //    return MessagePack.MessagePackSerializer.Deserialize<Foo>(MessagePack.MessagePackSerializer.Serialize(_foo));
        //}
    }
}
