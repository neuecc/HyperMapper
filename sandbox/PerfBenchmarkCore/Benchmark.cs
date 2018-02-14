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

        static MapBenchmark()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Foo, FooDest>();
                cfg.CreateMap<InnerFoo, InnerFooDest>();
                cfg.CreateMap<Foo, Foo>();
                cfg.CreateMap<InnerFoo, InnerFoo>();
            });

            _foo = Foo.New();

            global::Nelibur.ObjectMapper.TinyMapper.Bind<Foo, FooDest>();
            global::Nelibur.ObjectMapper.TinyMapper.Bind<Foo, Foo>();
            global::Nelibur.ObjectMapper.TinyMapper.Bind<InnerFoo, InnerFoo>();

            var mapper = StandardResolver.Default.GetMapper<Foo, Foo>();
        }

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

        [Benchmark()]
        public Foo Mapster()
        {
            return _foo.Adapt<Foo>();
        }

        //[Benchmark()]
        //public Foo ExpressMapper()
        //{
        //    return global::ExpressMapper.Mapper.Map<Foo, Foo>(_foo);
        //}


        [Benchmark(Baseline = true)]
        public Foo HyperMapper()
        {
            return StandardResolver.Default.GetMapper<Foo, Foo>().Map(_foo, StandardResolver.Default);
        }


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
